namespace TRsDistortThis
{
    partial class EffectPluginConfigDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(416, 365);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 37);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonOK.Location = new System.Drawing.Point(416, 410);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(72, 40);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.TabStop = false;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // PreViewBMP
            // 
            this.PreViewBMP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PreViewBMP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PreViewBMP.Controls.Add(this.cParam);
            this.PreViewBMP.Location = new System.Drawing.Point(0, 0);
            this.PreViewBMP.Name = "PreViewBMP";
            this.PreViewBMP.Size = new System.Drawing.Size(402, 402);
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
            this.PerspBox.CheckedChanged += new System.EventHandler(this.Box_CheckedChanged);
            // 
            // RstButton
            // 
            this.RstButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.RstButton.Location = new System.Drawing.Point(417, 12);
            this.RstButton.Name = "RstButton";
            this.RstButton.Size = new System.Drawing.Size(72, 36);
            this.RstButton.TabIndex = 8;
            this.RstButton.TabStop = false;
            this.RstButton.Text = "Reset";
            this.RstButton.Click += new System.EventHandler(this.RstButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 411);
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
            this.Busy.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.SWA.CheckedChanged += new System.EventHandler(this.SWA_CheckedChanged);
            // 
            // UAxis
            // 
            this.UAxis.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.UAxis.LargeChange = 1;
            this.UAxis.Location = new System.Drawing.Point(412, 136);
            this.UAxis.Name = "UAxis";
            this.UAxis.Size = new System.Drawing.Size(78, 17);
            this.UAxis.TabIndex = 12;
            this.UAxis.Value = 100;
            this.UAxis.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Axis_Scroll);
            this.UAxis.ValueChanged += new System.EventHandler(this.Axis_ValueChanged);
            // 
            // VAxis
            // 
            this.VAxis.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.VAxis.LargeChange = 1;
            this.VAxis.Location = new System.Drawing.Point(412, 172);
            this.VAxis.Name = "VAxis";
            this.VAxis.Size = new System.Drawing.Size(78, 17);
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
            this.VVal.Location = new System.Drawing.Point(415, 159);
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
            // 
            // EffectPluginConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(496, 462);
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
            this.MinimizeBox = true;
            this.Name = "EffectPluginConfigDialog";
            this.Activated += new System.EventHandler(this.EffectPluginConfigDialog_Activated);
            this.Deactivate += new System.EventHandler(this.EffectPluginConfigDialog_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EffectPluginConfigDialog_FormClosing);
            this.Load += new System.EventHandler(this.EffectPluginConfigDialog_Load);
            this.PreViewBMP.ResumeLayout(false);
            this.PreViewBMP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel PreViewBMP;
        private System.Windows.Forms.CheckBox AlphaBox;
        private System.Windows.Forms.CheckBox PerspBox;
        private System.Windows.Forms.Button RstButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Busy;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox SeeThru;
        private System.Windows.Forms.CheckBox SWA;
        private System.Windows.Forms.HScrollBar UAxis;
        private System.Windows.Forms.HScrollBar VAxis;
        private System.Windows.Forms.Label UVal;
        private System.Windows.Forms.Label VVal;
        private System.Windows.Forms.CheckBox MirrorX;
        private System.Windows.Forms.CheckBox MirrorY;
        private System.Windows.Forms.TextBox cParam;
        private System.Windows.Forms.Button buttonCancel;
    }
}