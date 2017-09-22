using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.NetworkInformation;
using DNS;

namespace DNS_Config
{
    public partial class dnsquery : Form
    {
        public dnsquery()
        {
            InitializeComponent();
            //NetworkInterface[] t_interface = NetworkInterface.GetAllNetworkInterfaces();
            //System.Console.WriteLine(t_interface[1].Description);
            //IPInterfaceProperties e_property = t_interface[1].GetIPProperties();
            //IPAddressCollection e_collection = e_property.DnsAddresses;
            textBox_DnsServer.Text = "192.168.10.3";
                //e_collection[0].ToString();
            comboBox_qType.SelectedIndex = 0;
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

        private void button_query_Click(object sender, EventArgs e)
        {
            string dns_ServerIP = textBox_DnsServer.Text.Trim();
            string q_FQDN = textBox_fqdn.Text;
            string record_type = comboBox_qType.SelectedItem.ToString();
            if (dns_ServerIP == "")
            {
                MessageBox.Show("Please fill in the DNS Server", "Alarm");
                return;
            }
            else
            {
                if (!is_ipv4(dns_ServerIP) && !is_ipv6(dns_ServerIP))
                {
                    MessageBox.Show("It is not a IPv4 or IPv6 Address, please fill in a correct DNS Server", "Alarm");
                    return;
                }
            }
            if (q_FQDN == "")
            {
                MessageBox.Show("Please fill in the FQDN！", "Alarm");
                return;
            }

            bool success = Dns_server(dns_ServerIP, q_FQDN, record_type);
            if (!success)
            {
                MessageBox.Show("DNS Query Failed！", "Alarm");
                return;
            }
        }

        private bool Dns_server(string dns_ServerIP, string fqdn, string Type)
        {
            Dns_Client dnsC = new Dns_Client(dns_ServerIP);
            try
            {
                QTYPE qType = (QTYPE)Enum.Parse(typeof(QTYPE), Type);
                DnsServerResponse first_query = dnsC.Query(fqdn, qType);
                if (Type == "A")
                {
                    textBox_Result.Text = "";
                    //textBox_Result.Text += first_query.GetARecords.ToString() + "\r\n";
                    foreach (DNS_rr_A record in first_query.GetARecords())
                    {
                        textBox_Result.Text += record.Domain + ":" + record.IP.ToString() + "\r\n";
                    }
                }
                else if (Type == "NAPTR")
                {
                    textBox_Result.Text = "";
                    foreach (DNS_rr_NAPTR record in first_query.GetNAPTRRecords())
                    {
                        textBox_Result.Text += record.Domain + ":" + record.Services.ToString() + "   " + record.Replacement.ToString() + "\r\n";
                        string flags = record.Flags.ToString();
                        if (flags == "a")
                        {
                            if (record.Services.ToString().Contains("sgw"))
                            {
                                string[] sgwArray = record.Replacement.ToString().Split('.');
                                string sgwCanonicalName = "";
                                for (int i = 2; i < sgwArray.Length - 1; i++)
                                {
                                    sgwCanonicalName += sgwArray[i].ToString() + ".";
                                }
                                sgwCanonicalName += sgwArray[sgwArray.Length - 1].ToString();
                                textBox_Result.Text += "SGW Canonical Name:" + sgwCanonicalName + "\r\n";
                            }
                            DnsServerResponse second_query = dnsC.Query(record.Replacement.ToString());
                            foreach (DNS_rr_A second_record in second_query.GetARecords())
                            {
                                textBox_Result.Text += second_record.Domain + ":" + second_record.IP.ToString() + "\r\n";
                            }
                            DnsServerResponse second_aaaa_query = dnsC.Query(record.Replacement.ToString(), (QTYPE)Enum.Parse(typeof(QTYPE), "AAAA"));
                            foreach (DNS_rr_AAAA second_aaaa_record in second_aaaa_query.GetAAAARecords())
                            {
                                textBox_Result.Text += second_aaaa_record.Domain + ":" + second_aaaa_record.IP.ToString() + "\r\n";
                            }
                        }
                        else if (flags == "s")
                        {
                            QTYPE s_qType = (QTYPE)Enum.Parse(typeof(QTYPE), "SRV");
                            DnsServerResponse second_query = dnsC.Query(record.Replacement.ToString(), s_qType);
                            foreach (DNS_rr_SRV second_record in second_query.GetSRVRecords())
                            {
                                textBox_Result.Text += second_record.Domain + ":" + second_record.Target.ToString() + "\r\n";
                                if (record.Services.ToString().Contains("sgw"))
                                {
                                    string sgw_CanonicalName = "";
                                    string[] sgwArray = second_record.Target.ToString().Split('.');
                                    for (int i = 2; i < sgwArray.Length - 1; i++)
                                    {
                                        sgw_CanonicalName += sgwArray[i] + ".";
                                    }
                                    sgw_CanonicalName += sgwArray[sgwArray.Length - 1];

                                    DnsServerResponse sgw_s11_query = dnsC.Query(sgw_CanonicalName, (QTYPE)Enum.Parse(typeof(QTYPE), "NAPTR"));
                                    foreach (DNS_rr_NAPTR sgw_s11_record in sgw_s11_query.GetNAPTRRecords())
                                    {
                                        if (sgw_s11_record.Services.ToString().Contains("sgw"))
                                        {
                                            textBox_Result.Text += sgw_s11_record.Domain + ":" + sgw_s11_record.Services.ToString() + "   " + sgw_s11_record.Replacement.ToString() + "\r\n";
                                            DnsServerResponse sgw_s11_query_a = dnsC.Query(sgw_s11_record.Replacement.ToString(), (QTYPE)Enum.Parse(typeof(QTYPE), "A"));
                                            foreach (DNS_rr_A sgw_s11_record_a in sgw_s11_query_a.GetARecords())
                                            {
                                                textBox_Result.Text += sgw_s11_record_a.Domain + ":" + sgw_s11_record_a.IP.ToString() + "\r\n";
                                            }
                                        }
                                    }
                                    //string sgwCanonicalName = "";
                                    //textBox_Result.Text
                                    //for (int i = 2; i < sgwArray.Length - 1; i++)
                                    //{
                                    //    sgwCanonicalName += sgwArray[i].ToString() + ".";
                                    //}
                                    //sgwCanonicalName += sgwArray[sgwArray.Length].ToString();
                                    //textBox_Result.Text += "SGW Canonical Name:" + sgwCanonicalName + "\r\n";
                                }

                                //textBox_Result.Text += second_record.Domain + ":" + second_record.Target.ToString() + "\r\n";
                                //DnsServerResponse third_query = dnsC.Query(second_record.Target.ToString());
                                //foreach (DNS_rr_A third_record in third_query.GetARecords())
                                //{
                                //    textBox_Result.Text += third_record.Domain + ":" + third_record.IP.ToString() + "\r\n";
                                //}
                                //
                                //DnsServerResponse third_aaaa_query = dnsC.Query(second_record.Target.ToString(), (QTYPE)Enum.Parse(typeof(QTYPE), "AAAA"));
                                //foreach (DNS_rr_AAAA third_aaaa_record in third_aaaa_query.GetAAAARecords())
                                //{
                                //    textBox_Result.Text += third_aaaa_record.Domain + ":" + third_aaaa_record.IP.ToString() + "\r\n";
                                //}
                            }
                        }
                    }
                }
                else if (Type == "AAAA")
                {
                    foreach (DNS_rr_AAAA record in first_query.GetAAAARecords())
                    {
                        textBox_Result.Text += record.Domain + ":" + record.IP.ToString() + "\r\n";
                    }
                }

                //foreach (DNS_rr_NS record in response.GetAuthRecords())
                //{
                //    listBox.Items.Add(record.Domain + ":" + record.NameServer);
                //}
                //foreach (DNS_rr_A record in response.GetAdditRecords())
                //{
                //    listBox.Items.Add(record.Domain + ":" + record.IP.ToString());
                //}
            }
            catch (DNS_ClientException ee)
            {
                MessageBox.Show(ee.ErrorCode.ToString());
                return false;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return false;
            }
            return true;
        }
        private void Host_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button_query_Click(sender, e);
            }
        }
    }
}