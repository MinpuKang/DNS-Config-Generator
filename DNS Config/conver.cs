using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DNS_Config
{
    public partial class conver : Form
    {
        public conver()
        {
            InitializeComponent();
        }

        //验证输入的数据是不是非负整数,传入字符串,返回true或者false
        static bool is_dec(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[0-9]*$");
            return reg1.IsMatch(str);
        }

        //验证输入的数据是不是十六进制,传入字符串,返回true或者false
        static bool is_hex(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[0-9a-fA-F]*$");
            return reg1.IsMatch(str);
        }

        //验证输入的数据是不是二进制,传入字符串,返回true或者false
        static bool is_bin(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[0-1]*$");
            return reg1.IsMatch(str);
        }

        private void dec_textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (dec_textBox.Text != "")
            {
                if (is_dec(dec_textBox.Text) == true)
                {
                    if (dec_textBox.Text.Length > 10)
                    {
                        alarm_label.Text = "The dec length should be less than 10!";
                        hex_textBox.Text = "";
                        bin_textBox.Text = "";
                        bin_textBox1.Text = "";
                    }
                    else
                    {
                        alarm_label.Text = "";
                        if (alarm_label.Text == "")
                        {
                            long dec = Convert.ToInt64(dec_textBox.Text);
                            hex_textBox.Text = dec.ToString("X");
                            string bin = Convert.ToString(dec, 2);
                            //切割二进制
                            string sub_bin = "";
                            int l_bin = bin.Length;
                            int _int = l_bin / 4;
                            if ((bin.Length == 4) || (bin.Length == 8) || (bin.Length == 12) || (bin.Length == 16) || (bin.Length == 20) || (bin.Length == 24) || (bin.Length == 28) || (bin.Length == 32))
                            {
                                _int = _int - 1;
                            }
                            sub_bin = bin.Substring(0, bin.Length - (4 * _int));
                            for (int i = _int - 1; i >= 0; i--)
                            {
                                int start_index = bin.Length - (4 * (i + 1));
                                sub_bin += "-" + bin.Substring(start_index, 4);
                            }
                            bin_textBox.Text = bin;
                            bin_textBox1.Text = sub_bin;
                        }
                    }
                }
                else
                {
                    alarm_label.Text = "The input should be decimal!";
                    hex_textBox.Text = "";
                    bin_textBox.Text = "";
                    bin_textBox1.Text = "";
                }
            }
            else
            {
                alarm_label.Text = "The dec should be filled in!";
                hex_textBox.Text = "";
                bin_textBox.Text = "";
                bin_textBox1.Text = "";
            }
        }

        private void hex_textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (hex_textBox.Text != "")
            {
                if (is_hex(hex_textBox.Text) == true)
                {
                    if (hex_textBox.Text.Length > 8)
                    {
                        alarm_label.Text = "The hex length should be less than 8!";
                        dec_textBox.Text = "";
                        bin_textBox.Text = "";
                        bin_textBox1.Text = "";
                    }
                    else
                    {
                        alarm_label.Text = "";
                        if (alarm_label.Text == "")
                        {
                            if (is_hex(hex_textBox.Text) == true)
                            {
                                long dec = Int64.Parse(hex_textBox.Text, System.Globalization.NumberStyles.HexNumber);
                                dec_textBox.Text = dec.ToString();
                                string bin = Convert.ToString(dec, 2);
                                //切割二进制
                                string sub_bin = "";
                                int l_bin = bin.Length;
                                int _int = l_bin / 4;
                                if ((bin.Length == 4) || (bin.Length == 8) || (bin.Length == 12) || (bin.Length == 16) || (bin.Length == 20) || (bin.Length == 24) || (bin.Length == 28) || (bin.Length == 32))
                                {
                                    _int = _int - 1;
                                }
                                sub_bin = bin.Substring(0, bin.Length - (4 * _int));
                                for (int i = _int - 1; i >= 0; i--)
                                {
                                    int start_index = bin.Length - (4 * (i + 1));
                                    sub_bin += "-" + bin.Substring(start_index, 4);
                                }
                                bin_textBox.Text = bin;
                                bin_textBox1.Text = sub_bin;
                            }
                        }
                    }
                }
                else
                {
                    alarm_label.Text = "The input should be hexadecimal!";
                    dec_textBox.Text = "";
                    bin_textBox.Text = "";
                    bin_textBox1.Text = "";
                }
            }
            else
            {
                alarm_label.Text = "The hex should be filled in!";
                dec_textBox.Text = "";
                bin_textBox.Text = "";
                bin_textBox1.Text = "";
            }
        }

        private void bin_textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (bin_textBox.Text != "")
            {
                if (is_bin(bin_textBox.Text) == true)
                {
                    if (bin_textBox.Text.Length > 32)
                    {
                        alarm_label.Text = "The bin length should be less than 32!";
                        dec_textBox.Text = "";
                        hex_textBox.Text = "";
                    }
                    else
                    {
                        alarm_label.Text = "";
                        if (alarm_label.Text == "")
                        {
                            if (is_bin(bin_textBox.Text) == true)
                            {
                                string bin = bin_textBox.Text;
                                //切割二进制
                                string sub_bin = "";
                                int l_bin = bin.Length;
                                int _int = l_bin / 4;
                                if ((bin.Length == 4) || (bin.Length == 8) || (bin.Length == 12) || (bin.Length == 16) || (bin.Length == 20) || (bin.Length == 24) || (bin.Length == 28) || (bin.Length == 32))
                                {
                                    _int = _int - 1;
                                }
                                sub_bin = bin.Substring(0, bin.Length - (4 * _int));
                                for (int i = _int - 1; i >= 0; i--)
                                {
                                    int start_index = bin.Length - (4 * (i + 1));
                                    sub_bin += "-" + bin.Substring(start_index, 4);
                                }
                                bin_textBox1.Text = sub_bin;

                                long dec = Convert.ToInt64(bin_textBox.Text, 2);
                                dec_textBox.Text = dec.ToString();
                                hex_textBox.Text = dec.ToString("X");
                            }
                            else
                            {
                                alarm_label.Text = "The input should be binary!";
                                dec_textBox.Text = "";
                                hex_textBox.Text = "";
                            }
                        }
                    }
                }
                else
                {
                    alarm_label.Text = "The input should be binary!";
                    dec_textBox.Text = "";
                    hex_textBox.Text = "";
                }
            }
            else
            {
                alarm_label.Text = "The bin should be filled in!";
                dec_textBox.Text = "";
                hex_textBox.Text = "";
            }
        }
    }
}