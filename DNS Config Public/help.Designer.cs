namespace DNS_Config
{
    partial class help
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(help));
            this.back_button = new System.Windows.Forms.Button();
            this.accounce_richTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // back_button
            // 
            this.back_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.back_button.Location = new System.Drawing.Point(207, 478);
            this.back_button.Name = "back_button";
            this.back_button.Size = new System.Drawing.Size(75, 27);
            this.back_button.TabIndex = 0;
            this.back_button.Text = "OK";
            this.back_button.UseVisualStyleBackColor = true;
            this.back_button.Click += new System.EventHandler(this.back_button_Click);
            // 
            // accounce_richTextBox
            // 
            this.accounce_richTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.accounce_richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.accounce_richTextBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.accounce_richTextBox.Font = new System.Drawing.Font("Microsoft New Tai Lue", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.accounce_richTextBox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.accounce_richTextBox.Location = new System.Drawing.Point(12, 12);
            this.accounce_richTextBox.Name = "accounce_richTextBox";
            this.accounce_richTextBox.Size = new System.Drawing.Size(498, 460);
            this.accounce_richTextBox.TabIndex = 2;
            this.accounce_richTextBox.Text = resources.GetString("accounce_richTextBox.Text");
            // 
            // help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(522, 517);
            this.Controls.Add(this.accounce_richTextBox);
            this.Controls.Add(this.back_button);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "help";
            this.Text = "Help";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button back_button;
        private System.Windows.Forms.RichTextBox accounce_richTextBox;
    }
}