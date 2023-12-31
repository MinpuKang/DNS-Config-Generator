namespace DNS_Config
{
    partial class about
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(about));
            this.back_button = new System.Windows.Forms.Button();
            this.author_linkLabel = new System.Windows.Forms.LinkLabel();
            this.author_label = new System.Windows.Forms.Label();
            this.version_label = new System.Windows.Forms.Label();
            this.version_richTextBox = new System.Windows.Forms.RichTextBox();
            this.pictureBox_icon = new System.Windows.Forms.PictureBox();
            this.linkLabel_issue_report = new System.Windows.Forms.LinkLabel();
            this.label_issue_report = new System.Windows.Forms.Label();
            this.pictureBox_qiheyehk = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_icon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_qiheyehk)).BeginInit();
            this.SuspendLayout();
            // 
            // back_button
            // 
            this.back_button.Location = new System.Drawing.Point(176, 532);
            this.back_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.back_button.Name = "back_button";
            this.back_button.Size = new System.Drawing.Size(75, 26);
            this.back_button.TabIndex = 0;
            this.back_button.Text = "OK";
            this.back_button.UseVisualStyleBackColor = true;
            this.back_button.Click += new System.EventHandler(this.back_button_Click);
            // 
            // author_linkLabel
            // 
            this.author_linkLabel.AutoSize = true;
            this.author_linkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.author_linkLabel.Location = new System.Drawing.Point(173, 50);
            this.author_linkLabel.Name = "author_linkLabel";
            this.author_linkLabel.Size = new System.Drawing.Size(83, 17);
            this.author_linkLabel.TabIndex = 2;
            this.author_linkLabel.TabStop = true;
            this.author_linkLabel.Text = "Minpu Kang";
            this.author_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.author_linkLabel_LinkClicked);
            // 
            // author_label
            // 
            this.author_label.AutoSize = true;
            this.author_label.Location = new System.Drawing.Point(121, 50);
            this.author_label.Name = "author_label";
            this.author_label.Size = new System.Drawing.Size(54, 17);
            this.author_label.TabIndex = 3;
            this.author_label.Text = "Author:";
            // 
            // version_label
            // 
            this.version_label.AutoSize = true;
            this.version_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.version_label.ForeColor = System.Drawing.Color.Black;
            this.version_label.Location = new System.Drawing.Point(113, 12);
            this.version_label.Name = "version_label";
            this.version_label.Size = new System.Drawing.Size(199, 17);
            this.version_label.TabIndex = 4;
            this.version_label.Text = "DNS Config Public Version 7.1";
            // 
            // version_richTextBox
            // 
            this.version_richTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.version_richTextBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.version_richTextBox.Font = new System.Drawing.Font("Microsoft New Tai Lue", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.version_richTextBox.Location = new System.Drawing.Point(7, 130);
            this.version_richTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.version_richTextBox.Name = "version_richTextBox";
            this.version_richTextBox.Size = new System.Drawing.Size(466, 394);
            this.version_richTextBox.TabIndex = 0;
            this.version_richTextBox.Text = resources.GetString("version_richTextBox.Text");
            // 
            // pictureBox_icon
            // 
            this.pictureBox_icon.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_icon.ErrorImage")));
            this.pictureBox_icon.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_icon.Image")));
            this.pictureBox_icon.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_icon.InitialImage")));
            this.pictureBox_icon.Location = new System.Drawing.Point(7, 12);
            this.pictureBox_icon.Name = "pictureBox_icon";
            this.pictureBox_icon.Size = new System.Drawing.Size(90, 83);
            this.pictureBox_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_icon.TabIndex = 6;
            this.pictureBox_icon.TabStop = false;
            // 
            // linkLabel_issue_report
            // 
            this.linkLabel_issue_report.AutoSize = true;
            this.linkLabel_issue_report.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel_issue_report.Location = new System.Drawing.Point(173, 87);
            this.linkLabel_issue_report.Name = "linkLabel_issue_report";
            this.linkLabel_issue_report.Size = new System.Drawing.Size(150, 17);
            this.linkLabel_issue_report.TabIndex = 8;
            this.linkLabel_issue_report.TabStop = true;
            this.linkLabel_issue_report.Text = "Issue Raised in Github";
            this.linkLabel_issue_report.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_issue_report_LinkClicked);
            // 
            // label_issue_report
            // 
            this.label_issue_report.AutoSize = true;
            this.label_issue_report.Location = new System.Drawing.Point(83, 87);
            this.label_issue_report.Name = "label_issue_report";
            this.label_issue_report.Size = new System.Drawing.Size(92, 17);
            this.label_issue_report.TabIndex = 9;
            this.label_issue_report.Text = "Issue Report:";
            // 
            // pictureBox_qiheyehk
            // 
            this.pictureBox_qiheyehk.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_qiheyehk.ErrorImage")));
            this.pictureBox_qiheyehk.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_qiheyehk.Image")));
            this.pictureBox_qiheyehk.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_qiheyehk.InitialImage")));
            this.pictureBox_qiheyehk.Location = new System.Drawing.Point(334, 12);
            this.pictureBox_qiheyehk.Name = "pictureBox_qiheyehk";
            this.pictureBox_qiheyehk.Size = new System.Drawing.Size(126, 103);
            this.pictureBox_qiheyehk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_qiheyehk.TabIndex = 10;
            this.pictureBox_qiheyehk.TabStop = false;
            // 
            // about
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 562);
            this.Controls.Add(this.pictureBox_qiheyehk);
            this.Controls.Add(this.label_issue_report);
            this.Controls.Add(this.linkLabel_issue_report);
            this.Controls.Add(this.version_richTextBox);
            this.Controls.Add(this.pictureBox_icon);
            this.Controls.Add(this.version_label);
            this.Controls.Add(this.author_label);
            this.Controls.Add(this.author_linkLabel);
            this.Controls.Add(this.back_button);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "about";
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_icon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_qiheyehk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button back_button;
        private System.Windows.Forms.LinkLabel author_linkLabel;
        private System.Windows.Forms.Label author_label;
        private System.Windows.Forms.Label version_label;
        private System.Windows.Forms.RichTextBox version_richTextBox;
        private System.Windows.Forms.PictureBox pictureBox_icon;
        private System.Windows.Forms.LinkLabel linkLabel_issue_report;
        private System.Windows.Forms.Label label_issue_report;
        private System.Windows.Forms.PictureBox pictureBox_qiheyehk;
    }
}