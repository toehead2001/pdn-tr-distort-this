using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Reflection;
using PaintDotNet.Effects;
using PaintDotNet;
using System.Linq;

namespace TRsDistortThis
{
    internal partial class EffectPluginConfigDialog : EffectConfigDialog
    {
        private Point[] Corners = new Point[4];
        private Point[] vCorners = new Point[4];
        private Rectangle sCorners = new Rectangle();
        private Rectangle anchor = new Rectangle();
        private PointF ConvertXY = new PointF(1, 1);
        private bool moveflag = false;
        private bool SWAFlag = false;
        private int CornerSelect = -1;
        private Point[] tweak = new Point[4];
        private bool initialize = true;
        bool nonNumberEntered = true;
        Keys[] keyCheck ={Keys.D0,Keys.D1,Keys.D2,Keys.D3,Keys.D4,Keys.D5,
                          Keys.D6,Keys.D7,Keys.D8,Keys.D9,
                          Keys.NumPad0,Keys.NumPad1,Keys.NumPad2,Keys.NumPad3,Keys.NumPad4,Keys.NumPad5,
                          Keys.NumPad6,Keys.NumPad7,Keys.NumPad8,Keys.NumPad9,
                          Keys.Back,Keys.Enter,Keys.Space};

        public EffectPluginConfigDialog()
        {
            InitializeComponent();
        }

        protected override void InitialInitToken()
        {
            theEffectToken = new EffectPluginConfigToken(new Point[4], new Rectangle(), new Rectangle(), new Point[4],
                new Point[4], true, true, 100, 100, true);
        }

        protected override void InitTokenFromDialog()
        {
            EffectPluginConfigToken token = (EffectPluginConfigToken)base.theEffectToken;
            token.Anchor = anchor;
            token.Corners = Corners;
            token.vCorners = vCorners;
            token.sCorners = sCorners;

            token.initialize = false;
            token.Tweak = tweak;

            token.AlphaTrans = AlphaBox.Checked;
            token.Perspective = PerspBox.Checked;
            token.UValue = UAxis.Value;
            token.VValue = VAxis.Value;
        }

        protected override void InitDialogFromToken(EffectConfigToken effectToken)
        {
            EffectPluginConfigToken token = (EffectPluginConfigToken)effectToken;
            Corners = token.Corners;
            anchor = token.Anchor;
            sCorners = token.sCorners;
            vCorners = token.vCorners;

            tweak = token.Tweak;
            initialize = token.initialize;

            AlphaBox.Checked = token.AlphaTrans;
            PerspBox.Checked = token.Perspective;
            UAxis.Value = token.UValue;
            VAxis.Value = token.VValue;
            SWAFlag = false;
        }

