using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Reflection;
using PaintDotNet.Effects;
using PaintDotNet;

namespace TRsDistortThis
{
    public class EffectPluginConfigDialog : EffectConfigDialog
    {
        private Point[] Corners = new Point[4];
        private Point[] vCorners = new Point[4];
        private Rectangle sCorners = new Rectangle();
        private Rectangle anchor = new Rectangle();
        private PointF ConvertXY = new PointF(1, 1);
        private bool moveflag = false;
        private bool RenderFlag = false;
        private bool SWAFlag = false;
        private int CornerSelect = -1;
        private Point[] tweak = new Point[4];
        private Bitmap CheckerBG;
        private float ratio;
        private bool closeform = false;
        private bool initialize = true;
        bool nonNumberEntered = true;
        Keys[] keyCheck ={Keys.D0,Keys.D1,Keys.D2,Keys.D3,Keys.D4,Keys.D5,
                          Keys.D6,Keys.D7,Keys.D8,Keys.D9,
                          Keys.NumPad0,Keys.NumPad1,Keys.NumPad2,Keys.NumPad3,Keys.NumPad4,Keys.NumPad5,
                          Keys.NumPad6,Keys.NumPad7,Keys.NumPad8,Keys.NumPad9,
                          Keys.Back,Keys.Enter,Keys.Space};
        //==================================================

        private Button buttonOK;
        private Panel PreViewBMP;
        private CheckBox AlphaBox;
        private CheckBox PerspBox;
        private Button RstButton;
        private Label label1;
        private IContainer components;
        private Label Busy;
        private Timer timer1;
        private CheckBox SeeThru;
        private CheckBox SWA;
        private HScrollBar UAxis;
        private HScrollBar VAxis;
        private Label UVal;
        private Label VVal;
        private CheckBox MirrorX;
        private CheckBox MirrorY;
        private TextBox cParam;
        private Button buttonCancel;

        public EffectPluginConfigDialog()
        {
            InitializeComponent();
        }

