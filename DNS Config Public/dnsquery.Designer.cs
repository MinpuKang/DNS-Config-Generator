namespace DNS_Config
{
    partial class dnsquery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dnsquery));
            this.label_dnsserver = new System.Windows.Forms.Label();
            this.label_fqdn = new System.Windows.Forms.Label();
            this.textBox_DnsServer = new System.Windows.Forms.TextBox();
            this.textBox_fqdn = new System.Windows.Forms.TextBox();
            this.button_query = new System.Windows.Forms.Button();
            this.groupBox_result = new System.Windows.Forms.GroupBox();
            this.textBox_Result = new System.Windows.Forms.TextBox();
            this.comboBox_qType = new System.Windows.Forms.ComboBox();
            this.groupBox_result.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_dnsserver
            // 
            this.label_dnsserver.AutoSize = true;
            this.label_dnsserver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_dnsserver.Location = new System.Drawing.Point(12, 9);
            this.label_dnsserver.Name = "label_dnsserver";
            this.label_dnsserver.Size = new System.Drawing.Size(78, 13);
            this.label_dnsserver.TabIndex = 9;
            this.label_dnsserver.Text = "DNS Server:";
            // 
            // label_fqdn
            // 
            this.label_fqdn.AutoSize = true;
            this.label_fqdn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_fqdn.Location = new System.Drawing.Point(45, 42);
            this.label_fqdn.Name = "label_fqdn";
            this.label_fqdn.Size = new System.Drawing.Size(45, 13);
            this.label_fqdn.TabIndex = 10;
            this.label_fqdn.Text = "FQDN:";
            // 
            // textBox_DnsServer
            // 
            this.textBox_DnsServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_DnsServer.Location = new System.Drawing.Point(97, 9);
            this.textBox_DnsServer.Name = "textBox_DnsServer";
            this.textBox_DnsServer.Size = new System.Drawing.Size(259, 20);
            this.textBox_DnsServer.TabIndex = 11;
            // 
            // textBox_fqdn
            // 
            this.textBox_fqdn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_fqdn.Location = new System.Drawing.Point(97, 39);
            this.textBox_fqdn.Name = "textBox_fqdn";
            this.textBox_fqdn.Size = new System.Drawing.Size(259, 20);
            this.textBox_fqdn.TabIndex = 12;
            // 
            // button_query
            // 
            this.button_query.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_query.Location = new System.Drawing.Point(378, 36);
            this.button_query.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button_query.Name = "button_query";
            this.button_query.Size = new System.Drawing.Size(54, 25);
            this.button_query.TabIndex = 19;
            this.button_query.Text = "Query";
            this.button_query.UseVisualStyleBackColor = true;
            this.button_query.Click += new System.EventHandler(this.button_query_Click);
            // 
            // groupBox_result
            // 
            this.groupBox_result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_result.Controls.Add(this.textBox_Result);
            this.groupBox_result.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_result.Location = new System.Drawing.Point(3, 69);
            this.groupBox_result.Name = "groupBox_result";
            this.groupBox_result.Size = new System.Drawing.Size(435, 281);
            this.groupBox_result.TabIndex = 20;
            this.groupBox_result.TabStop = false;
            this.groupBox_result.Text = "Result";
            // 
            // textBox_Result
            // 
            this.textBox_Result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Result.BackColor = System.Drawing.Color.White;
            this.textBox_Result.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_Result.Location = new System.Drawing.Point(7, 15);
            this.textBox_Result.Multiline = true;
            this.textBox_Result.Name = "textBox_Result";
            this.textBox_Result.ReadOnly = true;
            this.textBox_Result.Size = new System.Drawing.Size(422, 260);
            this.textBox_Result.TabIndex = 0;
            // 
            // comboBox_qType
            // 
            this.comboBox_qType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_qType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_qType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_qType.FormattingEnabled = true;
            this.comboBox_qType.Items.AddRange(new object[] {
            "A",
            "NAPTR",
            "AAAA"});
            this.comboBox_qType.Location = new System.Drawing.Point(362, 9);
            this.comboBox_qType.Name = "comboBox_qType";
            this.comboBox_qType.Size = new System.Drawing.Size(70, 21);
            this.comboBox_qType.TabIndex = 21;
            // 
            // dnsquery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(440, 351);
            this.Controls.Add(this.comboBox_qType);
            this.Controls.Add(this.groupBox_result);
            this.Controls.Add(this.button_query);
            this.Controls.Add(this.textBox_fqdn);
            this.Controls.Add(this.textBox_DnsServer);
            this.Controls.Add(this.label_fqdn);
            this.Controls.Add(this.label_dnsserver);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "dnsquery";
            this.Text = "DNS Query";
            this.groupBox_result.ResumeLayout(false);
            this.groupBox_result.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_dnsserver;
        private System.Windows.Forms.Label label_fqdn;
        private System.Windows.Forms.TextBox textBox_DnsServer;
        private System.Windows.Forms.TextBox textBox_fqdn;
        private System.Windows.Forms.Button button_query;
        private System.Windows.Forms.GroupBox groupBox_result;
        private System.Windows.Forms.TextBox textBox_Result;
        private System.Windows.Forms.ComboBox comboBox_qType;
    }
}