        private void EffectPluginConfigDialog_Load(object sender, EventArgs e)
        {
            float ratio = (float)EffectSourceSurface.Height / EffectSourceSurface.Width;

            if (ratio > 1)
            {
                int tx = (int)Math.Round(PreViewBMP.ClientRectangle.Width / ratio);
                int pos = (int)Math.Round((PreViewBMP.ClientRectangle.Width - tx) / 2f);
                PreViewBMP.Bounds = new Rectangle(pos, PreViewBMP.Top, tx, PreViewBMP.Height);
            }
            else if (ratio < 1)
            {
                int ty = (int)Math.Round(PreViewBMP.ClientRectangle.Height * ratio);
                int pos = (int)Math.Round((PreViewBMP.ClientRectangle.Height - ty) / 2f);
                PreViewBMP.Bounds = new Rectangle(PreViewBMP.Left, pos, PreViewBMP.ClientRectangle.Width, ty);
            }
            //=====make checkerboard

            PreViewBMP.BackgroundImage = new Bitmap(PreViewBMP.ClientRectangle.Width, PreViewBMP.ClientRectangle.Height);
            PreViewBMP.Image = new Bitmap(PreViewBMP.ClientRectangle.Width, PreViewBMP.ClientRectangle.Height);
            using (Surface tmp = new Surface(PreViewBMP.ClientRectangle.Size))
            {
                ResamplingAlgorithm algorithm = (EffectSourceSurface.Width > tmp.Width || EffectSourceSurface.Height > tmp.Height) ? ResamplingAlgorithm.Fant : ResamplingAlgorithm.Bicubic;
                tmp.FitSurface(algorithm, EffectSourceSurface);
                using (Graphics g = Graphics.FromImage(PreViewBMP.Image))
                    g.DrawImage(tmp.CreateAliasedBitmap(), 0, 0);

                tmp.ClearWithCheckerboardPattern();
                using (Graphics g = Graphics.FromImage(PreViewBMP.BackgroundImage))
                    g.DrawImage(tmp.CreateAliasedBitmap(), 0, 0);
            }
            GC.Collect();

            this.Opacity = 1;
            ConvertXY.X = (float)(EffectSourceSurface.Width - 1) / (PreViewBMP.ClientRectangle.Width - 1);
            ConvertXY.Y = (float)(EffectSourceSurface.Height - 1) / (PreViewBMP.ClientRectangle.Height - 1);
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = EffectPlugin.StaticName + " ver. " + version.Major + "." + version.Minor + "." + version.Build;
            ReZet(initialize);
        }

        private void PreViewBMP_MouseDown(object sender, MouseEventArgs e)
        {
            cParam.Visible = false;
            if (SWA.Checked)
            {
                SWAFlag = true;
                anchor = new Rectangle(e.X, e.Y, 1, 1);
            }
            else if (CheckNode(e.X, e.Y))
            {
                symmetry(e.Location);
                moveflag = true;
                PreViewBMP.Refresh();
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
                if (anchor.Width < 0)
                {
                    anchor.X += anchor.Width;
                    anchor.Width *= -1;
                }
                if (anchor.Height < 0)
                {
                    anchor.Y += anchor.Height;
                    anchor.Height *= -1;
                }

                vCorners = MyBounds(anchor);
                PreViewBMP.Refresh();


                for (int i = 0; i < 4; i++)
                {
                    Corners[i] = getCorner(vCorners[i], tweak[i]).Clamp(EffectSourceSurface.Bounds);
                }
                sCorners = MyRect(Corners);
                if (sCorners.Width <= 0 || sCorners.Height <= 0)
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

                if (CornerSelect != -1)
                {
                    symmetry(e.Location);
                    PreViewBMP.Refresh();
                    FinishTokenUpdate();
                }

            }
        }

        private void symmetry(Point e)
        {
            vCorners[CornerSelect] = e.Clamp(PreViewBMP.ClientRectangle);

            Point pmid = MyUtils.Centroid(vCorners);
            int mx = pmid.X + (pmid.X - e.X);
            int my = pmid.Y + (pmid.Y - e.Y);
            if (MirrorX.Checked && MirrorY.Checked)
            {
                int[] sym = { 1, 0, 3, 2 };
                vCorners[sym[CornerSelect]] = new Point(mx, e.Y).Clamp(PreViewBMP.ClientRectangle);
                int[] sym2 = { 3, 2, 1, 0 };
                vCorners[sym2[CornerSelect]] = new Point(e.X, my).Clamp(PreViewBMP.ClientRectangle);
                int[] sym3 = { 2, 3, 0, 1 };
                vCorners[sym3[CornerSelect]] = new Point(mx, my).Clamp(PreViewBMP.ClientRectangle);
            }
            else if (MirrorX.Checked)
            {
                int[] sym = { 1, 0, 3, 2 };
                vCorners[sym[CornerSelect]] = new Point(mx, e.Y).Clamp(PreViewBMP.ClientRectangle);
            }
            else if (MirrorY.Checked)
            {
                int[] sym = { 3, 2, 1, 0 };
                vCorners[sym[CornerSelect]] = new Point(e.X, my).Clamp(PreViewBMP.ClientRectangle);
            }

            for (int i = 0; i < 4; i++) Corners[i] = getCorner(vCorners[i], Point.Empty);
            tweak = new Point[4];
        }

