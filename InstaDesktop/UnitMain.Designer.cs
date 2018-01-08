namespace InstaDesktop
{
    partial class UnitMain
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.InputImage = new System.Windows.Forms.PictureBox();
            this.ApplyBtn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.ProgressPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.FiltersList = new System.Windows.Forms.ComboBox();
            this.FileInfoEdit = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InputImage)).BeginInit();
            this.ProgressPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.InputImage);
            this.panel1.Location = new System.Drawing.Point(11, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(716, 462);
            this.panel1.TabIndex = 0;
            // 
            // InputImage
            // 
            this.InputImage.BackColor = System.Drawing.SystemColors.Control;
            this.InputImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InputImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputImage.Location = new System.Drawing.Point(0, 0);
            this.InputImage.Name = "InputImage";
            this.InputImage.Size = new System.Drawing.Size(714, 460);
            this.InputImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.InputImage.TabIndex = 0;
            this.InputImage.TabStop = false;
            this.InputImage.Click += new System.EventHandler(this.InputImage_Click);
            // 
            // ApplyBtn
            // 
            this.ApplyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ApplyBtn.Location = new System.Drawing.Point(733, 466);
            this.ApplyBtn.Name = "ApplyBtn";
            this.ApplyBtn.Size = new System.Drawing.Size(231, 21);
            this.ApplyBtn.TabIndex = 2;
            this.ApplyBtn.Text = "Save";
            this.ApplyBtn.UseVisualStyleBackColor = true;
            this.ApplyBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*.jpg";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Jpeg Files|*.jpg;*.jpeg|Png Files|*.png";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Jpeg Files|*.jpg;*.jpeg|Png Files|*.png";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Input image (click to open):";
            // 
            // ProgressPanel
            // 
            this.ProgressPanel.Controls.Add(this.label4);
            this.ProgressPanel.Controls.Add(this.progressBar1);
            this.ProgressPanel.Location = new System.Drawing.Point(399, 107);
            this.ProgressPanel.Name = "ProgressPanel";
            this.ProgressPanel.Size = new System.Drawing.Size(200, 100);
            this.ProgressPanel.TabIndex = 7;
            this.ProgressPanel.Visible = false;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 87);
            this.label4.TabIndex = 1;
            this.label4.Text = "Please wait...";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 87);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(200, 13);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(64, 64);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FiltersList
            // 
            this.FiltersList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.FiltersList.DropDownHeight = 400;
            this.FiltersList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FiltersList.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FiltersList.FormattingEnabled = true;
            this.FiltersList.IntegralHeight = false;
            this.FiltersList.Location = new System.Drawing.Point(733, 397);
            this.FiltersList.Name = "FiltersList";
            this.FiltersList.Size = new System.Drawing.Size(231, 63);
            this.FiltersList.TabIndex = 10;
            this.FiltersList.SelectedIndexChanged += new System.EventHandler(this.FiltersList_SelectedIndexChanged);
            // 
            // FileInfoEdit
            // 
            this.FileInfoEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileInfoEdit.Location = new System.Drawing.Point(733, 26);
            this.FileInfoEdit.Multiline = true;
            this.FileInfoEdit.Name = "FileInfoEdit";
            this.FileInfoEdit.ReadOnly = true;
            this.FileInfoEdit.Size = new System.Drawing.Size(231, 365);
            this.FileInfoEdit.TabIndex = 11;
            // 
            // UnitMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 499);
            this.Controls.Add(this.FileInfoEdit);
            this.Controls.Add(this.FiltersList);
            this.Controls.Add(this.ProgressPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ApplyBtn);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "UnitMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InstaDesktop";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnitMain_FormClosing);
            this.Load += new System.EventHandler(this.UnitMain_Load);
            this.Shown += new System.EventHandler(this.UnitMain_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.InputImage)).EndInit();
            this.ProgressPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox InputImage;
        private System.Windows.Forms.Button ApplyBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel ProgressPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox FiltersList;
        private System.Windows.Forms.TextBox FileInfoEdit;
    }
}

