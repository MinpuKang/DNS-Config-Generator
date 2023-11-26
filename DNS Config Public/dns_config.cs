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
using System.IO;
using System.Threading;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace DNS_Config
{
    public partial class DNS_config : Form
    {
        string StrStartTime = string.Empty;
        string StrEndTime = string.Empty;
        static DateTime nx = new DateTime(1970, 1, 1);
        TimeSpan ts = DateTime.UtcNow - nx;

        string fileTime = "";

        public DNS_config()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(',') ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");
            dllName = dllName.Replace(".", "_");
            if (dllName.EndsWith("_resources")) return null;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
            byte[] bytes = (byte[])rm.GetObject(dllName);
            return System.Reflection.Assembly.Load(bytes);
        }


        private void DNS_config_Load(object sender, EventArgs e)
        {
            StrStartTime = ((long)(ts.TotalSeconds * 1000)).ToString();
        }

        private void DNS_config_FormClosing(object sender, FormClosingEventArgs e)
        {
            ts = DateTime.UtcNow - nx;
            StrEndTime = ((long)(ts.TotalSeconds * 1000)).ToString();

            Thread SendUsage = new Thread(checkIntranet);
            SendUsage.Start();
        }

        public void checkIntranet()
        {
            UsageReport.UsageReport myUsageReport = new UsageReport.UsageReport();
            myUsageReport.starttime = StrStartTime;
            myUsageReport.endtime = StrEndTime;
            myUsageReport.checkIntranet();
        }

        /// <summary>  
        /// if nodepad++ is installed 
        /// </summary>  
        /// <returns>true: yes, false: not</returns>  
        private bool checkNodepadplus()
        {
            Microsoft.Win32.RegistryKey uninstallNode = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE/Microsoft/Windows/CurrentVersion/Uninstall");
            foreach (string subKeyName in uninstallNode.GetSubKeyNames())
            {
                Microsoft.Win32.RegistryKey subKey = uninstallNode.OpenSubKey(subKeyName);
                object displayName = subKey.GetValue("DisplayName");
                if (displayName != null)
                {
                    if (displayName.ToString().Contains("nodepad++"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //MNC长度
        private string mnc_length(string str)
        {
            if (str.Length == 2)
            {
                str = "0" + str;
                return str;
            }
            else if (str.Length == 3)
            {
                return str;
            }
            else
            {
                return "wrong";
            }

        }

        //验证输入的数据是不是正整数,传入字符串,返回true或者false
        static bool is_integer(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
            return reg1.IsMatch(str);
        }

        //验证IPv4
        public bool is_ipv4(string ip_v4)
        {
            Regex reg = new Regex(@"(?n)^\s*(([1-9]?[0-9]|1[0-9]{2}|2([0-4][0-9]|5[0-5]))\.){3}([1-9]?[0-9]|1[0-9]{2}|2([0-4][0-9]|5[0-5]))\s*$");
            if (reg.IsMatch(ip_v4))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //验证IPv6
        public bool is_ipv6(string ip_v6)
        {
            Regex reg = new Regex(@"^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$");
            if (reg.IsMatch(ip_v6))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //amf region id + amf set id to mme group id
        private string ari_asi_to_mmegid(int amfregionid, int amfsetid)
        {
            //convert amf region id to binary
            string bin_amfregionid = Convert.ToString(amfregionid, 2);

            //convert amf set id to binary
            string bin_amfsetid = Convert.ToString(amfsetid, 2);

            //bin of amf region id is 8 bits
            while (bin_amfregionid.Length < 8)
            {
                bin_amfregionid = "0" + bin_amfregionid;
            }

            //bin of amf set id is 10 bits
            while (bin_amfsetid.Length < 10)
            {
                bin_amfsetid = "0" + bin_amfsetid;
            }

            //mme group id(16bits) = amf region id(8 bits) + the highest 8 bits of amf set id
            string bin_mmegid = bin_amfregionid + bin_amfsetid.Substring(0, 8);

            //convert the binary of mmegid to decimalism
            int mmegid = Convert.ToInt32(bin_mmegid, 2);

            return mmegid.ToString("X");
        }

        //amf set id + amf pointer to mme code
        private string asi_ap_to_mmecode(int amfsetid, int amfpointer)
        {
            //convert amf set id to binary
            string bin_amfsetid = Convert.ToString(amfsetid, 2);

            //convert amf pointer to binary
            string bin_amfpointer = Convert.ToString(amfpointer, 2);

            //bin of amf set id is 10 bits
            while (bin_amfsetid.Length < 10)
            {
                bin_amfsetid = "0" + bin_amfsetid;
            }

            //bin of amf pointer is 6 bits
            while (bin_amfpointer.Length < 6)
            {
                bin_amfpointer = "0" + bin_amfpointer;
            }

            //mmecode(8 bits)=lowest 2 bits of amf set id + total 6 bits of amf pointer
            string bin_mmecode = bin_amfsetid.Substring(10 - 2) + bin_amfpointer;

            //convert the bin of mmecode to decimalism
            int mmecode = Convert.ToInt32(bin_mmecode, 2);

            return mmecode.ToString("X");

        }

        //remove empty line in excel
        private void DeleteEmptyRows(ExcelWorksheet worksheet)
        {
            // 从底部开始遍历行
            for (int row = worksheet.Dimension.End.Row; row > 0; row--)
            {
                bool isRowEmpty = true;

                // 检查当前行的每个单元格是否为空
                for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
                {
                    var cellValue = worksheet.Cells[row, col].Text;
                    if (!string.IsNullOrWhiteSpace(cellValue))
                    {
                        isRowEmpty = false;
                        break;
                    }
                }

                // 如果当前行为空，则删除
                if (isRowEmpty)
                {
                    worksheet.DeleteRow(row);
                }
            }
        }

        static bool IsNumericAndDotOnly(string input)
        {
            // 使用正则表达式检查输入是否只包含数字和点，且点不能连续出现
            // \d表示数字，\.表示点，*表示零次或多次
            // (?<!\.)表示前面不能是点
            // (?>\.\d+)表示点后面必须跟着至少一个数字
            string pattern = @"^[1-9]\d*(?:\.[1-9]\d*)*$";
            return Regex.IsMatch(input, pattern);
        }


        static void cell_format(ExcelWorksheet worksheet, string input)
        {
            // 获取单元格 A1 的样式对象
            var cellStyle = worksheet.Cells[input].Style;

            // 设置字体加粗
            cellStyle.Font.Bold = true;

            // 设置背景颜色
            cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
            cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

            // 设置字体颜色
            cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
        }



        private void Submit_Click(object sender, EventArgs e)
        {
            result_textBox.Text = "";
            fileTime = DateTime.Now.ToString("yyyyMMddhhmmss");
            string mcc = "";
            string mnc = "";
            string lac = "";
            string rac = "";
            string tac_hb = "";
            string tac_lb = "";

            string tac_5gepc_hb = "";
            string tac_5gepc_lb = "";

            string tac_5gs_hb = "";
            string tac_5gs_mb = "";
            string tac_5gs_lb = "";

            string apn = "";
            string mmegid = "";
            string mmec = "";
            string topological = "";

            string uetype = "";
            string network_capacity = "";
            string sgw_network_capacity = "";

            string amfrid = "";
            string amfsid = "";
            string amfpointer = "";

            string amfrid_hex = "";
            string amfsid_hex = "";
            string amfpointer_hex = "";

            string amf_mmegid = "";
            string amf_mmec = "";

            //检测MCC
            if (mcc_textBox.Text == "")
            {
                MessageBox.Show("MCC cannot be empty!", "MCC");
                return;
            }
            else
            {
                if (is_integer(mcc_textBox.Text) == false)
                {
                    MessageBox.Show("MCC must be integer!", "MCC");
                    return;
                }
                else
                {
                    if (mcc_textBox.Text.Length != 3)
                    {
                        MessageBox.Show("MCC must be integer with 3 digits!", "MCC");
                        return;
                    }
                    else
                    {
                        mcc = mcc_textBox.Text;
                    }
                }
            }
            //检测MNC
            if (mnc_textBox.Text == "")
            {
                MessageBox.Show("MNC cannot be empty!", "MNC");
                return;
            }
            else
            {
                if (is_integer(mnc_textBox.Text) == false)
                {
                    MessageBox.Show("MMNC must be integer!", "MNC");
                    return;
                }
                else
                {
                    if (mnc_length(mnc_textBox.Text) != "wrong")
                    {
                        mnc = mnc_length(mnc_textBox.Text);
                    }
                    else
                    {
                        MessageBox.Show("MNC must be integer with 2-3 digits!", "MNC");
                        return;
                    }
                }
            }

            if ((lac_textBox.Text == "") && (rac_textBox.Text == "") && (mmegid_textBox.Text == "") && (mmec_textBox.Text == "") && (amfri_textBox.Text == "") && (amfsi_textBox.Text == "") && (amfp_textBox.Text == "") && (tac_textBox.Text == "") && (tac_5g_textBox.Text == "") && (apn_textBox.Text == ""))
            {
                MessageBox.Show("No available ID in LAC-RAC pair, 4G TAC, 5G TAC, GUMMEI, GUAMI or APN!", "ID");
                return;
            }

            //检测lac-rac
            if ((lac_textBox.Text != "") && (rac_textBox.Text != ""))
            {
                if ((is_integer(lac_textBox.Text)) && (is_integer(rac_textBox.Text)))
                {
                    if ((lac_textBox.Text.Length < 6) && (rac_textBox.Text.Length < 4))
                    {
                        int int_lac = Convert.ToInt32(lac_textBox.Text);
                        int int_rac = Convert.ToInt32(rac_textBox.Text);
                        if ((int_lac < 65536) && (int_lac != 65534) && (int_rac < 256))
                        {
                            lac = int_lac.ToString("X");
                            rac = int_rac.ToString("X");
                            //检查十六进制位数
                            while (lac.Length < 4)
                            {
                                lac = '0' + lac;
                            }
                            while (rac.Length < 4)
                            {
                                rac = '0' + rac;
                            }
                        }
                        else
                        {
                            MessageBox.Show("LAC with value rang 1-65533 and 65535, RAC is from 0 to 255.!", "LAC-RAC");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("LAC with value rang 1-65533 and 65535, RAC is from 0 to 255.!", "LAC-RAC");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("LAC-RAC must be integer!", "LAC-RAC");
                    return;
                }
            }
            else if (((lac_textBox.Text != "") && (rac_textBox.Text == "")) || ((lac_textBox.Text == "") && (rac_textBox.Text != "")))
            {
                MessageBox.Show("LAC-RAC must be in pair!", "LAC-RAC");
                return;
            }

            //检测TAC
            if (tac_textBox.Text != "")
            {
                if (is_integer(tac_textBox.Text))
                {
                    if (tac_textBox.Text.Length < 6)
                    {
                        int tac = Convert.ToInt32(tac_textBox.Text);
                        if (tac < 65536)
                        {
                            string hex_tac = tac.ToString("X");

                            while (hex_tac.Length < 4)
                            {
                                hex_tac = '0' + hex_tac;
                            }
                            tac_hb = hex_tac.Substring(0, 2);
                            tac_lb = hex_tac.Substring(2, 2);
                        }
                        else
                        {
                            MessageBox.Show("TAC cannot be large than 65535!", "TAC");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("TAC cannot be large than 65535!", "TAC");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("TAC must be integer!", "TAC");
                    return;
                }
            }

            //检测5G TAC
            if (tac_5g_textBox.Text != "")
            {
                if (is_integer(tac_5g_textBox.Text))
                {
                    if (tac_5g_textBox.Text.Length < 6)
                    {
                        int tac_5g = Convert.ToInt32(tac_5g_textBox.Text);
                        if (tac_5g < 65536)
                        {
                            string hex_tac_5gepc = tac_5g.ToString("X");
                            string hex_tac_5gs = hex_tac_5gepc;
                            while (hex_tac_5gepc.Length < 4)
                            {
                                hex_tac_5gepc = '0' + hex_tac_5gepc;
                            }
                            tac_5gepc_hb = hex_tac_5gepc.Substring(0, 2);
                            tac_5gepc_lb = hex_tac_5gepc.Substring(2, 2);

                            while (hex_tac_5gs.Length < 6)
                            {
                                hex_tac_5gs = '0' + hex_tac_5gs;
                            }
                            tac_5gs_hb = hex_tac_5gs.Substring(0, 2);
                            tac_5gs_mb = hex_tac_5gs.Substring(2, 2);
                            tac_5gs_lb = hex_tac_5gs.Substring(4, 2);
                        }
                        else
                        {
                            MessageBox.Show("5G TAC cannot be large than 65535!", "5G TAC");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("5G TAC cannot be large than 65535!", "5G TAC");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("5G TAC must be integer!", "5G TAC");
                    return;
                }
            }

            //GUMMEI检测
            if ((mmegid_textBox.Text != "") && (mmec_textBox.Text != ""))
            {
                if ((is_integer(mmegid_textBox.Text)) && (is_integer(mmec_textBox.Text)))
                {
                    if ((mmegid_textBox.Text.Length < 6) && (mmec_textBox.Text.Length < 4))
                    {
                        int int_mmegid = Convert.ToInt32(mmegid_textBox.Text);
                        int int_mmec = Convert.ToInt32(mmec_textBox.Text);

                        if ((int_mmegid < 65536) && (int_mmegid != 65534) && (int_mmec < 256))
                        {
                            mmegid = int_mmegid.ToString("X");
                            mmec = int_mmec.ToString("X");
                            //检查十六进制位数
                            while (mmegid.Length < 4)
                            {
                                mmegid = '0' + mmegid;
                            }
                            while (mmec.Length < 2)
                            {
                                mmec = '0' + mmec;
                            }
                        }
                        else
                        {
                            MessageBox.Show("MMEGID with value rang 1-65533 and 65535, MMECode is from 0 to 255!", "GUMMEI");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("MMEGID with value rang 1-65533 and 65535, MMECode is from 0 to 255!", "GUMMEI");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("GUMMEI-MMEGID and MMECode must be integer!", "GUMMEI");
                    return;
                }
            }
            else if (((mmegid_textBox.Text != "") && (mmec_textBox.Text == "")) || ((mmegid_textBox.Text == "") && (mmec_textBox.Text != "")))
            {
                MessageBox.Show("GUMMEI-MMEGID and MMECode must be in pair!", "GUMMEI");
                return;
            }

            //检查GUAMI: amf ids to mme: gid and code
            if ((amfri_textBox.Text != "") && (amfsi_textBox.Text != "") && (amfp_textBox.Text != ""))
            {

                if ((Regex.IsMatch(amfri_textBox.Text, "^([0-9]|([1-9][0-9])|(1[0-9][0-9])|(2[0-4][0-9])|(2[5][0-5]))$")))
                {
                    amfrid = amfri_textBox.Text;
                    amfrid_hex = Convert.ToInt32(amfri_textBox.Text).ToString("X");

                    while (amfrid_hex.Length < 2)
                    {
                        amfrid_hex = "0" + amfrid_hex;
                    }

                }
                else
                {
                    MessageBox.Show("AMF Region ID should be from 0 to 255.", "AMF Region ID");
                }

                if ((Regex.IsMatch(amfsi_textBox.Text, "^([0-9]{1,3}|10[0-1][0-9]|102[0-3])$")))
                {
                    amfsid = amfsi_textBox.Text;
                    amfsid_hex = Convert.ToInt32(amfsi_textBox.Text).ToString("X");

                    while (amfsid_hex.Length < 3)
                    {
                        amfsid_hex = "0" + amfsid_hex;
                    }
                }
                else
                {
                    MessageBox.Show("AMF Set ID should be from 0 to 1023.", "AMF Set ID");
                }

                if ((Regex.IsMatch(amfp_textBox.Text, "^([0-9]|[1-5][0-9]|6[0-3])$")))
                {
                    amfpointer = amfp_textBox.Text;
                    amfpointer_hex = Convert.ToInt32(amfp_textBox.Text).ToString("X");

                    while (amfpointer_hex.Length < 2)
                    {
                        amfpointer_hex = "0" + amfpointer_hex;
                    }
                }
                else
                {
                    MessageBox.Show("AMF Pointer should be from 0 to 63.", "AMF Pointer");
                }

                if (amfrid != "" && amfsid != "" && amfpointer != "")
                {
                    amf_mmegid = ari_asi_to_mmegid(Convert.ToInt32(amfrid), Convert.ToInt32(amfsid));
                    amf_mmec = asi_ap_to_mmecode(Convert.ToInt32(amfsid), Convert.ToInt32(amfpointer));

                    //检查十六进制位数
                    while (amf_mmegid.Length < 4)
                    {
                        amf_mmegid = '0' + amf_mmegid;
                    }
                    while (amf_mmec.Length < 2)
                    {
                        amf_mmec = '0' + amf_mmec;
                    }
                }

            }
            else if (((amfri_textBox.Text != "") && (amfsi_textBox.Text == "" || amfp_textBox.Text == "")) || ((amfri_textBox.Text == "" || amfsi_textBox.Text == "") && (amfp_textBox.Text != "")) || ((amfri_textBox.Text == "" || amfp_textBox.Text == "") && (amfsi_textBox.Text != "")))
            {
                MessageBox.Show("GUAMI-AMF Regio ID, Set ID and Pointer must be filled!", "GUAMI");
                return;
            }


            //APN是否为空
            if (apn_textBox.Text != "")
            {
                apn = apn_textBox.Text;
            }

            //topon and topoff selection
            if (topon_radioButton.Checked == true)
            {
                topological = topon_radioButton.Text;
            }
            else if (topoff_radioButton.Checked == true)
            {
                topological = topoff_radioButton.Text;
            }

            //smf and nr support as a network capacity
            if ((smf_support_checkBox.Checked == true) && (nr_support_checkBox.Checked == true))
            {
                network_capacity = "+nc-nr.smf";
            }
            else if ((smf_support_checkBox.Checked == true) && (nr_support_checkBox.Checked == false))
            {
                network_capacity = "+nc-smf";
            }
            else if ((smf_support_checkBox.Checked == false) && (nr_support_checkBox.Checked == true))
            {
                network_capacity = "+nc-nr";
            }
            if (nr_support_checkBox.Checked == true)
            {
                sgw_network_capacity = "+nc-nr";
            }

            //ue uasage type check
            if (uetype_textBox.Text != "")
            {
                if (IsNumericAndDotOnly(uetype_textBox.Text))
                {
                    uetype = "+ue-" + uetype_textBox.Text;

                }
                else
                {
                    uetype_error_label.Text = "UE Type is not correct!";
                }
            }

            //For FQDN
            if ((sgwip_textBox.Text == "") && (pgwip_textBox.Text == "") && (sgsnip_textBox.Text == "") && (mmeip_textBox.Text == "") && (amfip_textBox.Text == ""))
            {
                result_textBox.Text = "";
                result_textBox.Height = this.Height;

                result_textBox.Text = "============================\r\n" + "FQDN for Test DNS resolving:\r\n============================\r\n";

                if ((lac != "") && (rac != ""))
                {
                    result_textBox.Text += "\r\n===New SGSN query old SGSN for RAU===\r\n";
                    result_textBox.Text += "    rac" + rac + ".lac" + lac + ".mnc" + mnc + ".mcc" + mcc + ".gprs.\r\n";

                    result_textBox.Text += "\r\n===New MME selects an old SGSN based on GUMMEI which is mapped from LAC-RAC in the old SGSN, this is used when ISC-TAU from W/G to LTE with Gn/Gp:------------IRAT!===" + "\r\n";
                    result_textBox.Text += "    rac" + rac + ".lac" + lac + ".rac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                    result_textBox.Text += "    Service: x-3gpp-sgsn:x-gn:x-gp\r\n";
                }

                if ((tac_hb != "") && (tac_lb != ""))
                {
                    result_textBox.Text += "\r\n===MME seletes A SGW, Source MME quary Target MME for inter-LTE S1-HO===\r\n";
                    result_textBox.Text += "    tac-lb" + tac_lb + ".tac-hb" + tac_hb + ".tac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                    result_textBox.Text += "    Service for SGW: x-3gpp-sgw:x-s11\r\n";
                    result_textBox.Text += "    Service for MME: x-3gpp-mme:x-s10\r\n";
                }

                if ((tac_5gepc_hb != "") && (tac_5gepc_lb != ""))
                {
                    result_textBox.Text += "\r\n===MME seletes an AMF which using S10 during HO from 4G to 5G===\r\n";
                    result_textBox.Text += "    tac-lb" + tac_5gepc_lb + ".tac-hb" + tac_5gepc_hb + ".tac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                    result_textBox.Text += "    Service for MME: x-3gpp-mme:x-s10\r\n";
                }

                if ((tac_5gs_hb != "") && (tac_5gs_mb != "") && (tac_5gs_lb != ""))
                {
                    result_textBox.Text += "\r\n===MME seletes an AMF which using N26 during HO from 4G to 5G===\r\n";
                    result_textBox.Text += "    tac-lb" + tac_5gs_lb + ".tac-mb" + tac_5gs_mb + ".tac-hb" + tac_5gs_hb + ".5gstac.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                    result_textBox.Text += "    Service for AMF: x-3gpp-amf:x-n26\r\n";
                }

                if (apn != "")
                {
                    result_textBox.Text += "\r\n===APN FQDN for GGSN===\r\n";
                    result_textBox.Text += "    " + apn + ".mnc" + mnc + ".mcc" + mcc + ".gprs.\r\n";

                    result_textBox.Text += "\r\n===APN FQDN for PGW===\r\n";
                    result_textBox.Text += "    " + apn + ".apn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                    result_textBox.Text += "    Services: x-3gpp-pgw:x-s5-gtp:x-s8-gtp:x-s2b-gtp:x-s2a-gtp:x-gn:x-gp\r\n";
                }


                if ((mmegid != "") && (mmec != ""))
                {
                    result_textBox.Text += "\r\n===Old MME selected based on GUMMEI during inter TAU!===\r\n";
                    result_textBox.Text += "    mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                    result_textBox.Text += "    Service for MME: x-3gpp-mme:x-s10\r\n";

                    string lac_mmegid = mmegid;
                    string rac_mmec = mmec;
                    while (rac_mmec.Length < 4)
                    {
                        rac_mmec = "0" + rac_mmec;
                    }
                    result_textBox.Text += "\r\n===New SGSN queries the old MME during ISC-RAU from LTE to W/G with Gn/Gp:------------IRAT, and the MMEGroupID mapped to old LAC, MMECode mapped to old RAC!===\r\n";
                    result_textBox.Text += "    rac" + rac_mmec + ".lac" + lac_mmegid + ".mnc" + mnc + ".mcc" + mcc + ".gprs.\r\n\r\n";
                }

                if ((amf_mmegid != "") && (amf_mmec != ""))
                {
                    result_textBox.Text += "\r\n===Old AMF selected based on GUAMI during TAU from 5G to LTE with S10 used as N26!===\r\n";
                    result_textBox.Text += "    mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                    result_textBox.Text += "    Service for AMF: x-3gpp-mme:x-s10\r\n";
                }

                if ((amfrid_hex != "") && (amfsid_hex != "") && (amfpointer_hex != ""))
                {
                    result_textBox.Text += "\r\n===Old AMF selected based on GUAMI during TAU from 5G to LTE with N26!===\r\n";
                    result_textBox.Text += "    pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                    result_textBox.Text += "    Service for AMF: x-3gpp-amf:x-n26\r\n";
                }

            }

            //For DNS config
            if ((sgwip_textBox.Text != "") || (pgwip_textBox.Text != "") || (sgsnip_textBox.Text != "") || (mmeip_textBox.Text != "") || (amfip_textBox.Text != ""))
            {
                result_textBox.Text = "";

                result_textBox.Text += ";===========================================================\r\n" + ";DNS configuration: services including all are examples here\r\n;===========================================================\r\n";

                //DNS config for SGW
                if (sgwip_textBox.Text != "")
                {
                    if ((tac_hb != "") && (tac_lb != ""))
                    {
                        for (int i = 0; i < sgwip_textBox.Lines.Length; i++)
                        {
                            if (is_ipv4(sgwip_textBox.Lines[i]) || is_ipv6(sgwip_textBox.Lines[i]))
                            {
                                result_textBox.Text += "\r\n;\r\n" + "; SGW selected based on TAC, used during attach, inter/intra TAU with SGW relocation and inter/intra HO with SGW relocation!" + "\r\n;\r\n";
                                if (srv_radioButton.Checked == true)
                                {
                                    result_textBox.Text += "tac-lb" + tac_lb + ".tac-hb" + tac_hb + ".tac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"s\"  \"x-3gpp-sgw:x-s5-gtp" + uetype + sgw_network_capacity + ":x-s8-gtp" + uetype + sgw_network_capacity + "\" \"\" " + "sgw-list" + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                                break;
                            }
                        }

                        for (int i = 0; i < sgwip_textBox.Lines.Length; i++)
                        {
                            if (naptr_radioButton.Checked == true)
                            {
                                if (is_ipv4(sgwip_textBox.Lines[i]) || is_ipv6(sgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += "tac-lb" + tac_lb + ".tac-hb" + tac_hb + ".tac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\"  \"x-3gpp-sgw:x-s5-gtp" + uetype + sgw_network_capacity + ":x-s8-gtp" + uetype + sgw_network_capacity + "\" \"\" " + topological + ".sgw-s5s8.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                                if (is_ipv4(sgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-sgw:x-s11" + uetype + sgw_network_capacity + "\" \"\" " + topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                    result_textBox.Text += topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A    " + sgwip_textBox.Lines[i] + "\r\n\r\n";
                                }
                                if (is_ipv6(sgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-sgw:x-s11" + uetype + sgw_network_capacity + "\" \"\" " + topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                    result_textBox.Text += topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA    " + sgwip_textBox.Lines[i] + "\r\n\r\n";
                                }
                            }
                            else if (srv_radioButton.Checked == true)
                            {
                                if (is_ipv4(sgwip_textBox.Lines[i]) || is_ipv6(sgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += "sgw-list" + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org." + "   IN SRV 100 100 2123   " + topological + ".sgw-s5s8.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                            }
                        }

                        if (srv_radioButton.Checked == true)
                        {
                            for (int i = 0; i < sgwip_textBox.Lines.Length; i++)
                            {
                                if (is_ipv4(sgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-sgw:x-s11" + uetype + sgw_network_capacity + "\" \"\" " + topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                                if (is_ipv6(sgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-sgw:x-s11" + uetype + sgw_network_capacity + "\" \"\" " + topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                            }
                            for (int i = 0; i < sgwip_textBox.Lines.Length; i++)
                            {
                                if (is_ipv4(sgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A    " + sgwip_textBox.Lines[i] + "\r\n";
                                }
                                if (is_ipv6(sgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA    " + sgwip_textBox.Lines[i] + "\r\n";
                                }
                            }
                            result_textBox.Text += "\r\n";
                        }
                    }
                }

                //DNS config for PGW
                if (pgwip_textBox.Text != "")
                {
                    if (apn_textBox.Text != "")
                    {

                        for (int i = 0; i < pgwip_textBox.Lines.Length; i++)
                        {
                            if (is_ipv4(pgwip_textBox.Lines[i]) || is_ipv6(pgwip_textBox.Lines[i]))
                            {
                                result_textBox.Text += "\r\n;\r\n" + "; GGSN selected based on APN, used during PDP active in 2/3G!" + "\r\n;\r\n";
                                break;
                            }
                        }

                        for (int i = 0; i < pgwip_textBox.Lines.Length; i++)
                        {
                            if (is_ipv4(pgwip_textBox.Lines[i]))
                            {
                                result_textBox.Text += apn + ".mnc" + mnc + ".mcc" + mcc + ".gprs.     IN A     " + pgwip_textBox.Lines[i] + "\r\n";
                            }

                            if (is_ipv6(pgwip_textBox.Lines[i]))
                            {
                                result_textBox.Text += apn + ".mnc" + mnc + ".mcc" + mcc + ".gprs.     IN AAAA      " + pgwip_textBox.Lines[i] + "\r\n";
                            }
                        }

                        for (int i = 0; i < pgwip_textBox.Lines.Length; i++)
                        {
                            if (is_ipv4(pgwip_textBox.Lines[i]) || is_ipv6(pgwip_textBox.Lines[i]))
                            {
                                result_textBox.Text += "\r\n;\r\n" + "; PGW selected based on APN, used during LTE/WiFi attach or PDN connection, HO between LTE and WiFi!" + "\r\n;\r\n";
                                if (srv_radioButton.Checked == true)
                                {
                                    result_textBox.Text += apn + ".apn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"s\"  \"x-3gpp-pgw:x-s5-gtp" + network_capacity + uetype + ":x-s8-gtp" + network_capacity + uetype + ":x-s2b-gtp:x-s2a-gtp:x-gn:x-gp\" \"\" " + "pgw-list" + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                                break;
                            }
                        }

                        for (int i = 0; i < pgwip_textBox.Lines.Length; i++)
                        {
                            if (naptr_radioButton.Checked == true)
                            {
                                if (is_ipv4(pgwip_textBox.Lines[i]) || is_ipv6(pgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += apn + ".apn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-pgw:x-s5-gtp" + network_capacity + uetype + ":x-s8-gtp" + network_capacity + uetype + ":x-s2b-gtp:x-s2a-gtp:x-gn:x-gp\" \"\" " + topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                    result_textBox.Text += "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-pgw:x-s5-gtp" + network_capacity + uetype + ":x-s8-gtp" + network_capacity + uetype + ":x-s2b-gtp:x-s2a-gtp:x-gn:x-gp\" \"\" " + topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                                if (is_ipv4(pgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A    " + pgwip_textBox.Lines[i] + "\r\n\r\n";
                                }
                                if (is_ipv6(pgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA     " + pgwip_textBox.Lines[i] + "\r\n\r\n";
                                }
                            }
                            else if (srv_radioButton.Checked == true)
                            {
                                if (is_ipv4(pgwip_textBox.Lines[i]) || is_ipv6(pgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += "pgw-list" + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org." + "   IN SRV 100 100 2123   " + topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                            }
                        }

                        if (srv_radioButton.Checked == true)
                        {
                            for (int i = 0; i < pgwip_textBox.Lines.Length; i++)
                            {
                                if (is_ipv4(pgwip_textBox.Lines[i]) || is_ipv6(pgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-pgw:x-s5-gtp" + network_capacity + uetype + ":x-s8-gtp" + network_capacity + uetype + ":x-s2b-gtp:x-s2a-gtp:x-gn:x-gp\" \"\" " + topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                            }
                            for (int i = 0; i < pgwip_textBox.Lines.Length; i++)
                            {
                                if (is_ipv4(pgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A    " + pgwip_textBox.Lines[i] + "\r\n";
                                }
                                if (is_ipv6(pgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA     " + pgwip_textBox.Lines[i] + "\r\n";
                                }
                            }
                            result_textBox.Text += "\r\n";
                        }
                    }
                }

                //DNS config for SGSN
                if (sgsnip_textBox.Text != "")
                {
                    if ((lac != "") && (rac != ""))
                    {
                        if (is_ipv4(sgsnip_textBox.Text))
                        {
                            result_textBox.Text += "\r\n;\r\n" + "; OldSGSN selected based on LAC-RAC if no cooperating SGSN defined, used when P-TMSI attach, Inter RAU, ISC Inter RAU!" + "\r\n;\r\n";
                            result_textBox.Text += "rac" + rac + ".lac" + lac + ".mnc" + mnc + ".mcc" + mcc + ".gprs.        IN A " + sgsnip_textBox.Text + "\r\n";

                            result_textBox.Text += "\r\n;\r\n" + "; New MME selects an old SGSN based on GUMMEI which is mapped from LAC-RAC in the old SGSN, this is used when ISC-TAU from W/G to LTE with Gn/Gp:------------IRAT!" + "\r\n";
                            result_textBox.Text += "; Source MME selects target SGSN during Handover from LTE to WCDMA with Gn network" + "\r\n;\r\n";
                            result_textBox.Text += "rac" + rac + ".lac" + lac + ".rac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-sgsn:x-gn" + uetype + ":x-gp" + uetype + "\" \"\"    topoff.gn-target-sgsn01.sgsn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.gn-target-sgsn01.sgsn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A " + sgsnip_textBox.Text + "\r\n\r\n";
                        }

                        if (is_ipv6(sgsnip_textBox.Text))
                        {
                            result_textBox.Text += "\r\n;\r\n" + "; OldSGSN selected based on LAC-RAC if no cooperating SGSN defined, used when P-TMSI attach, Inter RAU, ISC Inter RAU!" + "\r\n;\r\n";
                            result_textBox.Text += "rac" + rac + ".lac" + lac + ".mnc" + mnc + ".mcc" + mcc + ".gprs.        IN AAAA " + sgsnip_textBox.Text + "\r\n";

                            result_textBox.Text += "\r\n;=======================================================\r\n" + ";New MME selects an old SGSN based on GUMMEI which is mapped from LAC-RAC in the old SGSN, this is used when ISC-TAU from W/G to LTE with Gn/Gp:------------IRAT!" + "\r\n;\r\n";
                            result_textBox.Text += "rac" + rac + ".lac" + lac + ".rac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-sgsn:x-gn" + uetype + ":x-gp" + uetype + "\" \"\" sgsnoldgn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "sgsnoldgn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA " + sgsnip_textBox.Text + "\r\n\r\n";
                        }
                    }

                    if ((mmegid != "") && (mmec != ""))
                    {
                        if (is_ipv4(sgsnip_textBox.Text))
                        {
                            string lac_mmegid = mmegid;
                            string rac_mmec = mmec;
                            while (rac_mmec.Length < 4)
                            {
                                rac_mmec = "0" + rac_mmec;
                            }
                            result_textBox.Text += "\r\n;\r\n" + "; New SGSN queries the old MME during ISC-RAU from LTE to W/G with Gn/Gp:------------IRAT, and the MMEGroupID mapped to old LAC, MMECode mapped to old RAC!" + "\r\n;\r\n";
                            result_textBox.Text += "rac" + rac_mmec + ".lac" + lac_mmegid + ".mnc" + mnc + ".mcc" + mcc + ".gprs.        IN A " + sgsnip_textBox.Text + "\r\n\r\n";
                        }

                        if (is_ipv6(sgsnip_textBox.Text))
                        {
                            string lac_mmegid = mmegid;
                            string rac_mmec = mmec;
                            while (rac_mmec.Length < 4)
                            {
                                rac_mmec = "0" + rac_mmec;
                            }
                            result_textBox.Text += "\r\n;\r\n" + "; New SGSN queries the old MME during ISC-RAU from LTE to W/G with Gn/Gp:------------IRAT, and the MMEGroupID mapped to old LAC, MMECode mapped to old RAC!" + "\r\n;\r\n";
                            result_textBox.Text += "rac" + rac_mmec + ".lac" + lac_mmegid + ".mnc" + mnc + ".mcc" + mcc + ".gprs.        IN AAAA " + sgsnip_textBox.Text + "\r\n\r\n";
                        }
                    }
                }

                //DNS config for MME
                if (mmeip_textBox.Text != "")
                {
                    if ((mmegid != "") && (mmec != ""))
                    {
                        if (is_ipv4(mmeip_textBox.Text))
                        {
                            result_textBox.Text += "\r\n;\r\n" + "; Old MME selected based on GUMMEI during inter TAU!" + "\r\n;\r\n";
                            result_textBox.Text += "mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-mme:x-s10" + uetype + "\" \"\" topoff.s10-old-mme01.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.s10-old-mme01.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A " + mmeip_textBox.Text + "\r\n\r\n";
                        }

                        if (is_ipv6(mmeip_textBox.Text))
                        {
                            result_textBox.Text += "\r\n;\r\n" + "; Old MME selected based on GUMMEI during inter TAU!" + "\r\n;\r\n";
                            result_textBox.Text += "mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-mme:x-s10" + uetype + "\" \"\" topoff.s10-old-mme01.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.s10-old-mme01.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA " + mmeip_textBox.Text + "\r\n\r\n";
                        }

                        if (tac_textBox.Text != "")
                        {
                            if (is_ipv4(mmeip_textBox.Text))
                            {
                                result_textBox.Text += "\r\n;\r\n" + "; Source MME selects target MME based on target TAC, used for Inter-LTE S1-HO; Source AMF selects target MME for HO from 5GC to EPC." + "\r\n;\r\n";
                                result_textBox.Text += "tac-lb" + tac_lb + ".tac-hb" + tac_hb + ".tac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-mme:x-s10" + uetype + "\" \"\"    topoff.s10-target-mme01.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.s10-target-mme01.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A " + mmeip_textBox.Text + "\r\n\r\n";
                            }

                            if (is_ipv6(mmeip_textBox.Text))
                            {
                                result_textBox.Text += "\r\n;\r\n" + "; Source MME selects target MME based on target TAC, used for Inter-LTE S1-HO; Source AMF selects target MME for HO from 5GC to EPC." + "\r\n;\r\n";
                                result_textBox.Text += "tac-lb" + tac_lb + ".tac-hb" + tac_hb + ".tac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-mme:x-s10" + uetype + "\" \"\" topoff.s10-target-mme01.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.s10-target-mme01.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA " + mmeip_textBox.Text + "\r\n\r\n";
                            }
                        }
                    }
                }

                //DNS config for AMF
                if (amfip_textBox.Text != "")
                {
                    if ((amfri_textBox.Text != "") && (amfsi_textBox.Text != "") && (amfp_textBox.Text != ""))
                    {
                        if (is_ipv4(amfip_textBox.Text))
                        {
                            result_textBox.Text += "\r\n;\r\n" + "; Old AMF selected based on GUAMI during TAU from 5G to LTE!" + "\r\n;\r\n";
                            result_textBox.Text += "mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-mme:x-s10" + "\" \"\" topoff.s10-mapped-from-old-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.s10-mapped-from-old-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A " + amfip_textBox.Text + "\r\n\r\n";

                            result_textBox.Text += "\r\n;\r\n" + "; Old AMF selected based on GUAMI during TAU from 5G to LTE!" + "\r\n;\r\n";
                            result_textBox.Text += "pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-amf:x-n26" + "\" \"\" topoff.n26-old-amf01.pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.n26-amf01.pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A " + amfip_textBox.Text + "\r\n\r\n";
                        }

                        if (is_ipv6(amfip_textBox.Text))
                        {
                            result_textBox.Text += "\r\n;\r\n" + "; Old AMF selected based on GUAMI during TAU from 5G to LTE!" + "\r\n;\r\n";
                            result_textBox.Text += "mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-mme:x-s10" + "\" \"\" topoff.s10-mapped-from-old-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.s10-mapped-from-old-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA " + amfip_textBox.Text + "\r\n\r\n";


                            result_textBox.Text += "\r\n;\r\n" + "; Old AMF selected based on GUAMI during TAU from 5G to LTE!" + "\r\n;\r\n";
                            result_textBox.Text += "pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-amf:x-n26" + "\" \"\" topoff.n26-old-amf01.pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.n26-old-amf01.pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA " + amfip_textBox.Text + "\r\n\r\n";
                        }

                        if (tac_5g_textBox.Text != "")
                        {
                            if (is_ipv4(amfip_textBox.Text))
                            {
                                result_textBox.Text += "\r\n;\r\n" + "; Source MME selected target AMF based on target TAC, used for HO from LTE to 5G with N26!" + "\r\n;\r\n";
                                result_textBox.Text += "tac-lb" + tac_5gs_lb + ".tac-mb" + tac_5gs_mb + ".tac-hb" + tac_5gs_hb + ".5gstac.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-amf:x-n26" + "\" \"\"    topoff.n26-target-amf01.pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.n26-target-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A " + amfip_textBox.Text + "\r\n\r\n";

                                result_textBox.Text += "\r\n;\r\n" + "; Source MME selected target AMF based on target TAC, used for HO from LTE to 5G, S10 as N26!" + "\r\n;\r\n";
                                result_textBox.Text += "tac-lb" + tac_5gs_lb + ".tac-mb" + tac_5gs_mb + ".tac-hb" + tac_5gs_hb + ".5gstac.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-mme:x-s10" + "\" \"\"    topoff.s10-mapped-from-target-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.s10-mapped-from-target-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A " + amfip_textBox.Text + "\r\n\r\n";
                            }


                            if (is_ipv6(amfip_textBox.Text))
                            {
                                result_textBox.Text += "\r\n;\r\n" + "; Source MME selected target AMF based on target TAC, used for HO from LTE to 5G with N26!" + "\r\n;\r\n";
                                result_textBox.Text += "tac-lb" + tac_5gs_lb + ".tac-mb" + tac_5gs_mb + ".tac-hb" + tac_5gs_hb + ".5gstac.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-amf:x-n26" + "\" \"\"    topoff.n26-target-amf01.pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.n26-target-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA " + amfip_textBox.Text + "\r\n\r\n";


                                result_textBox.Text += "\r\n;\r\n" + "; Source MME selected target AMF based on target TAC, used for HO from LTE to 5G, S10 as N26!" + "\r\n;\r\n";
                                result_textBox.Text += "tac-lb" + tac_5gs_lb + ".tac-mb" + tac_5gs_mb + ".tac-hb" + tac_5gs_hb + ".5gstac.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-mme:x-s10" + "\" \"\" topoff.s10-mapped-from-target-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.s10-mapped-from-target-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA " + amfip_textBox.Text + "\r\n\r\n";
                            }
                        }
                    }
                }

                //save to excel as a DNS design
                if (result_textBox.Text != "")
                {
                    //设置保存文件的格式
                    config_saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
                    //设置默认文件类型显示顺序  
                    config_saveFileDialog.FilterIndex = 2;
                    //保存对话框是否记忆上次打开的目录  
                    config_saveFileDialog.RestoreDirectory = true;
                    config_saveFileDialog.Title = "Save DNS inpu to Excel as Design file";
                    config_saveFileDialog.DefaultExt = "xlsx";

                    config_saveFileDialog.FileName = fileTime + "_DNS_Config_Design.xlsx";
                    if (config_saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //获得文件路径
                        string savedFilePath = config_saveFileDialog.FileName.ToString();

                        using (ExcelPackage excel_package = new ExcelPackage())
                        {

                            //Save input
                            // 添加一个工作表
                            ExcelWorksheet input_excel_worksheet = excel_package.Workbook.Worksheets.Add("DNS Input");
                            // 写入信息到单元格

                            input_excel_worksheet.Cells["A1"].Value = "MCC";
                            input_excel_worksheet.Cells["C1"].Value = "MNC";
                            cell_format(input_excel_worksheet, "A1");
                            cell_format(input_excel_worksheet, "C1");
                            input_excel_worksheet.Cells["B1"].Value = mcc_textBox.Text;
                            input_excel_worksheet.Cells["D1"].Value = mnc_textBox.Text;

                            input_excel_worksheet.Cells["A2"].Value = "LAC";
                            input_excel_worksheet.Cells["C2"].Value = "RAC";
                            cell_format(input_excel_worksheet, "A2");
                            cell_format(input_excel_worksheet, "C2");
                            input_excel_worksheet.Cells["B2"].Value = lac_textBox.Text;
                            input_excel_worksheet.Cells["D2"].Value = rac_textBox.Text;

                            input_excel_worksheet.Cells["A3"].Value = "MME Group ID";
                            input_excel_worksheet.Cells["C3"].Value = "MME Code";
                            cell_format(input_excel_worksheet, "A3");
                            cell_format(input_excel_worksheet, "C3");
                            input_excel_worksheet.Cells["B3"].Value = mmegid_textBox.Text;
                            input_excel_worksheet.Cells["D3"].Value = mmec_textBox.Text;

                            input_excel_worksheet.Cells["A4"].Value = "AMF Region ID";
                            input_excel_worksheet.Cells["C4"].Value = "AMF Set ID";
                            input_excel_worksheet.Cells["E4"].Value = "AMF Point";
                            cell_format(input_excel_worksheet, "A4");
                            cell_format(input_excel_worksheet, "C4");
                            cell_format(input_excel_worksheet, "E4");
                            input_excel_worksheet.Cells["B4"].Value = amfri_textBox.Text;
                            input_excel_worksheet.Cells["D4"].Value = amfsi_textBox.Text;
                            input_excel_worksheet.Cells["F4"].Value = amfp_textBox.Text;

                            input_excel_worksheet.Cells["A5"].Value = "4G TAC";
                            cell_format(input_excel_worksheet, "A5");
                            input_excel_worksheet.Cells["B5"].Value = tac_textBox.Text;


                            input_excel_worksheet.Cells["A6"].Value = "5G TAC";
                            cell_format(input_excel_worksheet, "A6");
                            input_excel_worksheet.Cells["B6"].Value = tac_5g_textBox.Text;

                            input_excel_worksheet.Cells["A7"].Value = "APN/DNN";
                            cell_format(input_excel_worksheet, "A7");
                            input_excel_worksheet.Cells["B7"].Value = apn_textBox.Text;

                            input_excel_worksheet.Cells["A8"].Value = "Topological";
                            cell_format(input_excel_worksheet, "A8");
                            //topon and topoff selection
                            if (topon_radioButton.Checked == true)
                            {
                                input_excel_worksheet.Cells["B8"].Value = topon_radioButton.Text;
                            }
                            else if (topoff_radioButton.Checked == true)
                            {
                                input_excel_worksheet.Cells["B8"].Value = topoff_radioButton.Text;
                            }

                            input_excel_worksheet.Cells["A9"].Value = "NAPTR or SRV";
                            cell_format(input_excel_worksheet, "A9");
                            //topon and topoff selection
                            if (naptr_radioButton.Checked == true)
                            {
                                input_excel_worksheet.Cells["B9"].Value = naptr_radioButton.Text;
                            }
                            else if (srv_radioButton.Checked == true)
                            {
                                input_excel_worksheet.Cells["B9"].Value = srv_radioButton.Text;
                            }

                            input_excel_worksheet.Cells["A10"].Value = "UE Usage Type";
                            cell_format(input_excel_worksheet, "A10");
                            input_excel_worksheet.Cells["B10"].Value = uetype_textBox.Text;

                            input_excel_worksheet.Cells["A11"].Value = "SMF Support";
                            cell_format(input_excel_worksheet, "A11");
                            if (smf_support_checkBox.Checked == true)
                            {
                                input_excel_worksheet.Cells["B11"].Value = "Yes";
                            }

                            input_excel_worksheet.Cells["A12"].Value = "NR Support";
                            cell_format(input_excel_worksheet, "A12");
                            if (nr_support_checkBox.Checked == true)
                            {
                                input_excel_worksheet.Cells["B12"].Value = "Yes";
                            }

                            input_excel_worksheet.Cells["A13"].Value = "SGW S11 IP";
                            cell_format(input_excel_worksheet, "A13");
                            input_excel_worksheet.Cells["B13"].Value = sgwip_textBox.Text;
                            var input_sgw_ip_cellStyle = input_excel_worksheet.Cells["B13"].Style;
                            // 设置文本自动换行
                            input_sgw_ip_cellStyle.WrapText = true;

                            input_excel_worksheet.Cells["A14"].Value = "PGW S5/S8/S2b/S2a/Gn IP";
                            cell_format(input_excel_worksheet, "A14");
                            input_excel_worksheet.Cells["B14"].Value = pgwip_textBox.Text;
                            var input_pgw_ip_cellStyle = input_excel_worksheet.Cells["B14"].Style;
                            // 设置文本自动换行
                            input_pgw_ip_cellStyle.WrapText = true;

                            input_excel_worksheet.Cells["A15"].Value = "SGSN Gn IP";
                            cell_format(input_excel_worksheet, "A15");
                            input_excel_worksheet.Cells["B15"].Value = sgsnip_textBox.Text;

                            input_excel_worksheet.Cells["A16"].Value = "MME S10 IP";
                            cell_format(input_excel_worksheet, "A16");
                            input_excel_worksheet.Cells["B16"].Value = mmeip_textBox.Text;

                            input_excel_worksheet.Cells["A17"].Value = "AMF N26 IP";
                            cell_format(input_excel_worksheet, "A17");
                            input_excel_worksheet.Cells["B17"].Value = amfip_textBox.Text;


                            // 删除空行
                            DeleteEmptyRows(input_excel_worksheet);

                            // 对行和列进行自动适配
                            input_excel_worksheet.Cells.AutoFitColumns();

                            //DNS config for SGW
                            if (sgwip_textBox.Text != "")
                            {
                                if (naptr_radioButton.Checked == true)
                                {
                                    // 添加一个工作表
                                    ExcelWorksheet excel_worksheet = excel_package.Workbook.Worksheets.Add("SGW DNS Design");
                                    // 写入信息到单元格
                                    excel_worksheet.Cells["A1"].Value = "Record Type";
                                    excel_worksheet.Cells["B1"].Value = "FQDN";
                                    excel_worksheet.Cells["C1"].Value = "Order";
                                    excel_worksheet.Cells["D1"].Value = "Pref";
                                    excel_worksheet.Cells["E1"].Value = "Flag";
                                    excel_worksheet.Cells["F1"].Value = "Service Name";
                                    excel_worksheet.Cells["G1"].Value = "Replacement";
                                    excel_worksheet.Cells["H1"].Value = "Remark";

                                    excel_worksheet.Cells["A" + (3 * sgwip_textBox.Lines.Length + 1).ToString()].Value = "Record Type";
                                    excel_worksheet.Cells["B" + (3 * sgwip_textBox.Lines.Length + 1).ToString()].Value = "FQDN";
                                    excel_worksheet.Cells["C" + (3 * sgwip_textBox.Lines.Length + 1).ToString()].Value = "IP address";
                                    excel_worksheet.Cells["D" + (3 * sgwip_textBox.Lines.Length + 1).ToString()].Value = "Comment";

                                    for (char c = 'A'; c <= 'H'; c++)
                                    {
                                        // 获取单元格 A1 的样式对象
                                        var cellStyle = excel_worksheet.Cells[c + "1"].Style;

                                        // 设置字体加粗
                                        cellStyle.Font.Bold = true;

                                        // 设置背景颜色
                                        cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                        cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                        // 设置字体颜色
                                        cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                    }

                                    for (char c = 'A'; c <= 'D'; c++)
                                    {
                                        // 获取单元格 A1 的样式对象
                                        var cellStyle = excel_worksheet.Cells[c + (3 * sgwip_textBox.Lines.Length + 1).ToString()].Style;

                                        // 设置字体加粗
                                        cellStyle.Font.Bold = true;

                                        // 设置背景颜色
                                        cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                        cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                        // 设置字体颜色
                                        cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                    }

                                    if ((tac_hb != "") && (tac_lb != ""))
                                    {
                                        for (int i = 0; i < sgwip_textBox.Lines.Length; i++)
                                        {
                                            if (is_ipv4(sgwip_textBox.Lines[i]) || is_ipv6(sgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (i + 2).ToString()].Value = "NAPTR";
                                                excel_worksheet.Cells["B" + (i + 2).ToString()].Value = "tac-lb" + tac_lb + ".tac-hb" + tac_hb + ".tac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (i + 2).ToString()].Value = "10";
                                                excel_worksheet.Cells["D" + (i + 2).ToString()].Value = "10";
                                                excel_worksheet.Cells["E" + (i + 2).ToString()].Value = "a";
                                                excel_worksheet.Cells["F" + (i + 2).ToString()].Value = "x-3gpp-sgw:x-s5-gtp" + uetype + sgw_network_capacity + ":x-s8-gtp" + uetype + sgw_network_capacity;
                                                excel_worksheet.Cells["G" + (i + 2).ToString()].Value = topological + ".sgw-s5s8.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["H2"].Value = "";

                                                excel_worksheet.Cells["A" + (2 * sgwip_textBox.Lines.Length + i).ToString()].Value = "NAPTR";
                                                excel_worksheet.Cells["B" + (2 * sgwip_textBox.Lines.Length + i).ToString()].Value = "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (2 * sgwip_textBox.Lines.Length + i).ToString()].Value = "10";
                                                excel_worksheet.Cells["D" + (2 * sgwip_textBox.Lines.Length + i).ToString()].Value = "10";
                                                excel_worksheet.Cells["E" + (2 * sgwip_textBox.Lines.Length + i).ToString()].Value = "a";
                                                excel_worksheet.Cells["F" + (2 * sgwip_textBox.Lines.Length + i).ToString()].Value = "x-3gpp-sgw:x-s11" + uetype + sgw_network_capacity;
                                                excel_worksheet.Cells["G" + (2 * sgwip_textBox.Lines.Length + i).ToString()].Value = topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["H" + (2 * sgwip_textBox.Lines.Length + i).ToString()].Value = "";

                                            }

                                            if (is_ipv4(sgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (3 * sgwip_textBox.Lines.Length + 2 + i).ToString()].Value = "A";
                                                excel_worksheet.Cells["B" + (3 * sgwip_textBox.Lines.Length + 2 + i).ToString()].Value = topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (3 * sgwip_textBox.Lines.Length + 2 + i).ToString()].Value = sgwip_textBox.Lines[i];
                                                excel_worksheet.Cells["D" + (3 * sgwip_textBox.Lines.Length + 2 + i).ToString()].Value = "";
                                            }
                                            if (is_ipv6(sgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (3 * sgwip_textBox.Lines.Length + 2 + i).ToString()].Value = "AAAA";
                                                excel_worksheet.Cells["B" + (3 * sgwip_textBox.Lines.Length + 2 + i).ToString()].Value = topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (3 * sgwip_textBox.Lines.Length + 2 + i).ToString()].Value = sgwip_textBox.Lines[i];
                                                excel_worksheet.Cells["D" + (3 * sgwip_textBox.Lines.Length + 2 + i).ToString()].Value = "";
                                            }
                                        }

                                        // 删除空行
                                        DeleteEmptyRows(excel_worksheet);

                                        // 对行和列进行自动适配
                                        excel_worksheet.Cells.AutoFitColumns();

                                    }
                                }
                                if (srv_radioButton.Checked == true)
                                {
                                    // 添加一个工作表 priority, weight, port
                                    ExcelWorksheet excel_worksheet = excel_package.Workbook.Worksheets.Add("SGW DNS Design");
                                    // 写入信息到单元格
                                    excel_worksheet.Cells["A1"].Value = "Record Type";
                                    excel_worksheet.Cells["B1"].Value = "FQDN";
                                    excel_worksheet.Cells["C1"].Value = "Order/Priority";
                                    excel_worksheet.Cells["D1"].Value = "Pref/Weight";
                                    excel_worksheet.Cells["E1"].Value = "Flag/Port";
                                    excel_worksheet.Cells["F1"].Value = "Service Name";
                                    excel_worksheet.Cells["G1"].Value = "Replacement";
                                    excel_worksheet.Cells["H1"].Value = "Remark";

                                    excel_worksheet.Cells["A" + (3 * sgwip_textBox.Lines.Length + 2).ToString()].Value = "Record Type";
                                    excel_worksheet.Cells["B" + (3 * sgwip_textBox.Lines.Length + 2).ToString()].Value = "FQDN";
                                    excel_worksheet.Cells["C" + (3 * sgwip_textBox.Lines.Length + 2).ToString()].Value = "IP address";
                                    excel_worksheet.Cells["D" + (3 * sgwip_textBox.Lines.Length + 2).ToString()].Value = "Comment";


                                    for (char c = 'A'; c <= 'H'; c++)
                                    {
                                        // 获取单元格 A1 的样式对象
                                        var cellStyle = excel_worksheet.Cells[c + "1"].Style;

                                        // 设置字体加粗
                                        cellStyle.Font.Bold = true;

                                        // 设置背景颜色
                                        cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                        cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                        // 设置字体颜色
                                        cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                    }

                                    for (char c = 'A'; c <= 'D'; c++)
                                    {
                                        // 获取单元格 A1 的样式对象
                                        var cellStyle = excel_worksheet.Cells[c + (3 * sgwip_textBox.Lines.Length + 2).ToString()].Style;

                                        // 设置字体加粗
                                        cellStyle.Font.Bold = true;

                                        // 设置背景颜色
                                        cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                        cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                        // 设置字体颜色
                                        cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                    }

                                    if ((tac_hb != "") && (tac_lb != ""))
                                    {
                                        for (int i = 0; i < sgwip_textBox.Lines.Length; i++)
                                        {
                                            if (is_ipv4(sgwip_textBox.Lines[i]) || is_ipv6(sgwip_textBox.Lines[i]))
                                            {
                                                if (srv_radioButton.Checked == true)
                                                {
                                                    excel_worksheet.Cells["A2"].Value = "NAPTR";
                                                    excel_worksheet.Cells["B2"].Value = "tac-lb" + tac_lb + ".tac-hb" + tac_hb + ".tac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                    excel_worksheet.Cells["C2"].Value = "10";
                                                    excel_worksheet.Cells["D2"].Value = "10";
                                                    excel_worksheet.Cells["E2"].Value = "s";
                                                    excel_worksheet.Cells["F2"].Value = "x-3gpp-sgw:x-s5-gtp" + uetype + sgw_network_capacity + ":x-s8-gtp" + uetype + sgw_network_capacity;
                                                    excel_worksheet.Cells["G2"].Value = "sgw-list" + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                    excel_worksheet.Cells["H2"].Value = "";
                                                }
                                                break;
                                            }
                                        }

                                        for (int i = 0; i < sgwip_textBox.Lines.Length; i++)
                                        {
                                            if (is_ipv4(sgwip_textBox.Lines[i]) || is_ipv6(sgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (i + 3).ToString()].Value = "SRV";
                                                excel_worksheet.Cells["B" + (i + 3).ToString()].Value = "sgw-list" + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (i + 3).ToString()].Value = "100";
                                                excel_worksheet.Cells["D" + (i + 3).ToString()].Value = "100";
                                                excel_worksheet.Cells["E" + (i + 3).ToString()].Value = "2123";
                                                excel_worksheet.Cells["F" + (i + 3).ToString()].Value = "";
                                                excel_worksheet.Cells["G" + (i + 3).ToString()].Value = topological + ".sgw-s5s8.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["H2"].Value = "";

                                                excel_worksheet.Cells["A" + (2 * sgwip_textBox.Lines.Length + 1 + i).ToString()].Value = "NAPTR";
                                                excel_worksheet.Cells["B" + (2 * sgwip_textBox.Lines.Length + 1 + i).ToString()].Value = "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (2 * sgwip_textBox.Lines.Length + 1 + i).ToString()].Value = "10";
                                                excel_worksheet.Cells["D" + (2 * sgwip_textBox.Lines.Length + 1 + i).ToString()].Value = "10";
                                                excel_worksheet.Cells["E" + (2 * sgwip_textBox.Lines.Length + 1 + i).ToString()].Value = "a";
                                                excel_worksheet.Cells["F" + (2 * sgwip_textBox.Lines.Length + 1 + i).ToString()].Value = "x-3gpp-sgw:x-s11" + uetype + sgw_network_capacity;
                                                excel_worksheet.Cells["G" + (2 * sgwip_textBox.Lines.Length + 1 + i).ToString()].Value = topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["H" + (2 * sgwip_textBox.Lines.Length + 1 + i).ToString()].Value = "";

                                            }

                                            if (is_ipv4(sgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (3 * sgwip_textBox.Lines.Length + 3 + i).ToString()].Value = "A";
                                                excel_worksheet.Cells["B" + (3 * sgwip_textBox.Lines.Length + 3 + i).ToString()].Value = topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (3 * sgwip_textBox.Lines.Length + 3 + i).ToString()].Value = sgwip_textBox.Lines[i];
                                                excel_worksheet.Cells["D" + (3 * sgwip_textBox.Lines.Length + 3 + i).ToString()].Value = "";
                                            }
                                            if (is_ipv6(sgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (3 * sgwip_textBox.Lines.Length + 3 + i).ToString()].Value = "AAAA";
                                                excel_worksheet.Cells["B" + (3 * sgwip_textBox.Lines.Length + 3 + i).ToString()].Value = topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (3 * sgwip_textBox.Lines.Length + 3 + i).ToString()].Value = sgwip_textBox.Lines[i];
                                                excel_worksheet.Cells["D" + (3 * sgwip_textBox.Lines.Length + 3 + i).ToString()].Value = "";
                                            }
                                        }

                                        // 删除空行
                                        DeleteEmptyRows(excel_worksheet);

                                        // 对行和列进行自动适配
                                        excel_worksheet.Cells.AutoFitColumns();

                                    }
                                }
                            }

                            //DNS config for PGW
                            if (pgwip_textBox.Text != "")
                            {
                                if (naptr_radioButton.Checked == true)
                                {
                                    // 添加一个工作表
                                    ExcelWorksheet excel_worksheet = excel_package.Workbook.Worksheets.Add("PGW DNS Design");
                                    // 写入信息到单元格
                                    excel_worksheet.Cells["A1"].Value = "Record Type";
                                    excel_worksheet.Cells["B1"].Value = "FQDN";
                                    excel_worksheet.Cells["C1"].Value = "Order";
                                    excel_worksheet.Cells["D1"].Value = "Pref";
                                    excel_worksheet.Cells["E1"].Value = "Flag";
                                    excel_worksheet.Cells["F1"].Value = "Service Name";
                                    excel_worksheet.Cells["G1"].Value = "Replacement";
                                    excel_worksheet.Cells["H1"].Value = "Remark";

                                    excel_worksheet.Cells["A" + (3 * pgwip_textBox.Lines.Length + 1).ToString()].Value = "Record Type";
                                    excel_worksheet.Cells["B" + (3 * pgwip_textBox.Lines.Length + 1).ToString()].Value = "FQDN";
                                    excel_worksheet.Cells["C" + (3 * pgwip_textBox.Lines.Length + 1).ToString()].Value = "IP address";
                                    excel_worksheet.Cells["D" + (3 * pgwip_textBox.Lines.Length + 1).ToString()].Value = "Comment";

                                    for (char c = 'A'; c <= 'H'; c++)
                                    {
                                        // 获取单元格 A1 的样式对象
                                        var cellStyle = excel_worksheet.Cells[c + "1"].Style;

                                        // 设置字体加粗
                                        cellStyle.Font.Bold = true;

                                        // 设置背景颜色
                                        cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                        cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                        // 设置字体颜色
                                        cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                    }

                                    for (char c = 'A'; c <= 'D'; c++)
                                    {
                                        // 获取单元格 A1 的样式对象
                                        var cellStyle = excel_worksheet.Cells[c + (3 * pgwip_textBox.Lines.Length + 1).ToString()].Style;

                                        // 设置字体加粗
                                        cellStyle.Font.Bold = true;

                                        // 设置背景颜色
                                        cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                        cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                        // 设置字体颜色
                                        cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                    }

                                    if (apn != "")
                                    {
                                        for (int i = 0; i < pgwip_textBox.Lines.Length; i++)
                                        {
                                            if (is_ipv4(pgwip_textBox.Lines[i]) || is_ipv6(pgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (i + 2).ToString()].Value = "NAPTR";
                                                excel_worksheet.Cells["B" + (i + 2).ToString()].Value = apn + ".apn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (i + 2).ToString()].Value = "10";
                                                excel_worksheet.Cells["D" + (i + 2).ToString()].Value = "10";
                                                excel_worksheet.Cells["E" + (i + 2).ToString()].Value = "a";
                                                excel_worksheet.Cells["F" + (i + 2).ToString()].Value = "x-3gpp-pgw:x-s5-gtp" + network_capacity + uetype + ":x-s8-gtp" + network_capacity + uetype + ":x-s2b-gtp:x-s2a-gtp:x-gn:x-gp";
                                                excel_worksheet.Cells["G" + (i + 2).ToString()].Value = topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["H" + (i + 3).ToString()].Value = "";

                                                excel_worksheet.Cells["A" + (2 * pgwip_textBox.Lines.Length + i).ToString()].Value = "NAPTR";
                                                excel_worksheet.Cells["B" + (2 * pgwip_textBox.Lines.Length + i).ToString()].Value = "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (2 * pgwip_textBox.Lines.Length + i).ToString()].Value = "10";
                                                excel_worksheet.Cells["D" + (2 * pgwip_textBox.Lines.Length + i).ToString()].Value = "10";
                                                excel_worksheet.Cells["E" + (2 * pgwip_textBox.Lines.Length + i).ToString()].Value = "a";
                                                excel_worksheet.Cells["F" + (2 * pgwip_textBox.Lines.Length + i).ToString()].Value = "x-3gpp-pgw:x-s5-gtp" + network_capacity + uetype + ":x-s8-gtp" + network_capacity + uetype + ":x-s2b-gtp:x-s2a-gtp:x-gn:x-gp";
                                                excel_worksheet.Cells["G" + (2 * pgwip_textBox.Lines.Length + i).ToString()].Value = topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["H" + (2 * pgwip_textBox.Lines.Length + i).ToString()].Value = "";

                                            }

                                            if (is_ipv4(pgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (3 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = "A";
                                                excel_worksheet.Cells["B" + (3 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (3 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = pgwip_textBox.Lines[i];
                                                excel_worksheet.Cells["D" + (3 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = "";
                                            }
                                            if (is_ipv6(pgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (3 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = "AAAA";
                                                excel_worksheet.Cells["B" + (3 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (3 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = pgwip_textBox.Lines[i];
                                                excel_worksheet.Cells["D" + (3 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = "";
                                            }

                                            ///2G and 3G GRPS
                                            if (is_ipv4(pgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (4 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = "A";
                                                excel_worksheet.Cells["B" + (4 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = apn + ".mnc" + mnc + ".mcc" + mcc + ".gprs.";
                                                excel_worksheet.Cells["C" + (4 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = pgwip_textBox.Lines[i];
                                                excel_worksheet.Cells["D" + (4 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = "";
                                            }
                                            if (is_ipv6(pgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (4 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = "AAAA";
                                                excel_worksheet.Cells["B" + (4 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = apn + ".mnc" + mnc + ".mcc" + mcc + ".gprs.";
                                                excel_worksheet.Cells["C" + (4 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = pgwip_textBox.Lines[i];
                                                excel_worksheet.Cells["D" + (4 * pgwip_textBox.Lines.Length + 2 + i).ToString()].Value = "";
                                            }

                                        }

                                        // 删除空行
                                        DeleteEmptyRows(excel_worksheet);

                                        // 对行和列进行自动适配
                                        excel_worksheet.Cells.AutoFitColumns();
                                    }
                                }
                                if (srv_radioButton.Checked == true)
                                {
                                    // 添加一个工作表 priority, weight, port
                                    ExcelWorksheet excel_worksheet = excel_package.Workbook.Worksheets.Add("PGW DNS Design");
                                    // 写入信息到单元格
                                    excel_worksheet.Cells["A1"].Value = "Record Type";
                                    excel_worksheet.Cells["B1"].Value = "FQDN";
                                    excel_worksheet.Cells["C1"].Value = "Order/Priority";
                                    excel_worksheet.Cells["D1"].Value = "Pref/Weight";
                                    excel_worksheet.Cells["E1"].Value = "Flag/Port";
                                    excel_worksheet.Cells["F1"].Value = "Service Name";
                                    excel_worksheet.Cells["G1"].Value = "Replacement";
                                    excel_worksheet.Cells["H1"].Value = "Remark";

                                    excel_worksheet.Cells["A" + (3 * pgwip_textBox.Lines.Length + 2).ToString()].Value = "Record Type";
                                    excel_worksheet.Cells["B" + (3 * pgwip_textBox.Lines.Length + 2).ToString()].Value = "FQDN";
                                    excel_worksheet.Cells["C" + (3 * pgwip_textBox.Lines.Length + 2).ToString()].Value = "IP address";
                                    excel_worksheet.Cells["D" + (3 * pgwip_textBox.Lines.Length + 2).ToString()].Value = "Comment";


                                    for (char c = 'A'; c <= 'H'; c++)
                                    {
                                        // 获取单元格 A1 的样式对象
                                        var cellStyle = excel_worksheet.Cells[c + "1"].Style;

                                        // 设置字体加粗
                                        cellStyle.Font.Bold = true;

                                        // 设置背景颜色
                                        cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                        cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                        // 设置字体颜色
                                        cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                    }

                                    for (char c = 'A'; c <= 'D'; c++)
                                    {
                                        // 获取单元格 A1 的样式对象
                                        var cellStyle = excel_worksheet.Cells[c + (3 * pgwip_textBox.Lines.Length + 2).ToString()].Style;

                                        // 设置字体加粗
                                        cellStyle.Font.Bold = true;

                                        // 设置背景颜色
                                        cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                        cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                        // 设置字体颜色
                                        cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                    }

                                    if (apn != "")
                                    {
                                        for (int i = 0; i < pgwip_textBox.Lines.Length; i++)
                                        {
                                            if (is_ipv4(pgwip_textBox.Lines[i]) || is_ipv6(pgwip_textBox.Lines[i]))
                                            {
                                                if (srv_radioButton.Checked == true)
                                                {
                                                    excel_worksheet.Cells["A2"].Value = "NAPTR";
                                                    excel_worksheet.Cells["B2"].Value = apn + ".apn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                    excel_worksheet.Cells["C2"].Value = "10";
                                                    excel_worksheet.Cells["D2"].Value = "10";
                                                    excel_worksheet.Cells["E2"].Value = "s";
                                                    excel_worksheet.Cells["F2"].Value = "x-3gpp-pgw:x-s5-gtp" + network_capacity + uetype + ":x-s8-gtp" + network_capacity + uetype + ":x-s2b-gtp:x-s2a-gtp:x-gn:x-gp";
                                                    excel_worksheet.Cells["G2"].Value = "pgw-list" + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                    excel_worksheet.Cells["H2"].Value = "";
                                                }
                                                break;
                                            }
                                        }

                                        for (int i = 0; i < pgwip_textBox.Lines.Length; i++)
                                        {
                                            if (is_ipv4(pgwip_textBox.Lines[i]) || is_ipv6(pgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (i + 3).ToString()].Value = "SRV";
                                                excel_worksheet.Cells["B" + (i + 3).ToString()].Value = "pgw-list" + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (i + 3).ToString()].Value = "100";
                                                excel_worksheet.Cells["D" + (i + 3).ToString()].Value = "100";
                                                excel_worksheet.Cells["E" + (i + 3).ToString()].Value = "2123";
                                                excel_worksheet.Cells["F" + (i + 3).ToString()].Value = "";
                                                excel_worksheet.Cells["G" + (i + 3).ToString()].Value = topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["H" + (i + 3).ToString()].Value = "";

                                                excel_worksheet.Cells["A" + (2 * pgwip_textBox.Lines.Length + 1 + i).ToString()].Value = "NAPTR";
                                                excel_worksheet.Cells["B" + (2 * pgwip_textBox.Lines.Length + 1 + i).ToString()].Value = "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (2 * pgwip_textBox.Lines.Length + 1 + i).ToString()].Value = "10";
                                                excel_worksheet.Cells["D" + (2 * pgwip_textBox.Lines.Length + 1 + i).ToString()].Value = "10";
                                                excel_worksheet.Cells["E" + (2 * pgwip_textBox.Lines.Length + 1 + i).ToString()].Value = "a";
                                                excel_worksheet.Cells["F" + (2 * pgwip_textBox.Lines.Length + 1 + i).ToString()].Value = "x-3gpp-pgw:x-s5-gtp" + network_capacity + uetype + ":x-s8-gtp" + network_capacity + uetype + ":x-s2b-gtp:x-s2a-gtp:x-gn:x-gp";
                                                excel_worksheet.Cells["G" + (2 * pgwip_textBox.Lines.Length + 1 + i).ToString()].Value = topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["H" + (2 * pgwip_textBox.Lines.Length + 1 + i).ToString()].Value = "";

                                            }

                                            if (is_ipv4(pgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (3 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = "A";
                                                excel_worksheet.Cells["B" + (3 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (3 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = pgwip_textBox.Lines[i];
                                                excel_worksheet.Cells["D" + (3 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = "";
                                            }
                                            if (is_ipv6(pgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (3 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = "AAAA";
                                                excel_worksheet.Cells["B" + (3 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                                excel_worksheet.Cells["C" + (3 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = pgwip_textBox.Lines[i];
                                                excel_worksheet.Cells["D" + (3 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = "";
                                            }

                                            ///2G and 3G GRPS
                                            if (is_ipv4(pgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (4 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = "A";
                                                excel_worksheet.Cells["B" + (4 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = apn + ".mnc" + mnc + ".mcc" + mcc + ".gprs.";
                                                excel_worksheet.Cells["C" + (4 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = pgwip_textBox.Lines[i];
                                                excel_worksheet.Cells["D" + (4 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = "";
                                            }
                                            if (is_ipv6(pgwip_textBox.Lines[i]))
                                            {
                                                excel_worksheet.Cells["A" + (4 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = "AAAA";
                                                excel_worksheet.Cells["B" + (4 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = apn + ".mnc" + mnc + ".mcc" + mcc + ".gprs.";
                                                excel_worksheet.Cells["C" + (4 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = pgwip_textBox.Lines[i];
                                                excel_worksheet.Cells["D" + (4 * pgwip_textBox.Lines.Length + 3 + i).ToString()].Value = "";
                                            }
                                        }

                                        // 删除空行
                                        DeleteEmptyRows(excel_worksheet);

                                        // 对行和列进行自动适配
                                        excel_worksheet.Cells.AutoFitColumns();
                                    }
                                }
                            }

                            //DNS config for SGSN
                            if (sgsnip_textBox.Text != "")
                            {
                                if ((lac != "") && (rac != ""))
                                {
                                    if (is_ipv4(sgsnip_textBox.Text))
                                    {
                                        // 添加一个工作表
                                        ExcelWorksheet excel_worksheet = excel_package.Workbook.Worksheets.Add("SGSN DNS Design");
                                        // 写入信息到单元格
                                        excel_worksheet.Cells["A1"].Value = "Record Type";
                                        excel_worksheet.Cells["B1"].Value = "FQDN";
                                        excel_worksheet.Cells["C1"].Value = "Order";
                                        excel_worksheet.Cells["D1"].Value = "Pref";
                                        excel_worksheet.Cells["E1"].Value = "Flag";
                                        excel_worksheet.Cells["F1"].Value = "Service Name";
                                        excel_worksheet.Cells["G1"].Value = "Replacement";
                                        excel_worksheet.Cells["H1"].Value = "Remark";

                                        excel_worksheet.Cells["A3"].Value = "Record Type";
                                        excel_worksheet.Cells["B3"].Value = "FQDN";
                                        excel_worksheet.Cells["C3"].Value = "IP address";
                                        excel_worksheet.Cells["D3"].Value = "Comment";

                                        for (char c = 'A'; c <= 'H'; c++)
                                        {
                                            // 获取单元格 A1 的样式对象
                                            var cellStyle = excel_worksheet.Cells[c + "1"].Style;

                                            // 设置字体加粗
                                            cellStyle.Font.Bold = true;

                                            // 设置背景颜色
                                            cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                            cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                            // 设置字体颜色
                                            cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                        }

                                        for (char c = 'A'; c <= 'D'; c++)
                                        {
                                            // 获取单元格 A1 的样式对象
                                            var cellStyle = excel_worksheet.Cells[c + "3"].Style;

                                            // 设置字体加粗
                                            cellStyle.Font.Bold = true;

                                            // 设置背景颜色
                                            cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                            cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                            // 设置字体颜色
                                            cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                        }



                                        excel_worksheet.Cells["A2"].Value = "NAPTR";
                                        excel_worksheet.Cells["B2"].Value = "rac" + rac + ".lac" + lac + ".rac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["C2"].Value = "10";
                                        excel_worksheet.Cells["D2"].Value = "10";
                                        excel_worksheet.Cells["E2"].Value = "a";
                                        excel_worksheet.Cells["F2"].Value = "x-3gpp-sgsn:x-gn" + uetype + ":x-gp" + uetype;
                                        excel_worksheet.Cells["G2"].Value = "topoff.gn-target-sgsn01.sgsn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["H2"].Value = "";


                                        excel_worksheet.Cells["A4"].Value = "A";
                                        excel_worksheet.Cells["B4"].Value = "topoff.gn-target-sgsn01.sgsn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["C4"].Value = sgsnip_textBox.Text;
                                        excel_worksheet.Cells["D4"].Value = "";


                                        excel_worksheet.Cells["A5"].Value = "A";
                                        excel_worksheet.Cells["B5"].Value = "rac" + rac + ".lac" + lac + ".mnc" + mnc + ".mcc" + mcc + ".gprs.";
                                        excel_worksheet.Cells["C5"].Value = sgsnip_textBox.Text;
                                        excel_worksheet.Cells["D5"].Value = "";

                                        // 对行和列进行自动适配
                                        excel_worksheet.Cells.AutoFitColumns();
                                    }

                                    if (is_ipv6(sgsnip_textBox.Text))
                                    {
                                        // 添加一个工作表
                                        ExcelWorksheet excel_worksheet = excel_package.Workbook.Worksheets.Add("SGSN DNS Design");
                                        // 写入信息到单元格
                                        excel_worksheet.Cells["A1"].Value = "Record Type";
                                        excel_worksheet.Cells["B1"].Value = "FQDN";
                                        excel_worksheet.Cells["C1"].Value = "Order";
                                        excel_worksheet.Cells["D1"].Value = "Pref";
                                        excel_worksheet.Cells["E1"].Value = "Flag";
                                        excel_worksheet.Cells["F1"].Value = "Service Name";
                                        excel_worksheet.Cells["G1"].Value = "Replacement";
                                        excel_worksheet.Cells["H1"].Value = "Remark";

                                        excel_worksheet.Cells["A3"].Value = "Record Type";
                                        excel_worksheet.Cells["B3"].Value = "FQDN";
                                        excel_worksheet.Cells["C3"].Value = "IP address";
                                        excel_worksheet.Cells["D3"].Value = "Comment";

                                        for (char c = 'A'; c <= 'H'; c++)
                                        {
                                            // 获取单元格 A1 的样式对象
                                            var cellStyle = excel_worksheet.Cells[c + "1"].Style;

                                            // 设置字体加粗
                                            cellStyle.Font.Bold = true;

                                            // 设置背景颜色
                                            cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                            cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                            // 设置字体颜色
                                            cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                        }

                                        for (char c = 'A'; c <= 'D'; c++)
                                        {
                                            // 获取单元格 A1 的样式对象
                                            var cellStyle = excel_worksheet.Cells[c + "3"].Style;

                                            // 设置字体加粗
                                            cellStyle.Font.Bold = true;

                                            // 设置背景颜色
                                            cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                            cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                            // 设置字体颜色
                                            cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                        }



                                        excel_worksheet.Cells["A2"].Value = "NAPTR";
                                        excel_worksheet.Cells["B2"].Value = "rac" + rac + ".lac" + lac + ".rac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["C2"].Value = "10";
                                        excel_worksheet.Cells["D2"].Value = "10";
                                        excel_worksheet.Cells["E2"].Value = "a";
                                        excel_worksheet.Cells["F2"].Value = "x-3gpp-sgsn:x-gn" + uetype + ":x-gp" + uetype;
                                        excel_worksheet.Cells["G2"].Value = "topoff.gn-target-sgsn01.sgsn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["H2"].Value = "";


                                        excel_worksheet.Cells["A4"].Value = "AAAA";
                                        excel_worksheet.Cells["B4"].Value = "topoff.gn-target-sgsn01.sgsn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["C4"].Value = sgsnip_textBox.Text;
                                        excel_worksheet.Cells["D4"].Value = "";


                                        excel_worksheet.Cells["A5"].Value = "AAAA";
                                        excel_worksheet.Cells["B5"].Value = "rac" + rac + ".lac" + lac + ".mnc" + mnc + ".mcc" + mcc + ".gprs.";
                                        excel_worksheet.Cells["C5"].Value = sgsnip_textBox.Text;
                                        excel_worksheet.Cells["D5"].Value = "";

                                        // 对行和列进行自动适配
                                        excel_worksheet.Cells.AutoFitColumns();

                                    }
                                }
                            }

                            //DNS config for MME
                            if (mmeip_textBox.Text != "")
                            {
                                if ((mmegid != "") && (mmec != ""))
                                {
                                    // 添加一个工作表
                                    ExcelWorksheet excel_worksheet = excel_package.Workbook.Worksheets.Add("MME DNS Design");
                                    // 写入信息到单元格
                                    excel_worksheet.Cells["A1"].Value = "Record Type";
                                    excel_worksheet.Cells["B1"].Value = "FQDN";
                                    excel_worksheet.Cells["C1"].Value = "Order";
                                    excel_worksheet.Cells["D1"].Value = "Pref";
                                    excel_worksheet.Cells["E1"].Value = "Flag";
                                    excel_worksheet.Cells["F1"].Value = "Service Name";
                                    excel_worksheet.Cells["G1"].Value = "Replacement";
                                    excel_worksheet.Cells["H1"].Value = "Remark";

                                    excel_worksheet.Cells["A5"].Value = "Record Type";
                                    excel_worksheet.Cells["B5"].Value = "FQDN";
                                    excel_worksheet.Cells["C5"].Value = "IP address";
                                    excel_worksheet.Cells["D5"].Value = "Comment";

                                    for (char c = 'A'; c <= 'H'; c++)
                                    {
                                        // 获取单元格 A1 的样式对象
                                        var cellStyle = excel_worksheet.Cells[c + "1"].Style;

                                        // 设置字体加粗
                                        cellStyle.Font.Bold = true;

                                        // 设置背景颜色
                                        cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                        cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                        // 设置字体颜色
                                        cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                    }

                                    for (char c = 'A'; c <= 'D'; c++)
                                    {
                                        // 获取单元格 A1 的样式对象
                                        var cellStyle = excel_worksheet.Cells[c + "5"].Style;

                                        // 设置字体加粗
                                        cellStyle.Font.Bold = true;

                                        // 设置背景颜色
                                        cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                        cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                        // 设置字体颜色
                                        cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                    }

                                    excel_worksheet.Cells["A2"].Value = "NAPTR";
                                    excel_worksheet.Cells["B2"].Value = "mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                    excel_worksheet.Cells["C2"].Value = "10";
                                    excel_worksheet.Cells["D2"].Value = "10";
                                    excel_worksheet.Cells["E2"].Value = "a";
                                    excel_worksheet.Cells["F2"].Value = "x-3gpp-mme:x-s10" + uetype;
                                    excel_worksheet.Cells["G2"].Value = "topoff.s10-old-mme01.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                    excel_worksheet.Cells["H2"].Value = "";
                                    if (is_ipv4(mmeip_textBox.Text))
                                    {
                                        excel_worksheet.Cells["A6"].Value = "A";
                                        excel_worksheet.Cells["B6"].Value = "topoff.s10-old-mme01.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["C6"].Value = mmeip_textBox.Text;
                                        excel_worksheet.Cells["D6"].Value = "";
                                    }

                                    if (is_ipv6(mmeip_textBox.Text))
                                    {
                                        excel_worksheet.Cells["A6"].Value = "AAAA";
                                        excel_worksheet.Cells["B6"].Value = "topoff.s10-old-mme01.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["C6"].Value = mmeip_textBox.Text;
                                        excel_worksheet.Cells["D6"].Value = "";
                                    }


                                    if (tac_textBox.Text != "")
                                    {

                                        excel_worksheet.Cells["A3"].Value = "NAPTR";
                                        excel_worksheet.Cells["B3"].Value = "tac-lb" + tac_lb + ".tac-hb" + tac_hb + ".tac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["C3"].Value = "10";
                                        excel_worksheet.Cells["D3"].Value = "10";
                                        excel_worksheet.Cells["E3"].Value = "a";
                                        excel_worksheet.Cells["F3"].Value = "x-3gpp-mme:x-s10" + uetype;
                                        excel_worksheet.Cells["G3"].Value = "topoff.s10-target-mme01.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["H3"].Value = "";

                                        if (is_ipv4(mmeip_textBox.Text))
                                        {
                                            excel_worksheet.Cells["A7"].Value = "A";
                                            excel_worksheet.Cells["B7"].Value = "topoff.s10-target-mme01.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                            excel_worksheet.Cells["C7"].Value = mmeip_textBox.Text;
                                            excel_worksheet.Cells["D7"].Value = "";
                                        }

                                        if (is_ipv6(mmeip_textBox.Text))
                                        {
                                            excel_worksheet.Cells["A7"].Value = "AAAA";
                                            excel_worksheet.Cells["B7"].Value = "topoff.s10-target-mme01.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                            excel_worksheet.Cells["C7"].Value = mmeip_textBox.Text;
                                            excel_worksheet.Cells["D7"].Value = "";
                                        }
                                    }


                                    string lac_mmegid = mmegid;
                                    string rac_mmec = mmec;
                                    while (rac_mmec.Length < 4)
                                    {
                                        rac_mmec = "0" + rac_mmec;
                                    }

                                    if (is_ipv4(sgsnip_textBox.Text))
                                    {
                                        excel_worksheet.Cells["A8"].Value = "A";
                                        excel_worksheet.Cells["B8"].Value = "rac" + rac_mmec + ".lac" + lac_mmegid + ".mnc" + mnc + ".mcc" + mcc + ".gprs.";
                                        excel_worksheet.Cells["C8"].Value = sgsnip_textBox.Text;
                                        excel_worksheet.Cells["D8"].Value = "";
                                    }

                                    if (is_ipv6(sgsnip_textBox.Text))
                                    {
                                        excel_worksheet.Cells["A8"].Value = "AAAA";
                                        excel_worksheet.Cells["B8"].Value = "rac" + rac_mmec + ".lac" + lac_mmegid + ".mnc" + mnc + ".mcc" + mcc + ".gprs.";
                                        excel_worksheet.Cells["C8"].Value = sgsnip_textBox.Text;
                                        excel_worksheet.Cells["D8"].Value = "";
                                    }

                                    // 删除空行
                                    DeleteEmptyRows(excel_worksheet);

                                    // 对行和列进行自动适配
                                    excel_worksheet.Cells.AutoFitColumns();
                                }
                            }

                            //DNS config for AMF
                            if (amfip_textBox.Text != "")
                            {
                                if ((amfri_textBox.Text != "") && (amfsi_textBox.Text != "") && (amfp_textBox.Text != ""))
                                {


                                    // 添加一个工作表
                                    ExcelWorksheet excel_worksheet = excel_package.Workbook.Worksheets.Add("AMF DNS Design");
                                    // 写入信息到单元格
                                    excel_worksheet.Cells["A1"].Value = "Record Type";
                                    excel_worksheet.Cells["B1"].Value = "FQDN";
                                    excel_worksheet.Cells["C1"].Value = "Order";
                                    excel_worksheet.Cells["D1"].Value = "Pref";
                                    excel_worksheet.Cells["E1"].Value = "Flag";
                                    excel_worksheet.Cells["F1"].Value = "Service Name";
                                    excel_worksheet.Cells["G1"].Value = "Replacement";
                                    excel_worksheet.Cells["H1"].Value = "Remark";

                                    excel_worksheet.Cells["A6"].Value = "Record Type";
                                    excel_worksheet.Cells["B6"].Value = "FQDN";
                                    excel_worksheet.Cells["C6"].Value = "IP address";
                                    excel_worksheet.Cells["D6"].Value = "Comment";

                                    for (char c = 'A'; c <= 'H'; c++)
                                    {
                                        // 获取单元格 A1 的样式对象
                                        var cellStyle = excel_worksheet.Cells[c + "1"].Style;

                                        // 设置字体加粗
                                        cellStyle.Font.Bold = true;

                                        // 设置背景颜色
                                        cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                        cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                        // 设置字体颜色
                                        cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                    }

                                    for (char c = 'A'; c <= 'D'; c++)
                                    {
                                        // 获取单元格 A1 的样式对象
                                        var cellStyle = excel_worksheet.Cells[c + "6"].Style;

                                        // 设置字体加粗
                                        cellStyle.Font.Bold = true;

                                        // 设置背景颜色
                                        cellStyle.Fill.PatternType = ExcelFillStyle.Solid;
                                        cellStyle.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#0070C0"));

                                        // 设置字体颜色
                                        cellStyle.Font.Color.SetColor(System.Drawing.Color.White);
                                    }

                                    excel_worksheet.Cells["A2"].Value = "NAPTR";
                                    excel_worksheet.Cells["B2"].Value = "mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                    excel_worksheet.Cells["C2"].Value = "10";
                                    excel_worksheet.Cells["D2"].Value = "10";
                                    excel_worksheet.Cells["E2"].Value = "a";
                                    excel_worksheet.Cells["F2"].Value = "x-3gpp-mme:x-s10";
                                    excel_worksheet.Cells["G2"].Value = "topoff.s10-mapped-from-old-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                    excel_worksheet.Cells["H2"].Value = "Old AMF selected based on GUAMI during TAU from 5G to LTE, S10 as N26!";

                                    excel_worksheet.Cells["A3"].Value = "NAPTR";
                                    excel_worksheet.Cells["B3"].Value = "pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                    excel_worksheet.Cells["C3"].Value = "10";
                                    excel_worksheet.Cells["D3"].Value = "10";
                                    excel_worksheet.Cells["E3"].Value = "a";
                                    excel_worksheet.Cells["F3"].Value = "x-3gpp-amf:x-n26";
                                    excel_worksheet.Cells["G3"].Value = "topoff.n26-old-amf01.pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                    excel_worksheet.Cells["H3"].Value = "Old AMF selected based on GUAMI during TAU from 5G to LTE!";

                                    if (is_ipv4(amfip_textBox.Text))
                                    {
                                        excel_worksheet.Cells["A7"].Value = "A";
                                        excel_worksheet.Cells["B7"].Value = "topoff.s10-mapped-from-old-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["C7"].Value = amfip_textBox.Text;
                                        excel_worksheet.Cells["D7"].Value = "";

                                        excel_worksheet.Cells["A8"].Value = "A";
                                        excel_worksheet.Cells["B8"].Value = "topoff.n26-old-amf01.pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["C8"].Value = amfip_textBox.Text;
                                        excel_worksheet.Cells["D8"].Value = "";

                                    }

                                    if (is_ipv6(amfip_textBox.Text))
                                    {
                                        excel_worksheet.Cells["A7"].Value = "AAAA";
                                        excel_worksheet.Cells["B7"].Value = "topoff.s10-mapped-from-old-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["C7"].Value = amfip_textBox.Text;
                                        excel_worksheet.Cells["D7"].Value = "";

                                        excel_worksheet.Cells["A8"].Value = "AAAA";
                                        excel_worksheet.Cells["B8"].Value = "topoff.n26-old-amf01.pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["C8"].Value = amfip_textBox.Text;
                                        excel_worksheet.Cells["D8"].Value = "";

                                    }

                                    if (tac_5g_textBox.Text != "")
                                    {

                                        excel_worksheet.Cells["A4"].Value = "NAPTR";
                                        excel_worksheet.Cells["B4"].Value = "tac-lb" + tac_5gs_lb + ".tac-mb" + tac_5gs_mb + ".tac-hb" + tac_5gs_hb + ".5gstac.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["C4"].Value = "10";
                                        excel_worksheet.Cells["D4"].Value = "10";
                                        excel_worksheet.Cells["E4"].Value = "a";
                                        excel_worksheet.Cells["F4"].Value = "x-3gpp-amf:x-n26";
                                        excel_worksheet.Cells["G4"].Value = "topoff.n26-target-amf01.pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["H4"].Value = "Source MME selected target AMF based on target TAC, used for HO from LTE to 5G with N26!";

                                        excel_worksheet.Cells["A5"].Value = "NAPTR";
                                        excel_worksheet.Cells["B5"].Value = "tac-lb" + tac_5gs_lb + ".tac-mb" + tac_5gs_mb + ".tac-hb" + tac_5gs_hb + ".5gstac.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["C5"].Value = "10";
                                        excel_worksheet.Cells["D5"].Value = "10";
                                        excel_worksheet.Cells["E5"].Value = "a";
                                        excel_worksheet.Cells["F5"].Value = "x-3gpp-mme:x-s10";
                                        excel_worksheet.Cells["G5"].Value = "topoff.s10-mapped-from-target-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                        excel_worksheet.Cells["H5"].Value = "Source MME selected target AMF based on target TAC, used for HO from LTE to 5G, S10 as N26!";


                                        if (is_ipv4(amfip_textBox.Text))
                                        {
                                            excel_worksheet.Cells["A9"].Value = "A";
                                            excel_worksheet.Cells["B9"].Value = "topoff.n26-target-amf01.pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                            excel_worksheet.Cells["C9"].Value = amfip_textBox.Text;
                                            excel_worksheet.Cells["D9"].Value = "";

                                            excel_worksheet.Cells["A10"].Value = "A";
                                            excel_worksheet.Cells["B10"].Value = "topoff.s10-mapped-from-target-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                            excel_worksheet.Cells["C10"].Value = amfip_textBox.Text;
                                            excel_worksheet.Cells["D10"].Value = "";

                                        }


                                        if (is_ipv6(amfip_textBox.Text))
                                        {
                                            excel_worksheet.Cells["A9"].Value = "AAAA";
                                            excel_worksheet.Cells["B9"].Value = "topoff.n26-target-amf01.pt" + amfpointer_hex + ".set" + amfsid_hex + ".region" + amfrid_hex + ".amfi.5gc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                            excel_worksheet.Cells["C9"].Value = amfip_textBox.Text;
                                            excel_worksheet.Cells["D9"].Value = "";

                                            excel_worksheet.Cells["A10"].Value = "AAAA";
                                            excel_worksheet.Cells["B10"].Value = "topoff.s10-mapped-from-target-amf01.mmec" + amf_mmec + ".mmegi" + amf_mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.";
                                            excel_worksheet.Cells["C10"].Value = amfip_textBox.Text;
                                            excel_worksheet.Cells["D10"].Value = "";
                                        }
                                    }

                                    // 删除空行
                                    DeleteEmptyRows(excel_worksheet);

                                    // 对行和列进行自动适配
                                    excel_worksheet.Cells.AutoFitColumns();
                                }
                            }
                            // 保存Excel文件
                            excel_package.SaveAs(new System.IO.FileInfo(savedFilePath));
                        }
                    }
                }
            }
        }
        private void reset_button_Click(object sender, EventArgs e)
        {
            mcc_textBox.Text = "";
            mnc_textBox.Text = "";
            lac_textBox.Text = "";
            rac_textBox.Text = "";
            tac_textBox.Text = "";
            tac_5g_textBox.Text = "";
            apn_textBox.Text = "";
            sgwip_textBox.Text = "";
            pgwip_textBox.Text = "";
            naptr_radioButton.Checked = true;
            topon_radioButton.Checked = true;
            sgsnip_textBox.Text = "";
            mmegid_textBox.Text = "";
            mmec_textBox.Text = "";
            mmeip_textBox.Text = "";

            amfri_textBox.Text = "";
            amfsi_textBox.Text = "";
            amfp_textBox.Text = "";

            uetype_textBox.Text = "";

            smf_support_checkBox.Checked = true;
            nr_support_checkBox.Checked = true;

            amfip_textBox.Text = "";
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (result_textBox.Text != "")
            {
                //设置保存文件的格式
                config_saveFileDialog.Filter = "All files (*.*)|*.*|Normal txt files (*.txt)|*.txt";
                //设置默认文件类型显示顺序  
                config_saveFileDialog.FilterIndex = 2;
                //保存对话框是否记忆上次打开的目录  
                config_saveFileDialog.RestoreDirectory = true;
                config_saveFileDialog.FileName = fileTime + "_DNS_config.txt";
                if (config_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //使用“另存为”对话框中输入的文件名实例化StreamWriter对象
                    StreamWriter sw = new StreamWriter(config_saveFileDialog.FileName, true);
                    //向创建的文件中写入内容
                    sw.WriteLine(result_textBox.Text);
                    //关闭当前文件写入流
                    sw.Close();
                    //获得文件路径
                    string savedFilePath = config_saveFileDialog.FileName.ToString();
                    if (MessageBox.Show("Config saved as: " + savedFilePath + "\r\n\r\nOpen or not?", "DNS Config", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        //if (checkNodepadplus() == true)
                        //{
                        //    MessageBox.Show("Installed");
                        //    System.Diagnostics.Process.Start("notepad++.exe", savedFilePath);
                        //}
                        //else
                        //{
                        System.Diagnostics.Process.Start("notepad.exe", savedFilePath);
                        //}
                    }
                }
            }
            else
            {
                MessageBox.Show("No Config is existed!", "DNS Config");
                return;
            }
        }


        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = Application.OpenForms["help"];  //查找是否打开过Form1窗体  
            if ((f == null) || (f.IsDisposed)) //没打开过  
            {
                help fa = new help();
                fa.Show();   //重新new一个Show出来
            }
            else
            {
                f.Activate();   //打开过就让其获得焦点  
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = Application.OpenForms["about"];  //查找是否打开过Form1窗体  
            if ((f == null) || (f.IsDisposed)) //没打开过  
            {
                about fa = new about();
                fa.Show();   //重新new一个Show出来
            }
            else
            {
                f.Activate();   //打开过就让其获得焦点  
                f.WindowState = FormWindowState.Normal;
            }
        }

        private void result_textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
            {
                ((TextBox)sender).Copy();
            }

            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
            {
                if (result_textBox.Text != "")
                {
                    //设置保存文件的格式
                    config_saveFileDialog.Filter = "All files (*.*)|*.*|Normal txt files (*.txt)|*.txt";
                    //设置默认文件类型显示顺序  
                    config_saveFileDialog.FilterIndex = 2;
                    //保存对话框是否记忆上次打开的目录  
                    config_saveFileDialog.RestoreDirectory = true;
                    config_saveFileDialog.FileName = fileTime + "_DNS_config.txt";
                    if (config_saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //使用“另存为”对话框中输入的文件名实例化StreamWriter对象
                        StreamWriter sw = new StreamWriter(config_saveFileDialog.FileName, true);
                        //向创建的文件中写入内容
                        sw.WriteLine(result_textBox.Text);
                        //关闭当前文件写入流
                        sw.Close();
                        //获得文件路径
                        string savedFilePath = config_saveFileDialog.FileName.ToString();
                        if (MessageBox.Show("Config saved as: " + savedFilePath + "\r\n\r\nOpen or not?", "DNS Config Basic", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            //if (checkNodepadplus() == true)
                            //{
                            //    MessageBox.Show("Installed");
                            //    System.Diagnostics.Process.Start("notepad++.exe", savedFilePath);
                            //}
                            //else
                            //{
                            System.Diagnostics.Process.Start("notepad.exe", savedFilePath);
                            //}
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Config is existed!", "DNS Config");
                    return;
                }
            }
        }

        private void uetype_textBox_KeyUp(object sender, KeyEventArgs e)
        {
            uetype_error_label.Text = "";
            //ue uasage type check
            if (uetype_textBox.Text != "")
            {
                if (!IsNumericAndDotOnly(uetype_textBox.Text))
                {
                    uetype_error_label.Text = "UE Usage Type is not correct!";
                }
            }

        }

        private void load_input_button_Click(object sender, EventArgs e)
        {
            try
            {
                input_excel_openFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx|All File (*.*)|*.*";
                input_excel_openFileDialog.Title = "DNS Config Input Excel";

                if (input_excel_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    mcc_textBox.Text = "";
                    mnc_textBox.Text = "";
                    lac_textBox.Text = "";
                    rac_textBox.Text = "";
                    tac_textBox.Text = "";
                    tac_5g_textBox.Text = "";
                    apn_textBox.Text = "";
                    sgwip_textBox.Text = "";
                    pgwip_textBox.Text = "";
                    naptr_radioButton.Checked = false;
                    topon_radioButton.Checked = false;
                    sgsnip_textBox.Text = "";
                    mmegid_textBox.Text = "";
                    mmec_textBox.Text = "";
                    mmeip_textBox.Text = "";

                    amfri_textBox.Text = "";
                    amfsi_textBox.Text = "";
                    amfp_textBox.Text = "";

                    uetype_textBox.Text = "";

                    smf_support_checkBox.Checked = false;
                    nr_support_checkBox.Checked = false;

                    amfip_textBox.Text = "";

                    result_textBox.Text = "";

                    string selectedFilePath = input_excel_openFileDialog.FileName;

                    // 在这里处理选择的 Excel 文件
                    using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(selectedFilePath)))
                    {
                        // 处理 Excel 文件内容
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["DNS Input"];
                        // 读取并显示单元格的值到 TextBox
                        string mcc_cell_value = worksheet.Cells["B1"].Text;
                        mcc_textBox.Text = mcc_cell_value;
                        string mnc_cell_value = worksheet.Cells["D1"].Text;
                        mnc_textBox.Text = mnc_cell_value;

                        string lac_cell_value = worksheet.Cells["B2"].Text;
                        lac_textBox.Text = lac_cell_value;
                        string rac_cell_value = worksheet.Cells["D2"].Text;
                        rac_textBox.Text = rac_cell_value;

                        string mmegi_cell_value = worksheet.Cells["B3"].Text;
                        mmegid_textBox.Text = mmegi_cell_value;
                        string mmec_cell_value = worksheet.Cells["D3"].Text;
                        mmec_textBox.Text = rac_cell_value;

                        string amfri_cell_value = worksheet.Cells["B4"].Text;
                        amfri_textBox.Text = amfri_cell_value;
                        string amfsi_cell_value = worksheet.Cells["D4"].Text;
                        amfsi_textBox.Text = amfsi_cell_value;
                        string amfp_cell_value = worksheet.Cells["F4"].Text;
                        amfp_textBox.Text = amfsi_cell_value;

                        string tac_cell_value = worksheet.Cells["B5"].Text;
                        tac_textBox.Text = tac_cell_value;

                        string tac_5g_cell_value = worksheet.Cells["B6"].Text;
                        tac_5g_textBox.Text = tac_5g_cell_value;

                        string apn_cell_value = worksheet.Cells["B7"].Text;
                        apn_textBox.Text = apn_cell_value;

                        string top_cell_value = worksheet.Cells["B8"].Text;
                        if (top_cell_value == "topon")
                        {
                            topoff_radioButton.Checked = false;
                            topon_radioButton.Checked = true;
                        }
                        else if (top_cell_value == "topoff")
                        {
                            topoff_radioButton.Checked = true;
                            topon_radioButton.Checked = false;
                        }
                        else
                        {
                            topoff_radioButton.Checked = false;
                            topon_radioButton.Checked = true;
                        }

                        string naptr_srv_cell_value = worksheet.Cells["B9"].Text;
                        if (naptr_srv_cell_value == "NAPTR")
                        {
                            naptr_radioButton.Checked = true;
                            srv_radioButton.Checked = false;
                        }
                        else if (naptr_srv_cell_value == "SRV")
                        {
                            naptr_radioButton.Checked = false;
                            srv_radioButton.Checked = true;
                        }
                        else
                        {
                            naptr_radioButton.Checked = true;
                            srv_radioButton.Checked = false;
                        }

                        string uetype_cell_value = worksheet.Cells["B10"].Text;
                        uetype_textBox.Text = uetype_cell_value;

                        string smf_support_cell_value = worksheet.Cells["B11"].Text;
                        if (smf_support_cell_value == "Yes")
                        {
                            smf_support_checkBox.Checked = true;
                        }
                        else
                        {
                            smf_support_checkBox.Checked = false;
                        }

                        string nr_support_cell_value = worksheet.Cells["B12"].Text;
                        if (nr_support_cell_value == "Yes")
                        {
                            nr_support_checkBox.Checked = true;
                        }
                        else
                        {
                            nr_support_checkBox.Checked = false;
                        }

                        string sgwip_cell_value = worksheet.Cells["B13"].Text;
                        // 获取单元格的样式
                        var sgwip_cell_Style = worksheet.Cells["B13"].Style;

                        // 判断文本中是否包含换行符
                        if ((sgwip_cell_Style.WrapText) || (sgwip_cell_value.Contains("\n")))
                        {
                            // 使用 String.Split 方法拆分为多行
                            //string[] lines = sgwip_cell_value.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                            string[] lines = sgwip_cell_value.Split(new[] { "\n" }, StringSplitOptions.None);

                            // 将每一行的内容显示在输出 TextBox 中
                            //foreach (var line in lines)
                            //{
                            //    sgwip_textBox.AppendText(line + Environment.NewLine);
                            //}

                            // 将每一行的内容显示在输出 TextBox 中
                            for (int i = 0; i < lines.Length; i++)
                            {
                                if (string.IsNullOrEmpty(lines[i]))
                                {
                                    sgwip_textBox.AppendText(lines[i]);
                                }
                                else
                                {
                                    sgwip_textBox.AppendText(lines[i] + Environment.NewLine);
                                }
                            }
                        }
                        else
                        {
                            // 如果没有换行符，直接显示整个文本内容
                            sgwip_textBox.AppendText(sgwip_cell_value);
                        }


                        string pgwip_cell_value = worksheet.Cells["B14"].Text;
                        // 获取单元格的样式
                        var pgwip_cell_Style = worksheet.Cells["B14"].Style;

                        // 判断文本中是否包含换行符
                        if ((pgwip_cell_Style.WrapText) || (pgwip_cell_value.Contains("\n")))
                        {
                            // 使用 String.Split 方法拆分为多行
                            //string[] lines = sgwip_cell_value.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                            string[] lines = pgwip_cell_value.Split(new[] { "\n" }, StringSplitOptions.None);

                            // 将每一行的内容显示在输出 TextBox 中
                            //foreach (var line in lines)
                            //{
                            //    pgwip_textBox.AppendText(line + Environment.NewLine);
                            //}

                            // 将每一行的内容显示在输出 TextBox 中
                            for (int i = 0; i < lines.Length; i++)
                            {
                                if ( string.IsNullOrEmpty(lines[i]))
                                {
                                    pgwip_textBox.AppendText(lines[i]);
                                }
                                else
                                {
                                    pgwip_textBox.AppendText(lines[i] + Environment.NewLine);
                                }
                            }
                        }
                        else
                        {
                            // 如果没有换行符，直接显示整个文本内容
                            pgwip_textBox.AppendText(pgwip_cell_value);
                        }


                        string sgsnip_cell_value = worksheet.Cells["B15"].Text;
                        sgsnip_textBox.Text = sgsnip_cell_value;

                        string mmeip_cell_value = worksheet.Cells["B16"].Text;
                        mmeip_textBox.Text = mmeip_cell_value;

                        string amfip_cell_value = worksheet.Cells["B17"].Text;
                        amfip_textBox.Text = amfip_cell_value;
                    }
                }
            }
            catch (Exception ex)
            {
                topoff_radioButton.Checked = false;
                topon_radioButton.Checked = true;
                naptr_radioButton.Checked = true;
                srv_radioButton.Checked = false;
                smf_support_checkBox.Checked = true;
                nr_support_checkBox.Checked = true;
                // 处理其他异常
                MessageBox.Show($"Abnormal Error: excel may not have worksheet named \"DNS Input\" or the input is not correct, please manually fill all inputs firstly and submit to get a template!\nError Message：{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}