        private void PreViewBMP_MouseMove(object sender, MouseEventArgs e)
        {
            if (SWAFlag)
            {
                anchor.Width = e.X - anchor.X;
                anchor.Height = e.Y - anchor.Y;
                anchor = anchor.Clamp(PreViewBMP.ClientRectangle);

                PreViewBMP.Refresh();
            }
            else if (moveflag)
            {
                symmetry(e.Location);

                PreViewBMP.Refresh();
                FinishTokenUpdate();
            }

        }

        private void PreViewBMP_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.CompositingMode = CompositingMode.SourceOver;

            Rectangle nAnchor = anchor;
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
            using (Pen anchorPen = new Pen((SWAFlag) ? Color.Blue : Color.FromArgb(128, Color.Blue), 1))
                e.Graphics.DrawRectangle(anchorPen, nAnchor);

            using (Pen vCornersPen = new Pen(Color.FromArgb(128, Color.Red), 3))
            {
                vCornersPen.LineJoin = LineJoin.Bevel;
                e.Graphics.DrawPolygon(vCornersPen, vCorners);
            }

            for (int i = 0; i < 4; i++)
            {
                Rectangle nubPos = new Rectangle(vCorners[i].X - 5, vCorners[i].Y - 5, 10, 10);
                e.Graphics.FillEllipse(i == CornerSelect ? Brushes.LightBlue : Brushes.White, nubPos);
                e.Graphics.DrawEllipse(i == CornerSelect ? Pens.DarkBlue : Pens.Black, nubPos);
            }
        }

        private void RstButton_Click(object sender, EventArgs e)
        {
            ReZet(true);
        }

        private void ResetNubsButton_Click(object sender, EventArgs e)
        {
            vCorners = MyBounds(anchor);
            PreViewBMP.Refresh();

            for (int i = 0; i < 4; i++)
            {
                Corners[i] = getCorner(vCorners[i], tweak[i]).Clamp(EffectSourceSurface.Bounds);
            }
            FinishTokenUpdate();
        }

