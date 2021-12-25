using PaintDotNet;
using PaintDotNet.Effects;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace TRsDistortThis
{
    internal partial class EffectPluginConfigDialog : EffectConfigDialog
    {
        private Point[] srcCorners = new Point[4];
        private Point[] previewCorners = new Point[4];
        private Rectangle srcWorkArea = new Rectangle();
        private Rectangle previewWorkArea = new Rectangle();
        private PointF ConvertXY = new PointF(1, 1);
        private bool moveflag = false;
        private bool SWAFlag = false;
        private int CornerSelect = -1;
        private Point cornerOrigin = Point.Empty;
        private int sideSelected = -1;
        private Point sideOriginA = Point.Empty;
        private Point sideOriginB = Point.Empty;
        private Point mouseDownPoint = Point.Empty;
        private Point xMirrorPaintPoint = Point.Empty;
        private Point yMirrorPaintPoint = Point.Empty;
        private Point[] tweak = new Point[4];
        private bool initialize = true;
        private bool nonNumberEntered = true;
        private Rectangle srcBounds = Rectangle.Empty;
        private readonly Keys[] keyCheck =
        {
            Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9,
            Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5,
            Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9, Keys.Back, Keys.Enter, Keys.Space
        };

        public EffectPluginConfigDialog()
        {
            InitializeComponent();

            SeeThru.Enabled = EnableOpacity;

            // Dark Theme Fixes
            cParam.ForeColor = this.ForeColor;
            cParam.BackColor = this.BackColor;
        }

        protected override void InitialInitToken()
        {
            theEffectToken = new EffectPluginConfigToken();
        }

        protected override void InitTokenFromDialog()
        {
            EffectPluginConfigToken token = (EffectPluginConfigToken)base.theEffectToken;
            token.Anchor = previewWorkArea;
            token.Corners = srcCorners;
            token.vCorners = previewCorners;
            token.sCorners = srcWorkArea;

            token.initialize = false;
            token.Tweak = tweak;

            token.AlphaTrans = AlphaBox.Checked;
            token.AaLevel = AaTrack.Value;
            token.Perspective = PerspBox.Checked;
            token.UValue = PerspBox.Checked ? UAxis.Value : 1;
            token.VValue = PerspBox.Checked ? VAxis.Value : 1;
        }

        protected override void InitDialogFromToken(EffectConfigToken effectToken)
        {
            EffectPluginConfigToken token = (EffectPluginConfigToken)effectToken;
            srcCorners = token.Corners;
            previewWorkArea = token.Anchor;
            srcWorkArea = token.sCorners;
            previewCorners = token.vCorners;

            tweak = token.Tweak;
            initialize = token.initialize;

            AlphaBox.Checked = token.AlphaTrans;
            AaTrack.Value = token.AaLevel;
            PerspBox.Checked = token.Perspective;
            UAxis.Value = token.UValue;
            VAxis.Value = token.VValue;
            SWAFlag = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            srcBounds = this.EnvironmentParameters.SourceSurface.Bounds;
            float ratio = (float)srcBounds.Height / srcBounds.Width;

            if (ratio > 1)
            {
                int tx = (int)MathF.Round(PreViewBMP.ClientRectangle.Width / ratio);
                int pos = (int)MathF.Round((PreViewBMP.ClientRectangle.Width - tx) / 2f);
                PreViewBMP.Bounds = new Rectangle(pos, PreViewBMP.Top, tx, PreViewBMP.Height);
            }
            else if (ratio < 1)
            {
                int ty = (int)MathF.Round(PreViewBMP.ClientRectangle.Height * ratio);
                int pos = (int)MathF.Round((PreViewBMP.ClientRectangle.Height - ty) / 2f);
                PreViewBMP.Bounds = new Rectangle(PreViewBMP.Left, pos, PreViewBMP.ClientRectangle.Width, ty);
            }
            //=====make checkerboard

            Size previewBounds = PreViewBMP.ClientRectangle.Size;

            PreViewBMP.BackgroundImage = new Bitmap(previewBounds.Width, previewBounds.Height);
            PreViewBMP.Image = new Bitmap(previewBounds.Width, previewBounds.Height);
            using (Surface tmp = new Surface(previewBounds))
            {
                ResamplingAlgorithm algorithm = (srcBounds.Width > tmp.Width || srcBounds.Height > tmp.Height)
                    ? ResamplingAlgorithm.Fant
                    : ResamplingAlgorithm.Bicubic;

                tmp.FitSurface(algorithm, this.EnvironmentParameters.SourceSurface);
                using (Graphics g = Graphics.FromImage(PreViewBMP.Image))
                    g.DrawImage(tmp.CreateAliasedBitmap(), 0, 0);

                tmp.ClearWithCheckerboardPattern();
                using (Graphics g = Graphics.FromImage(PreViewBMP.BackgroundImage))
                    g.DrawImage(tmp.CreateAliasedBitmap(), 0, 0);
            }

            ConvertXY.X = (float)(srcBounds.Width - 1) / (previewBounds.Width - 1);
            ConvertXY.Y = (float)(srcBounds.Height - 1) / (previewBounds.Height - 1);
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = EffectPlugin.StaticName + " ver. " + version.Major + "." + version.Minor + "." + version.Build;
            Reset(initialize);

            base.OnLoad(e);
        }

        private void PreViewBMP_MouseDown(object sender, MouseEventArgs e)
        {
            cParam.Visible = false;
            if (SWA.Checked)
            {
                SWAFlag = true;
                previewWorkArea = new Rectangle(e.X, e.Y, 1, 1);
            }
            else if (CheckNode(e.Location))
            {
                this.mouseDownPoint = e.Location;
                MoveNub(e.Location);
                moveflag = true;
                PreViewBMP.Refresh();
            }

            bool CheckNode(Point hit)
            {
                bool results = false;
                for (int i = 0; i < 4; i++)
                {
                    if (MyUtils.Pythag(previewCorners[i], hit) <= 5)
                    {
                        results = true;
                        CornerSelect = i;
                        this.sideSelected = -1;
                        this.cornerOrigin = previewCorners[i];
                        break;
                    }

                    Point sideNub = MyUtils.CenterPoint(new[] { previewCorners[i], previewCorners[(i + 1) % 4] });
                    if (MyUtils.Pythag(sideNub, hit) <= 5)
                    {
                        results = true;
                        this.sideSelected = i;
                        CornerSelect = -1;
                        this.sideOriginA = previewCorners[i];
                        this.sideOriginB = previewCorners[(i + 1) % 4];
                        break;
                    }
                }
                return results;
            }
        }

        private void PreViewBMP_MouseUp(object sender, MouseEventArgs e)
        {
            if (SWAFlag)
            {
                SWAFlag = false;
                SWA.Checked = false;
                PreViewBMP.Cursor = Cursors.Default;
                tweak = new Point[4];
                if (previewWorkArea.Width < 0)
                {
                    previewWorkArea.X += previewWorkArea.Width;
                    previewWorkArea.Width *= -1;
                }
                if (previewWorkArea.Height < 0)
                {
                    previewWorkArea.Y += previewWorkArea.Height;
                    previewWorkArea.Height *= -1;
                }

                previewCorners = previewWorkArea.ToPointArray();
                PreViewBMP.Refresh();

                for (int i = 0; i < 4; i++)
                {
                    srcCorners[i] = getCorner(previewCorners[i], tweak[i]).Clamp(srcBounds);
                }
                srcWorkArea = srcCorners.ToRectangle();
                if (srcWorkArea.Width <= 0 || srcWorkArea.Height <= 0)
                {
                    SWAFlag = true;
                    SWA.Checked = true;
                    return;
                }

                FinishTokenUpdate();
            }
            else if (moveflag)
            {
                moveflag = false;

                if (CornerSelect != -1 || sideSelected != -1)
                {
                    MoveNub(e.Location);
                    PreViewBMP.Refresh();
                    FinishTokenUpdate();
                }
            }
        }

        private void MoveNub(Point mouseLocation)
        {
            int xDist = mouseLocation.X - this.mouseDownPoint.X;
            int yDist = mouseLocation.Y - this.mouseDownPoint.Y;

            Rectangle bounds = this.PreViewBMP.ClientRectangle;

            if (CornerSelect > -1)
            {
                Point nubDest = new Point(this.cornerOrigin.X + xDist, this.cornerOrigin.Y + yDist).Clamp(bounds);

                bool mirrorX = MirrorX.Checked;
                bool mirrorY = MirrorY.Checked;

                if (mirrorX || mirrorY)
                {
                    int[] xSym = { 1, 0, 3, 2 };
                    int[] ySym = { 3, 2, 1, 0 };
                    int[] dSym = { 2, 3, 0, 1 };

                    int xMidPoint = MyUtils.Average(previewCorners[xSym[CornerSelect]].X, previewCorners[CornerSelect].X);
                    int mx = xMidPoint + (xMidPoint - nubDest.X);

                    int yMidPoint = MyUtils.Average(previewCorners[ySym[CornerSelect]].Y, previewCorners[CornerSelect].Y);
                    int my = yMidPoint + (yMidPoint - nubDest.Y);

                    xMirrorPaintPoint = new Point(xMidPoint, nubDest.Y);
                    yMirrorPaintPoint = new Point(nubDest.X, yMidPoint);

                    if (mirrorX && mirrorY)
                    {
                        previewCorners[dSym[CornerSelect]] = new Point(mx, my).Clamp(bounds);
                    }

                    if (mirrorX)
                    {
                        previewCorners[xSym[CornerSelect]] = new Point(mx, nubDest.Y).Clamp(bounds);
                    }

                    if (mirrorY)
                    {
                        previewCorners[ySym[CornerSelect]] = new Point(nubDest.X, my).Clamp(bounds);
                    }
                }

                previewCorners[CornerSelect] = nubDest;
            }
            else if (this.sideSelected > -1)
            {
                Point sideDestA = new Point(this.sideOriginA.X + xDist, this.sideOriginA.Y + yDist);
                Point sideDestB = new Point(this.sideOriginB.X + xDist, this.sideOriginB.Y + yDist);

                if (!bounds.Contains(sideDestA))
                {
                    Point clampedA = sideDestA.Clamp(bounds);
                    Point newDist = new Point(sideDestA.X - clampedA.X, sideDestA.Y - clampedA.Y);

                    sideDestA = new Point(sideDestA.X - newDist.X, sideDestA.Y - newDist.Y);
                    sideDestB = new Point(sideDestB.X - newDist.X, sideDestB.Y - newDist.Y);
                }

                if (!bounds.Contains(sideDestB))
                {
                    Point clampedB = sideDestB.Clamp(bounds);
                    Point newDist = new Point(sideDestB.X - clampedB.X, sideDestB.Y - clampedB.Y);

                    sideDestA = new Point(sideDestA.X - newDist.X, sideDestA.Y - newDist.Y);
                    sideDestB = new Point(sideDestB.X - newDist.X, sideDestB.Y - newDist.Y);
                }

                previewCorners[this.sideSelected] = sideDestA;
                previewCorners[(this.sideSelected + 1) % 4] = sideDestB;
            }

            for (int i = 0; i < 4; i++) srcCorners[i] = getCorner(previewCorners[i], Point.Empty);
            tweak = new Point[4];
        }

        private void PreViewBMP_MouseMove(object sender, MouseEventArgs e)
        {
            if (SWAFlag)
            {
                previewWorkArea.Width = e.X - previewWorkArea.X;
                previewWorkArea.Height = e.Y - previewWorkArea.Y;
                previewWorkArea = previewWorkArea.Clamp(PreViewBMP.ClientRectangle);

                PreViewBMP.Refresh();
            }
            else if (moveflag)
            {
                MoveNub(e.Location);

                PreViewBMP.Refresh();
                FinishTokenUpdate();
            }
        }

        private void PreViewBMP_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.CompositingMode = CompositingMode.SourceOver;

            Rectangle nAnchor = previewWorkArea;
            if (SWAFlag)
            {
                if (nAnchor.Width < 0)
                {
                    nAnchor.X += nAnchor.Width;
                    nAnchor.Width *= -1;
                }
                if (nAnchor.Height < 0)
                {
                    nAnchor.Y += nAnchor.Height;
                    nAnchor.Height *= -1;
                }
            }
            else
            {
                using (GraphicsPath fillPath = new GraphicsPath())
                using (HatchBrush hatch = new HatchBrush(HatchStyle.DiagonalCross, Color.FromArgb(127, Color.Black), Color.FromArgb(127, Color.White)))
                {
                    fillPath.AddRectangles(new RectangleF[] { PreViewBMP.ClientRectangle, nAnchor });
                    e.Graphics.FillPath(hatch, fillPath);
                }
            }
            nAnchor.Width--;
            nAnchor.Height--;
            using (Pen anchorPen = new Pen(SWAFlag ? Color.Blue : Color.FromArgb(128, Color.Blue), 1))
                e.Graphics.DrawRectangle(anchorPen, nAnchor);

            using (Pen vCornersPen = new Pen(Color.FromArgb(128, Color.Red), 3))
            {
                vCornersPen.LineJoin = LineJoin.Bevel;
                e.Graphics.DrawPolygon(vCornersPen, previewCorners);
            }

            if (moveflag && this.sideSelected == -1 && this.CornerSelect > -1)
            {
                bool mirrorX = MirrorX.Checked;
                bool mirrorY = MirrorY.Checked;
                if (mirrorX || mirrorY)
                {
                    if (mirrorX)
                    {
                        e.Graphics.DrawLine(Pens.DimGray, xMirrorPaintPoint.X, xMirrorPaintPoint.Y - 10, xMirrorPaintPoint.X, xMirrorPaintPoint.Y + 10);
                    }

                    if (mirrorY)
                    {
                        e.Graphics.DrawLine(Pens.DimGray, yMirrorPaintPoint.X - 10, yMirrorPaintPoint.Y, yMirrorPaintPoint.X + 10, yMirrorPaintPoint.Y);
                    }

                    if (mirrorX && mirrorY)
                    {
                        int oppositeLR = xMirrorPaintPoint.Y - (xMirrorPaintPoint.Y - yMirrorPaintPoint.Y) * 2;
                        e.Graphics.DrawLine(Pens.DimGray, xMirrorPaintPoint.X, oppositeLR - 10, xMirrorPaintPoint.X, oppositeLR + 10);
                        int oppositeTB = yMirrorPaintPoint.X - (yMirrorPaintPoint.X - xMirrorPaintPoint.X) * 2;
                        e.Graphics.DrawLine(Pens.DimGray, oppositeTB - 10, yMirrorPaintPoint.Y, oppositeTB + 10, yMirrorPaintPoint.Y);
                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
                Rectangle nubPos = new Rectangle(previewCorners[i].X - 5, previewCorners[i].Y - 5, 10, 10);
                bool nubSelected = i == CornerSelect;
                e.Graphics.FillEllipse(nubSelected ? Brushes.LightBlue : Brushes.White, nubPos);
                e.Graphics.DrawEllipse(nubSelected ? Pens.DarkBlue : Pens.Black, nubPos);

                Point sideNub = MyUtils.CenterPoint(new[] { previewCorners[i], previewCorners[(i + 1) % 4] });
                Rectangle sideNubPos = new Rectangle(sideNub.X - 4, sideNub.Y - 4, 8, 8);
                bool sideNubSelected = moveflag && i == sideSelected;
                e.Graphics.FillEllipse(sideNubSelected ? Brushes.LightGreen : Brushes.White, sideNubPos);
                e.Graphics.DrawEllipse(sideNubSelected ? Pens.DarkGreen : Pens.DimGray, sideNubPos);
            }

            if (moveflag && sideSelected > -1)
            {
                int iA = this.sideSelected;
                Rectangle nubPosA = new Rectangle(previewCorners[iA].X - 5, previewCorners[iA].Y - 5, 10, 10);
                e.Graphics.FillEllipse(Brushes.LightBlue, nubPosA);
                e.Graphics.DrawEllipse(Pens.DarkBlue, nubPosA);

                int iB = (this.sideSelected + 1) % 4;
                Rectangle nubPosb = new Rectangle(previewCorners[iB].X - 5, previewCorners[iB].Y - 5, 10, 10);
                e.Graphics.FillEllipse(Brushes.LightBlue, nubPosb);
                e.Graphics.DrawEllipse(Pens.DarkBlue, nubPosb);
            }
        }

        private void RstButton_Click(object sender, EventArgs e)
        {
            Reset(true);
        }

        private void ResetNubsButton_Click(object sender, EventArgs e)
        {
            srcCorners = srcBounds.ToPointArray();
            previewCorners = previewWorkArea.ToPointArray();

            PreViewBMP.Refresh();
            FinishTokenUpdate();
        }

        private void Reset(bool newSet)
        {
            if (newSet)
            {
                srcCorners = srcBounds.ToPointArray();
                srcWorkArea = srcBounds;

                previewCorners = PreViewBMP.ClientRectangle.ToPointArray();
                previewWorkArea = PreViewBMP.ClientRectangle;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    previewCorners[i] = getVcorner(srcCorners[i]);
                    tweak[i] = getTweak(srcCorners[i], previewCorners[i]);
                }
            }
            PreViewBMP.Refresh();
            FinishTokenUpdate();
        }

        private Point getTweak(Point C, Point v)
        {
            return new Point((int)MathF.Round(v.X * ConvertXY.X - C.X),
                             (int)MathF.Round(v.Y * ConvertXY.Y - C.Y));
        }

        private Point getVcorner(Point C)
        {
            return new Point((int)MathF.Round(C.X / ConvertXY.X),
                             (int)MathF.Round(C.Y / ConvertXY.Y));
        }

        private Point getCorner(Point v, Point t)
        {
            return new Point((int)MathF.Round(v.X * ConvertXY.X + t.X),
                             (int)MathF.Round(v.Y * ConvertXY.Y + t.Y));
        }

        private void PerspBox_CheckedChanged(object sender, EventArgs e)
        {
            bool forcedPerspective = PerspBox.Checked;

            UVal.ForeColor = forcedPerspective ? this.ForeColor : Color.Gray;
            UAxis.Enabled = forcedPerspective;
            VVal.ForeColor = forcedPerspective ? this.ForeColor : Color.Gray;
            VAxis.Enabled = forcedPerspective;

            FinishTokenUpdate();
        }

        private void AlphaBox_CheckedChanged(object sender, EventArgs e)
        {
            AaLabel.ForeColor = AlphaBox.Checked ? this.ForeColor : Color.Gray;
            AaTrack.Enabled = AlphaBox.Checked;
            FinishTokenUpdate();
        }

        private void AaLevel_Scroll(object sender, EventArgs e)
        {
            FinishTokenUpdate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                case Keys.Tab:
                    return HandleKeys(keyData);
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

            bool HandleKeys(Keys keys)
            {
                if (cParam.Visible)
                    return false;

                if (CornerSelect == -1)
                    return true;

                bool tweaked = false;
                switch (keys)
                {
                    case Keys.Up:
                        srcCorners[CornerSelect].Y -= 1;
                        tweaked = true;
                        break;
                    case Keys.Down:
                        srcCorners[CornerSelect].Y += 1;
                        tweaked = true;
                        break;
                    case Keys.Left:
                        srcCorners[CornerSelect].X -= 1;
                        tweaked = true;
                        break;
                    case Keys.Right:
                        srcCorners[CornerSelect].X += 1;
                        tweaked = true;
                        break;
                    case Keys.Tab:
                        CornerSelect = (CornerSelect + 1) % 4;
                        tweaked = false;
                        break;
                }

                if (tweaked)
                {
                    srcCorners[CornerSelect] = srcCorners[CornerSelect].Clamp(srcBounds);
                    previewCorners[CornerSelect] = getVcorner(srcCorners[CornerSelect]);
                    tweak[CornerSelect] = getTweak(srcCorners[CornerSelect], previewCorners[CornerSelect]);
                    FinishTokenUpdate();
                }

                PreViewBMP.Refresh();

                return true;
            }
        }

        private void SeeThru_CheckedChanged(object sender, EventArgs e)
        {
            this.Opacity = (SeeThru.Checked) ? 0.4 : 1.0;
        }

        private void SWA_CheckedChanged(object sender, EventArgs e)
        {
            SWAFlag = false;
            if (SWA.Checked)
            {
                Reset(true);
                PreViewBMP.Cursor = Cursors.Cross;
            }
            else
            {
                PreViewBMP.Cursor = Cursors.Default;
            }
        }

        private void UAxis_Scroll(object sender, EventArgs e)
        {
            UVal.Text = $"U Value {UAxis.Value}%";
            FinishTokenUpdate();
        }

        private void VAxis_Scroll(object sender, EventArgs e)
        {
            VVal.Text = $"V Value {VAxis.Value}%";
            FinishTokenUpdate();
        }

        private void cParam_Leave(object sender, EventArgs e)
        {
            cParam.Visible = false;
        }

        private void cParam_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = !keyCheck.Contains(e.KeyCode);
        }

        private void cParam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered) e.Handled = true;
        }

        private void cParam_KeyUp(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.Equals(Keys.Enter))
                return;

            string[] s = cParam.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length != 2)
                return;

            int x, y;
            if (!int.TryParse(s[0], out x) || !int.TryParse(s[1], out y))
                return;

            Point p = new Point(x, y);
            if (!srcBounds.Contains(p))
                return;

            cParam.Visible = false;
            srcCorners[CornerSelect] = p;
            previewCorners[CornerSelect] = getVcorner(srcCorners[CornerSelect]);
            tweak[CornerSelect] = getTweak(srcCorners[CornerSelect], previewCorners[CornerSelect]);
            PreViewBMP.Refresh();
            FinishTokenUpdate();
        }

        private void PreViewBMP_DoubleClick(object sender, EventArgs e)
        {
            if (CornerSelect == -1)
                return;
            cParam.Text = $"{srcCorners[CornerSelect].X} {srcCorners[CornerSelect].Y}";
            cParam.Select(cParam.Text.Length, 0);
            Point p = PreViewBMP.ClientRectangle.CenterPoint();
            cParam.Location = new Point(p.X - cParam.Width / 2, p.Y - cParam.Height / 2);
            cParam.Visible = true;
            cParam.Focus();
        }

        protected override void OnHelpButtonClicked(CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnHelpButtonClicked(e);

            const string helpMessage = "The control nubs can be manipulated in the following ways:\n"
                + "\n"
                + "  - Arrow Keys move the Selected Corner by 1px\n"
                + "\n"
                + "  - Tab Key to Select the Next Corner.\n"
                + "\n"
                + "  - Double Click to input coordinates manually.\n";

            MessageBox.Show(helpMessage, "Help");
        }
    }
}