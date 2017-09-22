namespace DNS_Config
{
    partial class conver
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(conver));
            this.dec_label = new System.Windows.Forms.Label();
            this.hex_label = new System.Windows.Forms.Label();
            this.bin_label = new System.Windows.Forms.Label();
            this.dec_textBox = new System.Windows.Forms.TextBox();
            this.hex_textBox = new System.Windows.Forms.TextBox();
            this.bin_textBox = new System.Windows.Forms.TextBox();
            this.alarm_label = new System.Windows.Forms.Label();
            this.bin_textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // dec_label
            // 
            this.dec_label.AutoSize = true;
            this.dec_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dec_label.Location = new System.Drawing.Point(7, 16);
            this.dec_label.Name = "dec_label";
            this.dec_label.Size = new System.Drawing.Size(34, 13);
            this.dec_label.TabIndex = 0;
            this.dec_label.Text = "Dec:";
            // 
            // hex_label
            // 
            this.hex_label.AutoSize = true;
            this.hex_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hex_label.Location = new System.Drawing.Point(7, 47);
            this.hex_label.Name = "hex_label";
            this.hex_label.Size = new System.Drawing.Size(33, 13);
            this.hex_label.TabIndex = 1;
            this.hex_label.Text = "Hex:";
            // 
            // bin_label
            // 
            this.bin_label.AutoSize = true;
            this.bin_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bin_label.Location = new System.Drawing.Point(7, 78);
            this.bin_label.Name = "bin_label";
            this.bin_label.Size = new System.Drawing.Size(29, 13);
            this.bin_label.TabIndex = 2;
            this.bin_label.Text = "Bin:";
            // 
            // dec_textBox
            // 
            this.dec_textBox.Location = new System.Drawing.Point(47, 13);
            this.dec_textBox.Name = "dec_textBox";
            this.dec_textBox.Size = new System.Drawing.Size(294, 22);
            this.dec_textBox.TabIndex = 3;
            this.dec_textBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dec_textBox_KeyUp);
            // 
            // hex_textBox
            // 
            this.hex_textBox.Location = new System.Drawing.Point(47, 43);
            this.hex_textBox.Name = "hex_textBox";
            this.hex_textBox.Size = new System.Drawing.Size(294, 22);
            this.hex_textBox.TabIndex = 4;
            this.hex_textBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.hex_textBox_KeyUp);
            // 
            // bin_textBox
            // 
            this.bin_textBox.Location = new System.Drawing.Point(47, 73);
            this.bin_textBox.Name = "bin_textBox";
            this.bin_textBox.Size = new System.Drawing.Size(294, 22);
            this.bin_textBox.TabIndex = 5;
            this.bin_textBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.bin_textBox_KeyUp);
            // 
            // alarm_label
            // 
            this.alarm_label.AutoSize = true;
            this.alarm_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.alarm_label.ForeColor = System.Drawing.Color.Red;
            this.alarm_label.Location = new System.Drawing.Point(44, 129);
            this.alarm_label.Name = "alarm_label";
            this.alarm_label.Size = new System.Drawing.Size(0, 13);
            this.alarm_label.TabIndex = 6;
            // 
            // bin_textBox1
            // 
            this.bin_textBox1.BackColor = System.Drawing.Color.White;
            this.bin_textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bin_textBox1.ForeColor = System.Drawing.Color.Red;
            this.bin_textBox1.Location = new System.Drawing.Point(47, 99);
            this.bin_textBox1.Name = "bin_textBox1";
            this.bin_textBox1.ReadOnly = true;
            this.bin_textBox1.Size = new System.Drawing.Size(294, 20);
            this.bin_textBox1.TabIndex = 7;
            // 
            // conver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(353, 154);
            this.Controls.Add(this.bin_textBox1);
            this.Controls.Add(this.alarm_label);
            this.Controls.Add(this.bin_textBox);
            this.Controls.Add(this.hex_textBox);
            this.Controls.Add(this.dec_textBox);
            this.Controls.Add(this.bin_label);
            this.Controls.Add(this.hex_label);
            this.Controls.Add(this.dec_label);
            this.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "conver";
            this.Text = "Bit Conver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dec_label;
        private System.Windows.Forms.Label hex_label;
        private System.Windows.Forms.Label bin_label;
        private System.Windows.Forms.TextBox dec_textBox;
        private System.Windows.Forms.TextBox hex_textBox;
        private System.Windows.Forms.TextBox bin_textBox;
        private System.Windows.Forms.Label alarm_label;
        private System.Windows.Forms.TextBox bin_textBox1;
    }
}