        private void ReZet(bool newSet)
        {
            if (newSet)
            {
                Corners = MyBounds(EffectSourceSurface.Bounds);
                sCorners = EffectSourceSurface.Bounds;
                vCorners = MyBounds(PreViewBMP.ClientRectangle);

                anchor = PreViewBMP.ClientRectangle;// MyRect(vCorners);
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    vCorners[i] = getVcorner(Corners[i]);
                    tweak[i] = getTweak(Corners[i], vCorners[i]);
                }
            }
            PreViewBMP.Refresh();
            FinishTokenUpdate();
            //test(Corners);
        }

        private Point getTweak(Point C, Point v)
        {
            return new Point((int)Math.Round(v.X * ConvertXY.X - C.X),
                             (int)Math.Round(v.Y * ConvertXY.Y - C.Y));
        }

        private Point getVcorner(Point C)
        {
            return new Point((int)Math.Round(C.X / ConvertXY.X),
                             (int)Math.Round(C.Y / ConvertXY.Y));
        }

        private Point getCorner(Point v, Point t)
        {
            return new Point((int)Math.Round(v.X * ConvertXY.X + t.X),
                             (int)Math.Round(v.Y * ConvertXY.Y + t.Y));
        }

        private void test(Point[] v)
        {
            //test
            string s = String.Empty;
            foreach (Point i in v) s += " " + i.ToString();
        }

        private void Box_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Name == PerspBox.Name)
            {
                if (cb.Checked)
                {
                    UVal.ForeColor = Color.Black;
                    UAxis.Enabled = true;
                    UAxis.Value = 100;
                    VVal.ForeColor = Color.Black;
                    VAxis.Enabled = true;
                    VAxis.Value = 100;
                }
                else
                {
                    UVal.ForeColor = Color.Gray;
                    UAxis.Enabled = false;
                    UAxis.Value = 0;
                    VVal.ForeColor = Color.Gray;
                    VAxis.Enabled = false;
                    VAxis.Value = 0;
                }
            }

            FinishTokenUpdate();
        }

        private Point[] MyBounds(Rectangle y)
        {
            Point[] x = new Point[4];
            x[0] = new Point(y.Left, y.Top);
            x[1] = new Point(y.Width + y.Left - 1, y.Top);
            x[2] = new Point(y.Width + y.Left - 1, y.Top + y.Height - 1);
            x[3] = new Point(y.Left, y.Top + y.Height - 1);
            return x;
        }

        private Rectangle MyRect(Point[] y) //fix rotation
        {
            return new Rectangle
            {
                //prelim
                Width = y[2].X - y[0].X,
                Height = y[2].Y - y[0].Y,
                Location = y[0]
            };
        }

        private bool CheckNode(int x, int y)
        {
            bool results = false;
            Point fixmotion = new Point(x, y);
            for (int i = 0; i < 4; i++)
            {
                if (MyUtils.Pythag(vCorners[i], fixmotion) <= 5)
                {
                    results = true;
                    CornerSelect = i;
                }
            }
            return results;
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
                    HandleKeys(keyData);
                    break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

            return true;
        }

        private void HandleKeys(Keys e)
        {
            if (CornerSelect != -1)
            {
                bool tweaked = false;
                switch (e)
                {
                    case Keys.Up:
                        Corners[CornerSelect].Y -= 1;
                        tweaked = true;
                        break;
                    case Keys.Down:
                        Corners[CornerSelect].Y += 1;
                        tweaked = true;
                        break;
                    case Keys.Left:
                        Corners[CornerSelect].X -= 1;
                        tweaked = true;
                        break;
                    case Keys.Right:
                        Corners[CornerSelect].X += 1;
                        tweaked = true;
                        break;
                    case Keys.Tab:
                        CornerSelect = (CornerSelect + 1) % 4;
                        tweaked = false;
                        break;
                }

                if (tweaked)
                {
                    Corners[CornerSelect] = Corners[CornerSelect].Clamp(EffectSourceSurface.Bounds);
                    vCorners[CornerSelect] = getVcorner(Corners[CornerSelect]);
                    tweak[CornerSelect] = getTweak(Corners[CornerSelect], vCorners[CornerSelect]);
                }
                FinishTokenUpdate();
                PreViewBMP.Refresh();
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
                ReZet(true);
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
            if (!Int32.TryParse(s[0], out x) || !Int32.TryParse(s[1], out y))
                return;

            Point p = new Point(x, y);
            if (!EffectSourceSurface.Bounds.Contains(p))
                return;

            cParam.Visible = false;
            Corners[CornerSelect] = p;
            vCorners[CornerSelect] = getVcorner(Corners[CornerSelect]);
            tweak[CornerSelect] = getTweak(Corners[CornerSelect], vCorners[CornerSelect]);
            PreViewBMP.Refresh();
            FinishTokenUpdate();
        }

        private void PreViewBMP_DoubleClick(object sender, EventArgs e)
        {
            if (CornerSelect == -1) return;
            //test(Corners);
            cParam.Text = String.Format("{0} {1}", Corners[CornerSelect].X, Corners[CornerSelect].Y);
            Point p = MyUtils.Centroid(MyBounds(PreViewBMP.ClientRectangle));
            cParam.Select(cParam.Text.Length, 0);
            cParam.Location = new Point(p.X - cParam.Width / 2, p.Y - cParam.Height / 2);
            cParam.Visible = true;
            cParam.Focus();
        }
    }
}