        protected override void InitialInitToken()
        {
            theEffectToken = new EffectPluginConfigToken(new Point[4], new Rectangle(), new Rectangle(), new Point[4],
                new Point[4], true, true, false, 100, 100, true);
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
            token.RenderFlag = RenderFlag;
            token.UValue = UAxis.Value;
            token.VValue = VAxis.Value;
            RenderFlag = false;
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
            RenderFlag = token.RenderFlag;
            SWAFlag = false;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.PreViewBMP = new System.Windows.Forms.Panel();
            this.cParam = new System.Windows.Forms.TextBox();
            this.AlphaBox = new System.Windows.Forms.CheckBox();
            this.PerspBox = new System.Windows.Forms.CheckBox();
            this.RstButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Busy = new System.Windows.Forms.Label();
            this.SeeThru = new System.Windows.Forms.CheckBox();
            this.SWA = new System.Windows.Forms.CheckBox();
            this.UAxis = new System.Windows.Forms.HScrollBar();
            this.VAxis = new System.Windows.Forms.HScrollBar();
            this.UVal = new System.Windows.Forms.Label();
            this.VVal = new System.Windows.Forms.Label();
            this.MirrorX = new System.Windows.Forms.CheckBox();
            this.MirrorY = new System.Windows.Forms.CheckBox();
            this.PreViewBMP.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.Location = new System.Drawing.Point(416, 355);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 37);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.ForeColor = System.Drawing.Color.White;
            this.buttonOK.Location = new System.Drawing.Point(416, 400);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(72, 40);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.TabStop = false;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // PreViewBMP
            // 
            this.PreViewBMP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PreViewBMP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PreViewBMP.Controls.Add(this.cParam);
            this.PreViewBMP.Location = new System.Drawing.Point(5, 12);
            this.PreViewBMP.Name = "PreViewBMP";
            this.PreViewBMP.Size = new System.Drawing.Size(402, 380);
            this.PreViewBMP.TabIndex = 3;
            this.PreViewBMP.Paint += new System.Windows.Forms.PaintEventHandler(this.PreViewBMP_Paint);
            this.PreViewBMP.DoubleClick += new System.EventHandler(this.PreViewBMP_DoubleClick);
            this.PreViewBMP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PreViewBMP_MouseDown);
            this.PreViewBMP.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PreViewBMP_MouseMove);
            this.PreViewBMP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PreViewBMP_MouseUp);
            // 
            // cParam
            // 
            this.cParam.Location = new System.Drawing.Point(171, 169);
            this.cParam.Name = "cParam";
            this.cParam.Size = new System.Drawing.Size(49, 20);
            this.cParam.TabIndex = 0;
            this.cParam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cParam.Visible = false;
            this.cParam.TextChanged += new System.EventHandler(this.cParam_TextChanged);
            this.cParam.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cParam_KeyDown);
            this.cParam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cParam_KeyPress);
            this.cParam.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cParam_KeyUp);
            this.cParam.Leave += new System.EventHandler(this.cParam_Leave);
            // 
            // AlphaBox
            // 
            this.AlphaBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.AlphaBox.AutoSize = true;
            this.AlphaBox.Checked = true;
            this.AlphaBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AlphaBox.Location = new System.Drawing.Point(410, 54);
            this.AlphaBox.Name = "AlphaBox";
            this.AlphaBox.Size = new System.Drawing.Size(83, 30);
            this.AlphaBox.TabIndex = 4;
            this.AlphaBox.TabStop = false;
            this.AlphaBox.Text = "Alpha \r\nTransparent";
            this.AlphaBox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.AlphaBox.UseVisualStyleBackColor = true;
            this.AlphaBox.CheckedChanged += new System.EventHandler(this.Box_CheckedChanged);
            // 
            // PerspBox
            // 
            this.PerspBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PerspBox.AutoSize = true;
            this.PerspBox.Checked = true;
            this.PerspBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PerspBox.Location = new System.Drawing.Point(410, 90);
            this.PerspBox.Name = "PerspBox";
            this.PerspBox.Size = new System.Drawing.Size(82, 30);
            this.PerspBox.TabIndex = 5;
            this.PerspBox.TabStop = false;
            this.PerspBox.Text = "Forced \r\nPerspective";
            this.PerspBox.UseVisualStyleBackColor = false;
            this.PerspBox.CheckedChanged += new System.EventHandler(this.Box_CheckedChanged);
            // 
            // RstButton
            // 
            this.RstButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RstButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.RstButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RstButton.ForeColor = System.Drawing.Color.White;
            this.RstButton.Location = new System.Drawing.Point(417, 12);
            this.RstButton.Name = "RstButton";
            this.RstButton.Size = new System.Drawing.Size(72, 36);
            this.RstButton.TabIndex = 8;
            this.RstButton.TabStop = false;
            this.RstButton.Text = "Reset";
            this.RstButton.UseVisualStyleBackColor = false;
            this.RstButton.Click += new System.EventHandler(this.RstButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.BackColor = System.Drawing.Color.Navy;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(5, 401);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(402, 42);
            this.label1.TabIndex = 9;
            this.label1.Text = "Arrow Keys move Selected Corner by One Pixel. \r\nUse Tab Key to Select Next Corner" +
    ", DoubleClick to Edit";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Busy
            // 
            this.Busy.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Busy.BackColor = System.Drawing.Color.Black;
            this.Busy.Font = new System.Drawing.Font("Microstyle Bold Extended ATT", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Busy.ForeColor = System.Drawing.Color.DimGray;
            this.Busy.Location = new System.Drawing.Point(411, 286);
            this.Busy.Name = "Busy";
            this.Busy.Size = new System.Drawing.Size(82, 26);
            this.Busy.TabIndex = 10;
            this.Busy.Text = "BUSY";
            this.Busy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SeeThru
            // 
            this.SeeThru.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SeeThru.AutoSize = true;
            this.SeeThru.Location = new System.Drawing.Point(416, 325);
            this.SeeThru.Name = "SeeThru";
            this.SeeThru.Size = new System.Drawing.Size(67, 17);
            this.SeeThru.TabIndex = 11;
            this.SeeThru.TabStop = false;
            this.SeeThru.Text = "SeeThru";
            this.SeeThru.UseVisualStyleBackColor = true;
            this.SeeThru.CheckedChanged += new System.EventHandler(this.SeeThru_CheckedChanged);
            // 
            // SWA
            // 
            this.SWA.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.SWA.Location = new System.Drawing.Point(412, 253);
            this.SWA.Name = "SWA";
            this.SWA.Size = new System.Drawing.Size(78, 30);
            this.SWA.TabIndex = 5;
            this.SWA.TabStop = false;
            this.SWA.Text = "Set Work Area";
            this.SWA.UseVisualStyleBackColor = true;
            this.SWA.CheckedChanged += new System.EventHandler(this.SWA_CheckedChanged);
            // 
            // UAxis
            // 
            this.UAxis.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.UAxis.LargeChange = 1;
            this.UAxis.Location = new System.Drawing.Point(412, 136);
            this.UAxis.Name = "UAxis";
            this.UAxis.Size = new System.Drawing.Size(78, 9);
            this.UAxis.TabIndex = 12;
            this.UAxis.Value = 100;
            this.UAxis.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Axis_Scroll);
            this.UAxis.ValueChanged += new System.EventHandler(this.Axis_ValueChanged);
            // 
            // VAxis
            // 
            this.VAxis.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.VAxis.LargeChange = 1;
            this.VAxis.Location = new System.Drawing.Point(412, 168);
            this.VAxis.Name = "VAxis";
            this.VAxis.Size = new System.Drawing.Size(78, 9);
            this.VAxis.TabIndex = 12;
            this.VAxis.Value = 100;
            this.VAxis.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Axis_Scroll);
            this.VAxis.ValueChanged += new System.EventHandler(this.Axis_ValueChanged);
            // 
            // UVal
            // 
            this.UVal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.UVal.AutoSize = true;
            this.UVal.Location = new System.Drawing.Point(412, 123);
            this.UVal.Name = "UVal";
            this.UVal.Size = new System.Drawing.Size(74, 13);
            this.UVal.TabIndex = 13;
            this.UVal.Text = "U Value 100%";
            // 
            // VVal
            // 
            this.VVal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.VVal.AutoSize = true;
            this.VVal.Location = new System.Drawing.Point(415, 155);
            this.VVal.Name = "VVal";
            this.VVal.Size = new System.Drawing.Size(73, 13);
            this.VVal.TabIndex = 13;
            this.VVal.Text = "V Value 100%";
            // 
            // MirrorX
            // 
            this.MirrorX.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.MirrorX.AutoSize = true;
            this.MirrorX.Location = new System.Drawing.Point(418, 195);
            this.MirrorX.Name = "MirrorX";
            this.MirrorX.Size = new System.Drawing.Size(62, 17);
            this.MirrorX.TabIndex = 5;
            this.MirrorX.TabStop = false;
            this.MirrorX.Text = "Mirror X";
            this.MirrorX.UseVisualStyleBackColor = false;
            // 
            // MirrorY
            // 
            this.MirrorY.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.MirrorY.AutoSize = true;
            this.MirrorY.Location = new System.Drawing.Point(418, 218);
            this.MirrorY.Name = "MirrorY";
            this.MirrorY.Size = new System.Drawing.Size(62, 17);
            this.MirrorY.TabIndex = 5;
            this.MirrorY.TabStop = false;
            this.MirrorY.Text = "Mirror Y";
            this.MirrorY.UseVisualStyleBackColor = false;
            // 
            // EffectPluginConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(496, 452);
            this.Controls.Add(this.VVal);
            this.Controls.Add(this.UVal);
            this.Controls.Add(this.VAxis);
            this.Controls.Add(this.UAxis);
            this.Controls.Add(this.SeeThru);
            this.Controls.Add(this.Busy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RstButton);
            this.Controls.Add(this.SWA);
            this.Controls.Add(this.MirrorY);
            this.Controls.Add(this.MirrorX);
            this.Controls.Add(this.PerspBox);
            this.Controls.Add(this.AlphaBox);
            this.Controls.Add(this.PreViewBMP);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimizeBox = true;
            this.Name = "EffectPluginConfigDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "0";
            this.Activated += new System.EventHandler(this.EffectPluginConfigDialog_Activated);
            this.Deactivate += new System.EventHandler(this.EffectPluginConfigDialog_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EffectPluginConfigDialog_FormClosing);
            this.Load += new System.EventHandler(this.EffectPluginConfigDialog_Load);
            this.Controls.SetChildIndex(this.buttonCancel, 0);
            this.Controls.SetChildIndex(this.buttonOK, 0);
            this.Controls.SetChildIndex(this.PreViewBMP, 0);
            this.Controls.SetChildIndex(this.AlphaBox, 0);
            this.Controls.SetChildIndex(this.PerspBox, 0);
            this.Controls.SetChildIndex(this.MirrorX, 0);
            this.Controls.SetChildIndex(this.MirrorY, 0);
            this.Controls.SetChildIndex(this.SWA, 0);
            this.Controls.SetChildIndex(this.RstButton, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.Busy, 0);
            this.Controls.SetChildIndex(this.SeeThru, 0);
            this.Controls.SetChildIndex(this.UAxis, 0);
            this.Controls.SetChildIndex(this.VAxis, 0);
            this.Controls.SetChildIndex(this.UVal, 0);
            this.Controls.SetChildIndex(this.VVal, 0);
            this.PreViewBMP.ResumeLayout(false);
            this.PreViewBMP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Application.DoEvents();
            FinishTokenUpdate();
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EffectPluginConfigDialog_Load(object sender, EventArgs e)
        {
            Surface srf = EffectSourceSurface;
            PreViewBMP.BackgroundImage = srf.CreateAliasedBitmap();
            ratio = (float)PreViewBMP.BackgroundImage.Height / PreViewBMP.BackgroundImage.Width;

            if (ratio > 1)
            {
                float tx = (float)PreViewBMP.ClientRectangle.Width / ratio;
                float pos = ((float)PreViewBMP.ClientRectangle.Width - tx) / 2;
                PreViewBMP.Bounds = new Rectangle((int)pos, 0, (int)tx, PreViewBMP.Height);
            }
            else
            {
                float ty = (float)PreViewBMP.ClientRectangle.Height * ratio;
                float pos = ((float)PreViewBMP.ClientRectangle.Height - ty) / 2;
                PreViewBMP.Bounds = new Rectangle(0, (int)pos, PreViewBMP.ClientRectangle.Width, (int)ty);
            }
            //=====make checkerboard

            CheckerBG = new Bitmap(PreViewBMP.ClientRectangle.Width, PreViewBMP.ClientRectangle.Height);
            using (Surface tmp = new Surface(PreViewBMP.ClientRectangle.Width, PreViewBMP.ClientRectangle.Height))
            {
                tmp.ClearWithCheckerboardPattern();
                Bitmap b = new RenderArgs(tmp).Bitmap;
                using (Graphics g = Graphics.FromImage(CheckerBG)) g.DrawImage(b, 0, 0);
            }
            GC.Collect();

            this.Opacity = 1;
            ConvertXY.X = (float)PreViewBMP.BackgroundImage.Width / PreViewBMP.ClientRectangle.Width;
            ConvertXY.Y = (float)PreViewBMP.BackgroundImage.Height / PreViewBMP.ClientRectangle.Height;
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = EffectPlugin.StaticName + " ver. " + version.Major + "." + version.Minor + "." + version.Build;
            KillSicky.AllowAccessibilityShortcutKeys(false);
            timer1.Enabled = true;
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
                DrawGraphics();
            }

        }

        private void PreViewBMP_MouseUp(object sender, MouseEventArgs e)
        {
            if (SWAFlag)
            {
                SWAFlag = false;
                SWA.Checked = false;
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
                DrawGraphics();
                RenderFlag = true;


                for (int i = 0; i < 4; i++)
                {
                    Point thisPoint = getCorner(vCorners[i], tweak[i]);

                    thisPoint = MyUtils.clampP(thisPoint, EffectSourceSurface.Bounds);
                    Corners[i] = thisPoint;

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
                    RenderFlag = true;
                    symmetry(e.Location);
                    DrawGraphics();
                    FinishTokenUpdate();
                }

            }
        }

        private void symmetry(Point e)
        {
            vCorners[CornerSelect] = MyUtils.clampP(e,
                    new Rectangle(0, 0, PreViewBMP.ClientRectangle.Width, PreViewBMP.ClientRectangle.Height));
            Point pmid = MyUtils.Centroid(vCorners);
            int mx = pmid.X + (pmid.X - e.X);
            int my = pmid.Y + (pmid.Y - e.Y);
            if (MirrorX.Checked && MirrorY.Checked)
            {
                int[] sym = { 1, 0, 3, 2 };
                vCorners[sym[CornerSelect]] = new Point(mx, e.Y);
                int[] sym2 = { 3, 2, 1, 0 };
                vCorners[sym2[CornerSelect]] = new Point(e.X, my);
                int[] sym3 = { 2, 3, 0, 1 };
                vCorners[sym3[CornerSelect]] = new Point(mx, my);

            }
            else if (MirrorX.Checked)
            {
                int[] sym = { 1, 0, 3, 2 };
                vCorners[sym[CornerSelect]] = new Point(mx, e.Y);
            }
            else if (MirrorY.Checked)
            {
                int[] sym = { 3, 2, 1, 0 };
                vCorners[sym[CornerSelect]] = new Point(e.X, my);
            }

            for (int i = 0; i < 4; i++) Corners[i]= getCorner(vCorners[i], Point.Empty);
            tweak =new Point[4];

        }

        private void PreViewBMP_MouseMove(object sender, MouseEventArgs e)
        {
            if (SWAFlag)
            {
                anchor.Width = e.X - anchor.X;
                anchor.Height = e.Y - anchor.Y;

                DrawGraphics();
            }
            else if (moveflag)
            {

                symmetry(e.Location);

                DrawGraphics();
                RenderFlag = true;
                FinishTokenUpdate();
            }

        }

        private void DrawGraphics()
        {

            Bitmap tmp = new Bitmap(PreViewBMP.ClientRectangle.Width, PreViewBMP.ClientRectangle.Height);
            Graphics g = Graphics.FromImage(tmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingMode = CompositingMode.SourceOver;
            g.DrawImage(CheckerBG, 0, 0);

            g.DrawImage(PreViewBMP.BackgroundImage, PreViewBMP.ClientRectangle);
            g.DrawRectangle(new Pen(Color.FromArgb(128, 0, 0, 255), 1), anchor);
            g.DrawPolygon(new Pen(Color.FromArgb(128, 255, 0, 0), 3), vCorners);

            //sort points
            Rectangle nAnchor = anchor;
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
            if (SWAFlag) g.DrawRectangle(Pens.Blue, nAnchor);
            //==========================

            for (int i = 0; i < 4; i++)
            {
                if (i == CornerSelect)
                {
                    g.FillEllipse(Brushes.LightBlue, new Rectangle(vCorners[i].X - 5, vCorners[i].Y - 5, 10, 10));
                    g.DrawEllipse(Pens.DarkBlue, new Rectangle(vCorners[i].X - 5, vCorners[i].Y - 5, 10, 10));
                }
                else
                {
                    g.FillEllipse(Brushes.White, new Rectangle(vCorners[i].X - 5, vCorners[i].Y - 5, 10, 10));
                    g.DrawEllipse(Pens.Black, new Rectangle(vCorners[i].X - 5, vCorners[i].Y - 5, 10, 10));
                }
            }

            g.Dispose();
            //dbl bufffer
            Graphics g2 = PreViewBMP.CreateGraphics();
            g2.DrawImage(tmp, 0, 0);
            g2.Dispose();
            tmp.Dispose();
        }

        private void PreViewBMP_Paint(object sender, PaintEventArgs e)
        {
            DrawGraphics();
        }

        private void RstButton_Click(object sender, EventArgs e)
        {
            ReZet(true);

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
            RenderFlag = true;
            PreViewBMP.Invalidate();
            FinishTokenUpdate();
            //test(Corners);

        }

        private Point getTweak(Point C, Point v)
        {
            Point t = new Point((int)(((float)v.X * ConvertXY.X) - (float)C.X),
                (int)(((float)v.Y * ConvertXY.Y) - (float)C.Y));

            return t;
        }
        private Point getVcorner(Point C)
        {
            Point t = new Point((int)((float)C.X / ConvertXY.X),
                             (int)((float)C.Y / ConvertXY.Y));
            return t;
        }
        private Point getCorner(Point v, Point t)
        {
            return new Point((int)((float)v.X * ConvertXY.X + t.X),
                         (int)((float)v.Y * ConvertXY.Y + t.Y));
        }

        private void test(Point[] v)
        {
            //test
            string s = String.Empty;
            foreach (Point i in v) s += " " + i.ToString();
            label1.Text = s;
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

            RenderFlag = true;
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
            Rectangle x = new Rectangle();
            //prelim
            x.Width = y[2].X - y[0].X;
            x.Height = y[2].Y - y[0].Y;
            x.Location = y[0];
            return x;
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
                    Corners[CornerSelect] = MyUtils.clampP(Corners[CornerSelect], EffectSourceSurface.Bounds);
                    vCorners[CornerSelect] = getVcorner(Corners[CornerSelect]);
                    tweak[CornerSelect] = getTweak(Corners[CornerSelect], vCorners[CornerSelect]);

                }
                RenderFlag = true;
                FinishTokenUpdate();
                DrawGraphics();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (closeform)
            {
                timer1.Enabled = false;
                KillSicky.AllowAccessibilityShortcutKeys(true);
                this.Close();
            }
            else if (passClass.StringID == "1")
            {
                Busy.ForeColor = Color.Red;
            }
            else
            {
                Busy.ForeColor = Color.DarkGray;
            }

        }

        private void EffectPluginConfigDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer1.Enabled)
            {
                closeform = true;
                e.Cancel = true;
            }

        }

        private void SeeThru_CheckedChanged(object sender, EventArgs e)
        {
            if (SeeThru.Checked)
            {
                this.Opacity = .4;
            }
            else
            {
                this.Opacity = 1;
            }
        }

        private void EffectPluginConfigDialog_Deactivate(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void EffectPluginConfigDialog_Activated(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void SWA_CheckedChanged(object sender, EventArgs e)
        {
            SWAFlag = false;
            if (SWA.Checked) RstButton_Click(sender, e);
        }

        private void Axis_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.EndScroll)
            {
                UVal.Text = "U Value " + UAxis.Value.ToString() + "%";
                VVal.Text = "V Value " + VAxis.Value.ToString() + "%";
                RenderFlag = true;
                FinishTokenUpdate();
            }
        }

        private void Axis_ValueChanged(object sender, EventArgs e)
        {
            UVal.Text = "U Value " + UAxis.Value.ToString() + "%";
            VVal.Text = "V Value " + VAxis.Value.ToString() + "%";
        }

        private void cParam_Leave(object sender, EventArgs e)
        {
            cParam.Visible = false;
        }

        private void cParam_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;


            foreach (Keys k in keyCheck)
            {
                if (e.KeyCode.Equals(k))
                {
                    nonNumberEntered = false;
                    return;
                }
            }
            nonNumberEntered = true;

        }

        private void cParam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered) e.Handled = true;
        }

        private void cParam_TextChanged(object sender, EventArgs e)
        {

        }

        private void cParam_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {

                string[] s = cParam.Text.Split(' ');
                int x, y;
                if (s.Length > 1 && Int32.TryParse(s[0], out x) && Int32.TryParse(s[1], out y))
                {
                    Point p = new Point(x, y);
                    if (EffectSourceSurface.Bounds.Contains(p))
                    {
                        Corners[CornerSelect] = p;
                        vCorners[CornerSelect] = getVcorner(Corners[CornerSelect]);
                        tweak[CornerSelect] = getTweak(Corners[CornerSelect], vCorners[CornerSelect]);
                        RenderFlag = true;
                        FinishTokenUpdate();
                    }
                }
                cParam.Visible = false;
            }

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