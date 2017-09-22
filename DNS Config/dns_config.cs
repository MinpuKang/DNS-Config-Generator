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

namespace DNS_Config
{
    public partial class DNS_config : Form
    {

        public DNS_config()
        {
            InitializeComponent();
        }

        private void DNS_config_Load(object sender, EventArgs e)
        {

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

        private void Submit_Click(object sender, EventArgs e)
        {
            string mcc = "";
            string mnc = "";
            string lac = "";
            string rac = "";
            string tac_hb = "";
            string tac_lb = "";
            string apn = "";
            string mmegid = "";
            string mmec = "";
            string topological = "";

            if (topon_radioButton.Checked == true)
            {
                topological = topon_radioButton.Text;
            }
            else if (topoff_radioButton.Checked == true)
            {
                topological = topoff_radioButton.Text;
            }

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

            if ((lac_textBox.Text == "") && (rac_textBox.Text == "") && (tac_textBox.Text == "") && (mmegid_textBox.Text == "") && (mmec_textBox.Text == "") && (apn_textBox.Text == ""))
            {
                MessageBox.Show("No available ID like LAC-RAC, TAC, GUMMEI or APN!", "Location ID");
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

            //APN是否为空
            if (apn_textBox.Text != "")
            {
                apn = apn_textBox.Text;
            }

            //For FQDN
            if ((sgwip_textBox.Text == "") && (pgw_textBox.Text == "") && (sgsn_textBox.Text == "") && (mme_textBox.Text == ""))
            {
                result_textBox.Text = "";

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

                if (apn != "")
                {
                    result_textBox.Text += "\r\n===APN FQDN for GGSN===\r\n";
                    result_textBox.Text += "    " + apn + ".mnc" + mnc + ".mcc" + mcc + ".gprs.\r\n";

                    result_textBox.Text += "\r\n===APN FQDN for PGW===\r\n";
                    result_textBox.Text += "    " + apn + ".apn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                    result_textBox.Text += "    Services: x-3gpp-pgw:x-s5-gtp:x-s8-gtp:x-s2b-gtp:x-s2a-gtp:x-gn\r\n";
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
            }

            //For DNS config
            if ((sgwip_textBox.Text != "") || (pgw_textBox.Text != "") || (sgsn_textBox.Text != "") || (mme_textBox.Text != ""))
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
                                    result_textBox.Text += "tac-lb" + tac_lb + ".tac-hb" + tac_hb + ".tac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"s\"  \"x-3gpp-sgw:x-s5-gtp:x-s8-gtp\" \"\" " + "sgw-list" + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
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
                                    result_textBox.Text += "tac-lb" + tac_lb + ".tac-hb" + tac_hb + ".tac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\"  \"x-3gpp-sgw:x-s5-gtp:x-s8-gtp\" \"\" " + topological + ".sgw-s5s8.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                                if (is_ipv4(sgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-sgw:x-s11\" \"\" " + topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                    result_textBox.Text += topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A	" + sgwip_textBox.Lines[i] + "\r\n\r\n";
                                }
                                if (is_ipv6(sgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-sgw:x-s11\" \"\" " + topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                    result_textBox.Text += topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA	" + sgwip_textBox.Lines[i] + "\r\n\r\n";
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
                                    result_textBox.Text += "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-sgw:x-s11\" \"\" " + topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                                if (is_ipv6(sgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-sgw:x-s11\" \"\" " + topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                            }
                            for (int i = 0; i < sgwip_textBox.Lines.Length; i++)
                            {
                                if (is_ipv4(sgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A	" + sgwip_textBox.Lines[i] + "\r\n";
                                }
                                if (is_ipv6(sgwip_textBox.Lines[i]))
                                {
                                    result_textBox.Text += topological + ".sgw-s11.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA	" + sgwip_textBox.Lines[i] + "\r\n";
                                }
                            }
                            result_textBox.Text += "\r\n";
                        }
                    }
                }

                //DNS config for PGW
                if (pgw_textBox.Text != "")
                {
                    if (apn_textBox.Text != "")
                    {

                        for (int i = 0; i < pgw_textBox.Lines.Length; i++)
                        {
                            if (is_ipv4(pgw_textBox.Lines[i]) || is_ipv6(pgw_textBox.Lines[i]))
                            {
                                result_textBox.Text += "\r\n;\r\n" + "; GGSN selected based on APN, used during PDP active in 2/3G!" + "\r\n;\r\n";
                                break;
                            }
                        }

                        for (int i = 0; i < pgw_textBox.Lines.Length; i++)
                        {
                            if (is_ipv4(pgw_textBox.Lines[i]))
                            {
                                result_textBox.Text += apn + ".mnc" + mnc + ".mcc" + mcc + ".gprs.     IN A 	" + pgw_textBox.Lines[i] + "\r\n";
                            }

                            if (is_ipv6(pgw_textBox.Lines[i]))
                            {
                                result_textBox.Text += apn + ".mnc" + mnc + ".mcc" + mcc + ".gprs.     IN AAAA  	" + pgw_textBox.Lines[i] + "\r\n";
                            }
                        }

                        for (int i = 0; i < pgw_textBox.Lines.Length; i++)
                        {
                            if (is_ipv4(pgw_textBox.Lines[i]) || is_ipv6(pgw_textBox.Lines[i]))
                            {
                                result_textBox.Text += "\r\n;\r\n" + "; PGW selected based on APN, used during LTE/WiFi attach or PDN connection, HO between LTE and WiFi!" + "\r\n;\r\n";
                                if (srv_radioButton.Checked == true)
                                {
                                    result_textBox.Text += apn + ".apn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"s\"  \"x-3gpp-pgw:x-s5-gtp:x-s8-gtp:x-s2b-gtp:x-s2a-gtp:x-gn\" \"\" " + "pgw-list" + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                                break;
                            }
                        }

                        for (int i = 0; i < pgw_textBox.Lines.Length; i++)
                        {
                            if (naptr_radioButton.Checked == true)
                            {
                                if (is_ipv4(pgw_textBox.Lines[i]) || is_ipv6(pgw_textBox.Lines[i]))
                                {
                                    result_textBox.Text += apn + ".apn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-pgw:x-s5-gtp:x-s8-gtp:x-s2b-gtp:x-s2a-gtp:x-gn\" \"\" " + topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                    result_textBox.Text += "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-pgw:x-s5-gtp:x-s8-gtp:x-s2b-gtp:x-s2a-gtp:x-gn\" \"\" " + topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                                if (is_ipv4(pgw_textBox.Lines[i]))
                                {
                                    result_textBox.Text += topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A	" + pgw_textBox.Lines[i] + "\r\n\r\n";
                                }
                                if (is_ipv6(pgw_textBox.Lines[i]))
                                {
                                    result_textBox.Text += topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA 	" + pgw_textBox.Lines[i] + "\r\n\r\n";
                                }
                            }
                            else if (srv_radioButton.Checked == true)
                            {
                                if (is_ipv4(pgw_textBox.Lines[i]) || is_ipv6(pgw_textBox.Lines[i]))
                                {
                                    result_textBox.Text += "pgw-list" + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org." + "   IN SRV 100 100 2123   " + topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                            }
                        }

                        if (srv_radioButton.Checked == true)
                        {
                            for (int i = 0; i < pgw_textBox.Lines.Length; i++)
                            {
                                if (is_ipv4(pgw_textBox.Lines[i]) || is_ipv6(pgw_textBox.Lines[i]))
                                {
                                    result_textBox.Text += "epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-pgw:x-s5-gtp:x-s8-gtp:x-s2b-gtp:x-s2a-gtp:x-gn\" \"\" " + topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n";
                                }
                            }
                            for (int i = 0; i < pgw_textBox.Lines.Length; i++)
                            {
                                if (is_ipv4(pgw_textBox.Lines[i]))
                                {
                                    result_textBox.Text += topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A	" + pgw_textBox.Lines[i] + "\r\n";
                                }
                                if (is_ipv6(pgw_textBox.Lines[i]))
                                {
                                    result_textBox.Text += topological + ".pgw-s5s8s2b.epg" + i + ".epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA 	" + pgw_textBox.Lines[i] + "\r\n";
                                }
                            }
                            result_textBox.Text += "\r\n";
                        }
                    }
                }

                //DNS config for SGSN
                if (sgsn_textBox.Text != "")
                {
                    if ((lac != "") && (rac != ""))
                    {
                        if (is_ipv4(sgsn_textBox.Text))
                        {
                            result_textBox.Text += "\r\n;\r\n" + "; OldSGSN selected based on LAC-RAC if no cooperating SGSN defined, used when P-TMSI attach, Inter RAU, ISC Inter RAU!" + "\r\n;\r\n";
                            result_textBox.Text += "rac" + rac + ".lac" + lac + ".mnc" + mnc + ".mcc" + mcc + ".gprs.        IN A " + sgsn_textBox.Text + "\r\n";

                            result_textBox.Text += "\r\n;\r\n" + "; New MME selects an old SGSN based on GUMMEI which is mapped from LAC-RAC in the old SGSN, this is used when ISC-TAU from W/G to LTE with Gn/Gp:------------IRAT!" + "\r\n";
                            result_textBox.Text += "; Source MME selects target SGSN during Handover from LTE to WCDMA with Gn network" + "\r\n;\r\n";
                            result_textBox.Text += "rac" + rac + ".lac" + lac + ".rac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-sgsn:x-gn:x-gp\" \"\"    topoff.gn.sgsn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.gn.sgsn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A " + sgsn_textBox.Text + "\r\n\r\n";
                        }

                        if (is_ipv6(sgsn_textBox.Text))
                        {
                            result_textBox.Text += "\r\n;\r\n" + "; OldSGSN selected based on LAC-RAC if no cooperating SGSN defined, used when P-TMSI attach, Inter RAU, ISC Inter RAU!" + "\r\n;\r\n";
                            result_textBox.Text += "rac" + rac + ".lac" + lac + ".mnc" + mnc + ".mcc" + mcc + ".gprs.        IN AAAA " + sgsn_textBox.Text + "\r\n";

                            result_textBox.Text += "\r\n;=======================================================\r\n" + ";New MME selects an old SGSN based on GUMMEI which is mapped from LAC-RAC in the old SGSN, this is used when ISC-TAU from W/G to LTE with Gn/Gp:------------IRAT!" + "\r\n;\r\n";
                            result_textBox.Text += "rac" + rac + ".lac" + lac + ".rac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-sgsn:x-gn:x-gp\" \"\" sgsnoldgn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "sgsnoldgn.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA " + sgsn_textBox.Text + "\r\n\r\n";
                        }
                    }

                    if ((mmegid != "") && (mmec != ""))
                    {
                        if (is_ipv4(sgsn_textBox.Text))
                        {
                            string lac_mmegid = mmegid;
                            string rac_mmec = mmec;
                            while (rac_mmec.Length < 4)
                            {
                                rac_mmec = "0" + rac_mmec;
                            }
                            result_textBox.Text += "\r\n;\r\n" + "; New SGSN queries the old MME during ISC-RAU from LTE to W/G with Gn/Gp:------------IRAT, and the MMEGroupID mapped to old LAC, MMECode mapped to old RAC!" + "\r\n;\r\n";
                            result_textBox.Text += "rac" + rac_mmec + ".lac" + lac_mmegid + ".mnc" + mnc + ".mcc" + mcc + ".gprs.        IN A " + sgsn_textBox.Text + "\r\n\r\n";
                        }

                        if (is_ipv6(sgsn_textBox.Text))
                        {
                            string lac_mmegid = mmegid;
                            string rac_mmec = mmec;
                            while (rac_mmec.Length < 4)
                            {
                                rac_mmec = "0" + rac_mmec;
                            }
                            result_textBox.Text += "\r\n;\r\n" + "; New SGSN queries the old MME during ISC-RAU from LTE to W/G with Gn/Gp:------------IRAT, and the MMEGroupID mapped to old LAC, MMECode mapped to old RAC!" + "\r\n;\r\n";
                            result_textBox.Text += "rac" + rac_mmec + ".lac" + lac_mmegid + ".mnc" + mnc + ".mcc" + mcc + ".gprs.        IN AAAA " + sgsn_textBox.Text + "\r\n\r\n";
                        }
                    }
                }

                //DNS config for MME
                if (mme_textBox.Text != "")
                {
                    if ((mmegid != "") && (mmec != ""))
                    {
                        if (is_ipv4(mme_textBox.Text))
                        {
                            result_textBox.Text += "\r\n;\r\n" + "; Old MME selected based on GUMMEI during inter TAU!" + "\r\n;\r\n";
                            result_textBox.Text += "mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-mme:x-s10\" \"\" topoff.oldMMES10.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.oldMMES10.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A " + mme_textBox.Text + "\r\n\r\n";
                        }

                        if (is_ipv6(mme_textBox.Text))
                        {
                            result_textBox.Text += "\r\n;\r\n" + "; Old MME selected based on GUMMEI during inter TAU!" + "\r\n;\r\n";
                            result_textBox.Text += "mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-mme:x-s10\" \"\" topoff.oldMMES10.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.oldMMES10.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA " + mme_textBox.Text + "\r\n\r\n";
                        }
                    }

                    if (tac_textBox.Text != "")
                    {
                        if (is_ipv4(mme_textBox.Text))
                        {
                            result_textBox.Text += "\r\n;\r\n" + "; Source MME selected target MME based on target TAC, used for Inter-LTE S1-HO!" + "\r\n;\r\n";
                            result_textBox.Text += "tac-lb" + tac_lb + ".tac-hb" + tac_hb + ".tac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-mme:x-s10\" \"\"    topoff.targetMMES10.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.targetMMES10.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN A " + mme_textBox.Text + "\r\n\r\n";
                        }

                        if (is_ipv6(mme_textBox.Text))
                        {
                            result_textBox.Text += "\r\n;\r\n" + "; Source MME selected target MME based on target TAC, used for Inter-LTE S1-HO!" + "\r\n;\r\n";
                            result_textBox.Text += "tac-lb" + tac_lb + ".tac-hb" + tac_hb + ".tac.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN NAPTR 10 10 \"a\" \"x-3gpp-mme:x-s10\" \"\" topoff.targetMMES10.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.\r\n" + "topoff.targetMMES10.mmec" + mmec + ".mmegi" + mmegid + ".mme.epc.mnc" + mnc + ".mcc" + mcc + ".3gppnetwork.org.        IN AAAA " + mme_textBox.Text + "\r\n\r\n";
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
            apn_textBox.Text = "";
            sgwip_textBox.Text = "";
            pgw_textBox.Text = "";
            naptr_radioButton.Checked = true;
            topon_radioButton.Checked = true;
            sgsn_textBox.Text = "";
            mmegid_textBox.Text = "";
            mmec_textBox.Text = "";
            mme_textBox.Text = "";
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (result_textBox.Text != "")
            {
                //设置保存文件的格式
                saveFileDialog1.Filter = "All files (*.*)|*.*|Normal txt files (*.txt)|*.txt";
                //设置默认文件类型显示顺序  
                saveFileDialog1.FilterIndex = 2;
                //保存对话框是否记忆上次打开的目录  
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "_DNS_config.txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //使用“另存为”对话框中输入的文件名实例化StreamWriter对象
                    StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, true);
                    //向创建的文件中写入内容
                    sw.WriteLine(result_textBox.Text);
                    //关闭当前文件写入流
                    sw.Close();
                    //获得文件路径
                    string savedFilePath = saveFileDialog1.FileName.ToString();
                    if (MessageBox.Show("Config saved as: " + savedFilePath + "\r\n\r\nOpen or not?", "DNS Config", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("notepad.exe", savedFilePath);
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

        private void converToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = Application.OpenForms["conver"];  //查找是否打开过Form1窗体  
            if ((f == null) || (f.IsDisposed)) //没打开过  
            {
                conver fa = new conver();
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
                    saveFileDialog1.Filter = "All files (*.*)|*.*|Normal txt files (*.txt)|*.txt";
                    //设置默认文件类型显示顺序  
                    saveFileDialog1.FilterIndex = 2;
                    //保存对话框是否记忆上次打开的目录  
                    saveFileDialog1.RestoreDirectory = true;
                    saveFileDialog1.FileName = DateTime.Now.ToString("yyyyMMddhhmmss") + "_DNS_config_Basic.txt";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        //使用“另存为”对话框中输入的文件名实例化StreamWriter对象
                        StreamWriter sw = new StreamWriter(saveFileDialog1.FileName, true);
                        //向创建的文件中写入内容
                        sw.WriteLine(result_textBox.Text);
                        //关闭当前文件写入流
                        sw.Close();
                        //获得文件路径
                        string savedFilePath = saveFileDialog1.FileName.ToString();
                        if (MessageBox.Show("Config saved as: " + savedFilePath + "\r\n\r\nOpen or not?", "DNS Config Basic", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start("notepad.exe", savedFilePath);
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
    }
}