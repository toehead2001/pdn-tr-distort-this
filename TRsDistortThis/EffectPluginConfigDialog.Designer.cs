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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.PreViewBMP = new System.Windows.Forms.PictureBox();
            this.cParam = new System.Windows.Forms.TextBox();
            this.AlphaBox = new System.Windows.Forms.CheckBox();
            this.PerspBox = new System.Windows.Forms.CheckBox();
            this.RstButton = new System.Windows.Forms.Button();
            this.SeeThru = new System.Windows.Forms.CheckBox();
            this.SWA = new System.Windows.Forms.CheckBox();
            this.UAxis = new System.Windows.Forms.HScrollBar();
            this.VAxis = new System.Windows.Forms.HScrollBar();
            this.UVal = new System.Windows.Forms.Label();
            this.VVal = new System.Windows.Forms.Label();
            this.MirrorX = new System.Windows.Forms.CheckBox();
            this.MirrorY = new System.Windows.Forms.CheckBox();
            this.ResetNubsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PreViewBMP)).BeginInit();
            this.PreViewBMP.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(497, 377);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 30);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "Cancel";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(416, 377);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 30);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.TabStop = false;
            this.buttonOK.Text = "OK";
            // 
            // PreViewBMP
            // 
            this.PreViewBMP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PreViewBMP.Controls.Add(this.cParam);
            this.PreViewBMP.Location = new System.Drawing.Point(5, 5);
            this.PreViewBMP.Name = "PreViewBMP";
            this.PreViewBMP.Size = new System.Drawing.Size(402, 402);
            this.PreViewBMP.TabIndex = 3;
            this.PreViewBMP.TabStop = false;
            this.PreViewBMP.Paint += new System.Windows.Forms.PaintEventHandler(this.PreViewBMP_Paint);
            this.PreViewBMP.DoubleClick += new System.EventHandler(this.PreViewBMP_DoubleClick);
            this.PreViewBMP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PreViewBMP_MouseDown);
            this.PreViewBMP.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PreViewBMP_MouseMove);
            this.PreViewBMP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PreViewBMP_MouseUp);
            // 
            // cParam
            // 
            this.cParam.Location = new System.Drawing.Point(150, 190);
            this.cParam.Name = "cParam";
            this.cParam.Size = new System.Drawing.Size(100, 20);
            this.cParam.TabIndex = 0;
            this.cParam.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cParam.Visible = false;
            this.cParam.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cParam_KeyDown);
            this.cParam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cParam_KeyPress);
            this.cParam.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cParam_KeyUp);
            this.cParam.Leave += new System.EventHandler(this.cParam_Leave);
            // 
            // AlphaBox
            // 
            this.AlphaBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AlphaBox.AutoSize = true;
            this.AlphaBox.Checked = true;
            this.AlphaBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AlphaBox.Location = new System.Drawing.Point(416, 12);
            this.AlphaBox.Name = "AlphaBox";
            this.AlphaBox.Size = new System.Drawing.Size(113, 17);
            this.AlphaBox.TabIndex = 4;
            this.AlphaBox.Text = "Alpha Transparent";
            this.AlphaBox.CheckedChanged += new System.EventHandler(this.Box_CheckedChanged);
            // 
            // PerspBox
            // 
            this.PerspBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PerspBox.AutoSize = true;
            this.PerspBox.Checked = true;
            this.PerspBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PerspBox.Location = new System.Drawing.Point(416, 50);
            this.PerspBox.Name = "PerspBox";
            this.PerspBox.Size = new System.Drawing.Size(118, 17);
            this.PerspBox.TabIndex = 5;
            this.PerspBox.TabStop = false;
            this.PerspBox.Text = "Forced Perspective";
            this.PerspBox.CheckedChanged += new System.EventHandler(this.Box_CheckedChanged);
            // 
            // RstButton
            // 
            this.RstButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RstButton.Location = new System.Drawing.Point(416, 176);
            this.RstButton.Name = "RstButton";
            this.RstButton.Size = new System.Drawing.Size(156, 30);
            this.RstButton.TabIndex = 8;
            this.RstButton.TabStop = false;
            this.RstButton.Text = "Reset Nubs and Work Area";
            this.RstButton.Click += new System.EventHandler(this.RstButton_Click);
            // 
            // SeeThru
            // 
            this.SeeThru.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SeeThru.AutoSize = true;
            this.SeeThru.Location = new System.Drawing.Point(416, 347);
            this.SeeThru.Name = "SeeThru";
            this.SeeThru.Size = new System.Drawing.Size(67, 17);
            this.SeeThru.TabIndex = 11;
            this.SeeThru.TabStop = false;
            this.SeeThru.Text = "SeeThru";
            this.SeeThru.CheckedChanged += new System.EventHandler(this.SeeThru_CheckedChanged);
            // 
            // SWA
            // 
            this.SWA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SWA.AutoSize = true;
            this.SWA.Location = new System.Drawing.Point(416, 212);
            this.SWA.Name = "SWA";
            this.SWA.Size = new System.Drawing.Size(96, 17);
            this.SWA.TabIndex = 5;
            this.SWA.TabStop = false;
            this.SWA.Text = "Set Work Area";
            this.SWA.CheckedChanged += new System.EventHandler(this.SWA_CheckedChanged);
            // 
            // UAxis
            // 
            this.UAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UAxis.LargeChange = 1;
            this.UAxis.Location = new System.Drawing.Point(417, 91);
            this.UAxis.Name = "UAxis";
            this.UAxis.Size = new System.Drawing.Size(150, 17);
            this.UAxis.TabIndex = 12;
            this.UAxis.Value = 100;
            this.UAxis.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Axis_Scroll);
            this.UAxis.ValueChanged += new System.EventHandler(this.Axis_ValueChanged);
            // 
            // VAxis
            // 
            this.VAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VAxis.LargeChange = 1;
            this.VAxis.Location = new System.Drawing.Point(417, 139);
            this.VAxis.Name = "VAxis";
            this.VAxis.Size = new System.Drawing.Size(150, 17);
            this.VAxis.TabIndex = 12;
            this.VAxis.Value = 100;
            this.VAxis.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Axis_Scroll);
            this.VAxis.ValueChanged += new System.EventHandler(this.Axis_ValueChanged);
            // 
            // UVal
            // 
            this.UVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UVal.AutoSize = true;
            this.UVal.Location = new System.Drawing.Point(417, 73);
            this.UVal.Name = "UVal";
            this.UVal.Size = new System.Drawing.Size(74, 13);
            this.UVal.TabIndex = 13;
            this.UVal.Text = "U Value 100%";
            // 
            // VVal
            // 
            this.VVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VVal.AutoSize = true;
            this.VVal.Location = new System.Drawing.Point(420, 121);
            this.VVal.Name = "VVal";
            this.VVal.Size = new System.Drawing.Size(73, 13);
            this.VVal.TabIndex = 13;
            this.VVal.Text = "V Value 100%";
            // 
            // MirrorX
            // 
            this.MirrorX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MirrorX.AutoSize = true;
            this.MirrorX.Location = new System.Drawing.Point(416, 281);
            this.MirrorX.Name = "MirrorX";
            this.MirrorX.Size = new System.Drawing.Size(62, 17);
            this.MirrorX.TabIndex = 5;
            this.MirrorX.TabStop = false;
            this.MirrorX.Text = "Mirror X";
            // 
            // MirrorY
            // 
            this.MirrorY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MirrorY.AutoSize = true;
            this.MirrorY.Location = new System.Drawing.Point(416, 304);
            this.MirrorY.Name = "MirrorY";
            this.MirrorY.Size = new System.Drawing.Size(62, 17);
            this.MirrorY.TabIndex = 5;
            this.MirrorY.TabStop = false;
            this.MirrorY.Text = "Mirror Y";
            // 
            // ResetNubsButton
            // 
            this.ResetNubsButton.Location = new System.Drawing.Point(416, 245);
            this.ResetNubsButton.Name = "ResetNubsButton";
            this.ResetNubsButton.Size = new System.Drawing.Size(75, 30);
            this.ResetNubsButton.TabIndex = 14;
            this.ResetNubsButton.Text = "Reset Nubs";
            this.ResetNubsButton.UseVisualStyleBackColor = true;
            this.ResetNubsButton.Click += new System.EventHandler(this.ResetNubsButton_Click);
            // 
            // EffectPluginConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(584, 412);
            this.Controls.Add(this.ResetNubsButton);
            this.Controls.Add(this.VVal);
            this.Controls.Add(this.UVal);
            this.Controls.Add(this.VAxis);
            this.Controls.Add(this.UAxis);
            this.Controls.Add(this.SeeThru);
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
            this.Load += new System.EventHandler(this.EffectPluginConfigDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PreViewBMP)).EndInit();
            this.PreViewBMP.ResumeLayout(false);
            this.PreViewBMP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.PictureBox PreViewBMP;
        private System.Windows.Forms.CheckBox AlphaBox;
        private System.Windows.Forms.CheckBox PerspBox;
        private System.Windows.Forms.Button RstButton;
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
        private System.Windows.Forms.Button ResetNubsButton;
    }
}