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
            this.mail_linkLabel = new System.Windows.Forms.LinkLabel();
            this.author_label = new System.Windows.Forms.Label();
            this.version_label = new System.Windows.Forms.Label();
            this.version_richTextBox = new System.Windows.Forms.RichTextBox();
            this.help_groupBox = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.help_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // back_button
            // 
            this.back_button.Location = new System.Drawing.Point(134, 383);
            this.back_button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.back_button.Name = "back_button";
            this.back_button.Size = new System.Drawing.Size(75, 26);
            this.back_button.TabIndex = 0;
            this.back_button.Text = "OK";
            this.back_button.UseVisualStyleBackColor = true;
            this.back_button.Click += new System.EventHandler(this.back_button_Click);
            // 
            // mail_linkLabel
            // 
            this.mail_linkLabel.AutoSize = true;
            this.mail_linkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mail_linkLabel.Location = new System.Drawing.Point(60, 66);
            this.mail_linkLabel.Name = "mail_linkLabel";
            this.mail_linkLabel.Size = new System.Drawing.Size(83, 17);
            this.mail_linkLabel.TabIndex = 2;
            this.mail_linkLabel.TabStop = true;
            this.mail_linkLabel.Text = "Minpu Kang";
            this.mail_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.mail_linkLabel_LinkClicked);
            // 
            // author_label
            // 
            this.author_label.AutoSize = true;
            this.author_label.Location = new System.Drawing.Point(9, 70);
            this.author_label.Name = "author_label";
            this.author_label.Size = new System.Drawing.Size(41, 13);
            this.author_label.TabIndex = 3;
            this.author_label.Text = "Author:";
            // 
            // version_label
            // 
            this.version_label.AutoSize = true;
            this.version_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.version_label.ForeColor = System.Drawing.Color.Black;
            this.version_label.Location = new System.Drawing.Point(61, 14);
            this.version_label.Name = "version_label";
            this.version_label.Size = new System.Drawing.Size(139, 13);
            this.version_label.TabIndex = 4;
            this.version_label.Text = "DNS Config Advanced v5.1";
            // 
            // version_richTextBox
            // 
            this.version_richTextBox.BackColor = System.Drawing.Color.LightBlue;
            this.version_richTextBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.version_richTextBox.Font = new System.Drawing.Font("Microsoft New Tai Lue", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.version_richTextBox.Location = new System.Drawing.Point(6, 16);
            this.version_richTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.version_richTextBox.Name = "version_richTextBox";
            this.version_richTextBox.Size = new System.Drawing.Size(311, 260);
            this.version_richTextBox.TabIndex = 0;
            this.version_richTextBox.Text = resources.GetString("version_richTextBox.Text");
            // 
            // help_groupBox
            // 
            this.help_groupBox.Controls.Add(this.version_richTextBox);
            this.help_groupBox.Location = new System.Drawing.Point(12, 91);
            this.help_groupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.help_groupBox.Name = "help_groupBox";
            this.help_groupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.help_groupBox.Size = new System.Drawing.Size(324, 283);
            this.help_groupBox.TabIndex = 5;
            this.help_groupBox.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // about
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(346, 422);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.help_groupBox);
            this.Controls.Add(this.version_label);
            this.Controls.Add(this.author_label);
            this.Controls.Add(this.mail_linkLabel);
            this.Controls.Add(this.back_button);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "about";
            this.Text = "About";
            this.help_groupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button back_button;
        private System.Windows.Forms.LinkLabel mail_linkLabel;
        private System.Windows.Forms.Label author_label;
        private System.Windows.Forms.Label version_label;
        private System.Windows.Forms.GroupBox help_groupBox;
        private System.Windows.Forms.RichTextBox version_richTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}