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
            this.UVal = new System.Windows.Forms.Label();
            this.VVal = new System.Windows.Forms.Label();
            this.MirrorX = new System.Windows.Forms.CheckBox();
            this.MirrorY = new System.Windows.Forms.CheckBox();
            this.ResetNubsButton = new System.Windows.Forms.Button();
            this.UAxis = new System.Windows.Forms.TrackBar();
            this.VAxis = new System.Windows.Forms.TrackBar();
            this.AaTrack = new System.Windows.Forms.TrackBar();
            this.AaLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PreViewBMP)).BeginInit();
            this.PreViewBMP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UAxis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VAxis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AaTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonCancel.Location = new System.Drawing.Point(597, 470);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 30);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.TabStop = false;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonOK.Location = new System.Drawing.Point(516, 470);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 30);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.TabStop = false;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // PreViewBMP
            // 
            this.PreViewBMP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PreViewBMP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PreViewBMP.Controls.Add(this.cParam);
            this.PreViewBMP.Location = new System.Drawing.Point(5, 5);
            this.PreViewBMP.Name = "PreViewBMP";
            this.PreViewBMP.Size = new System.Drawing.Size(502, 502);
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
            this.cParam.Location = new System.Drawing.Point(200, 240);
            this.cParam.Name = "cParam";
            this.cParam.Size = new System.Drawing.Size(100, 23);
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
            this.AlphaBox.Location = new System.Drawing.Point(516, 12);
            this.AlphaBox.Name = "AlphaBox";
            this.AlphaBox.Size = new System.Drawing.Size(121, 19);
            this.AlphaBox.TabIndex = 4;
            this.AlphaBox.Text = "Alpha Transparent";
            this.AlphaBox.CheckedChanged += new System.EventHandler(this.AlphaBox_CheckedChanged);
            // 
            // PerspBox
            // 
            this.PerspBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PerspBox.AutoSize = true;
            this.PerspBox.Checked = true;
            this.PerspBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PerspBox.Location = new System.Drawing.Point(516, 117);
            this.PerspBox.Name = "PerspBox";
            this.PerspBox.Size = new System.Drawing.Size(125, 19);
            this.PerspBox.TabIndex = 5;
            this.PerspBox.TabStop = false;
            this.PerspBox.Text = "Forced Perspective";
            this.PerspBox.CheckedChanged += new System.EventHandler(this.PerspBox_CheckedChanged);
            // 
            // RstButton
            // 
            this.RstButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RstButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.RstButton.Location = new System.Drawing.Point(516, 255);
            this.RstButton.Name = "RstButton";
            this.RstButton.Size = new System.Drawing.Size(156, 30);
            this.RstButton.TabIndex = 8;
            this.RstButton.TabStop = false;
            this.RstButton.Text = "Reset Nubs and Work Area";
            this.RstButton.UseVisualStyleBackColor = true;
            this.RstButton.Click += new System.EventHandler(this.RstButton_Click);
            // 
            // SeeThru
            // 
            this.SeeThru.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SeeThru.AutoSize = true;
            this.SeeThru.Location = new System.Drawing.Point(516, 445);
            this.SeeThru.Name = "SeeThru";
            this.SeeThru.Size = new System.Drawing.Size(133, 19);
            this.SeeThru.TabIndex = 11;
            this.SeeThru.TabStop = false;
            this.SeeThru.Text = "Translucent Window";
            this.SeeThru.CheckedChanged += new System.EventHandler(this.SeeThru_CheckedChanged);
            // 
            // SWA
            // 
            this.SWA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SWA.AutoSize = true;
            this.SWA.Location = new System.Drawing.Point(516, 291);
            this.SWA.Name = "SWA";
            this.SWA.Size = new System.Drawing.Size(100, 19);
            this.SWA.TabIndex = 5;
            this.SWA.TabStop = false;
            this.SWA.Text = "Set Work Area";
            this.SWA.CheckedChanged += new System.EventHandler(this.SWA_CheckedChanged);
            // 
            // UVal
            // 
            this.UVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UVal.AutoSize = true;
            this.UVal.Location = new System.Drawing.Point(527, 140);
            this.UVal.Name = "UVal";
            this.UVal.Size = new System.Drawing.Size(77, 15);
            this.UVal.TabIndex = 13;
            this.UVal.Text = "U Value 100%";
            // 
            // VVal
            // 
            this.VVal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VVal.AutoSize = true;
            this.VVal.Location = new System.Drawing.Point(527, 185);
            this.VVal.Name = "VVal";
            this.VVal.Size = new System.Drawing.Size(76, 15);
            this.VVal.TabIndex = 13;
            this.VVal.Text = "V Value 100%";
            // 
            // MirrorX
            // 
            this.MirrorX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MirrorX.AutoSize = true;
            this.MirrorX.Location = new System.Drawing.Point(516, 379);
            this.MirrorX.Name = "MirrorX";
            this.MirrorX.Size = new System.Drawing.Size(69, 19);
            this.MirrorX.TabIndex = 5;
            this.MirrorX.TabStop = false;
            this.MirrorX.Text = "Mirror X";
            // 
            // MirrorY
            // 
            this.MirrorY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MirrorY.AutoSize = true;
            this.MirrorY.Location = new System.Drawing.Point(516, 402);
            this.MirrorY.Name = "MirrorY";
            this.MirrorY.Size = new System.Drawing.Size(69, 19);
            this.MirrorY.TabIndex = 5;
            this.MirrorY.TabStop = false;
            this.MirrorY.Text = "Mirror Y";
            // 
            // ResetNubsButton
            // 
            this.ResetNubsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetNubsButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ResetNubsButton.Location = new System.Drawing.Point(516, 316);
            this.ResetNubsButton.Name = "ResetNubsButton";
            this.ResetNubsButton.Size = new System.Drawing.Size(75, 30);
            this.ResetNubsButton.TabIndex = 14;
            this.ResetNubsButton.Text = "Reset Nubs";
            this.ResetNubsButton.UseVisualStyleBackColor = true;
            this.ResetNubsButton.Click += new System.EventHandler(this.ResetNubsButton_Click);
            // 
            // UAxis
            // 
            this.UAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UAxis.AutoSize = false;
            this.UAxis.LargeChange = 1;
            this.UAxis.Location = new System.Drawing.Point(516, 156);
            this.UAxis.Maximum = 100;
            this.UAxis.Name = "UAxis";
            this.UAxis.Size = new System.Drawing.Size(158, 24);
            this.UAxis.TabIndex = 15;
            this.UAxis.TickStyle = System.Windows.Forms.TickStyle.None;
            this.UAxis.Value = 100;
            this.UAxis.Scroll += new System.EventHandler(this.UAxis_Scroll);
            // 
            // VAxis
            // 
            this.VAxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.VAxis.AutoSize = false;
            this.VAxis.LargeChange = 1;
            this.VAxis.Location = new System.Drawing.Point(516, 201);
            this.VAxis.Maximum = 100;
            this.VAxis.Name = "VAxis";
            this.VAxis.Size = new System.Drawing.Size(158, 24);
            this.VAxis.TabIndex = 16;
            this.VAxis.TickStyle = System.Windows.Forms.TickStyle.None;
            this.VAxis.Value = 100;
            this.VAxis.Scroll += new System.EventHandler(this.VAxis_Scroll);
            // 
            // AaTrack
            // 
            this.AaTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AaTrack.AutoSize = false;
            this.AaTrack.LargeChange = 1;
            this.AaTrack.Location = new System.Drawing.Point(516, 64);
            this.AaTrack.Maximum = 6;
            this.AaTrack.Minimum = 1;
            this.AaTrack.Name = "AaTrack";
            this.AaTrack.Size = new System.Drawing.Size(158, 24);
            this.AaTrack.TabIndex = 17;
            this.AaTrack.TickStyle = System.Windows.Forms.TickStyle.None;
            this.AaTrack.Value = 1;
            this.AaTrack.Scroll += new System.EventHandler(this.AaLevel_Scroll);
            // 
            // AaLabel
            // 
            this.AaLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AaLabel.AutoSize = true;
            this.AaLabel.Location = new System.Drawing.Point(519, 49);
            this.AaLabel.Name = "AaLabel";
            this.AaLabel.Size = new System.Drawing.Size(69, 15);
            this.AaLabel.TabIndex = 18;
            this.AaLabel.Text = "Antialiasing";
            // 
            // EffectPluginConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(684, 512);
            this.Controls.Add(this.AaLabel);
            this.Controls.Add(this.AaTrack);
            this.Controls.Add(this.VAxis);
            this.Controls.Add(this.UAxis);
            this.Controls.Add(this.ResetNubsButton);
            this.Controls.Add(this.VVal);
            this.Controls.Add(this.UVal);
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
            this.ForeColor = System.Drawing.Color.Black;
            this.HelpButton = true;
            this.KeyPreview = true;
            this.Name = "EffectPluginConfigDialog";
            this.UseAppThemeColors = true;
            ((System.ComponentModel.ISupportInitialize)(this.PreViewBMP)).EndInit();
            this.PreViewBMP.ResumeLayout(false);
            this.PreViewBMP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UAxis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VAxis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AaTrack)).EndInit();
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
        private System.Windows.Forms.Label UVal;
        private System.Windows.Forms.Label VVal;
        private System.Windows.Forms.CheckBox MirrorX;
        private System.Windows.Forms.CheckBox MirrorY;
        private System.Windows.Forms.TextBox cParam;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button ResetNubsButton;
        private System.Windows.Forms.TrackBar UAxis;
        private System.Windows.Forms.TrackBar VAxis;
        private System.Windows.Forms.TrackBar AaTrack;
        private System.Windows.Forms.Label AaLabel;
    }
}