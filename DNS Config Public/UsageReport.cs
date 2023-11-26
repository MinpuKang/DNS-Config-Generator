using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Collections.Specialized;
using System.Net.Sockets;

namespace UsageReport
{
    class UsageReport
    {
        string username = Environment.UserName.Trim();
        string toolUID = "10ec0357-4778-4629-a293-574c5ce3a183";  //"28c25fab-5dd7-4703-8af2-042cb4c1d2e8"; //Please Modify this Tracking ID to your own.
        string temp = DateTime.Now.ToLongTimeString();
        DateTime nx = new DateTime(1970, 1, 1);
        string wholeMsg = string.Empty;
        public string starttime = string.Empty;
        public string endtime = string.Empty;


        string ShowIP()
        {
            string myIP = string.Empty;
            foreach (string ip in GetLocalIpv4())
            {
                myIP = ip.Trim();
            }
            return myIP;
        }

        string[] GetLocalIpv4()
        {
            IPAddress[] localIPs;
            localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            StringCollection IpCollection = new StringCollection();
            foreach (IPAddress ip in localIPs)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    IpCollection.Add(ip.ToString());
            }
            string[] IpArray = new string[IpCollection.Count];
            IpCollection.CopyTo(IpArray, 0);
            return IpArray;
        }


        public void checkIntranet()
        {
            try
            {
                TimeSpan ts = DateTime.UtcNow - nx;
                if (starttime == string.Empty || endtime == string.Empty)
                {
                    starttime = ((long)(ts.TotalSeconds * 1000 - 1000)).ToString();//.Remove(13);
                    endtime = ((long)(ts.TotalSeconds * 1000)).ToString();
                }

                Ping myPing = new Ping();
                PingReply myPingReply;
                myPingReply = myPing.Send("10.185.59.51", 1000);
                if (myPingReply.Status == IPStatus.Success)
                {
                    wholeMsg = "[{\"eid\":" + "\"" + username + "\"" + ",\"toolUID\":\"" + toolUID + "\",\"startTime\":" + starttime + ",\"endTime\":" + endtime + ",\"ipAddr\":\"" + ShowIP() + "\"}]";

                    sendTCPMsg(wholeMsg);

                    //
                    //>Send old records:
                    StreamReader srR = new StreamReader(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\tool_log.log");
                    string EncodeLine = string.Empty;
                    try
                    {
                        List<string> myEncodeLines = new List<string>();

                        while ((EncodeLine = srR.ReadLine()) != null)
                        {
                            myEncodeLines.Add(EncodeLine);
                        }
                        srR.Close();
                        foreach (string eachLine in myEncodeLines.Distinct().ToList())
                        {
                            sendTCPMsg(DecodeBase64(eachLine));
                        }
                        StreamWriter srW2 = new StreamWriter(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\tool_log.log", false, System.Text.Encoding.Default);
                        srW2.Write(string.Empty);
                        srW2.Close();
                    }
                    catch
                    {
                        srR.Close();
                    }
                    //<Send old records
                    //
                }
                else
                {
                    StreamWriter srW = new StreamWriter(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\tool_log.log", true, System.Text.Encoding.Default);
                    srW.Write(EncodeBase64(wholeMsg));
                    srW.Close();
                }
            }
            catch
            {
                StreamWriter srW = new StreamWriter(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\tool_log.log", true, System.Text.Encoding.Default);
                srW.Write(EncodeBase64(wholeMsg));
                srW.Close();
            }
        }

        public void sendTCPMsg(string SendStr)
        {
            Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mySocket.Connect("10.185.59.51", 9991);

            byte[] byteMsg;
            byteMsg = Encoding.ASCII.GetBytes(SendStr);
            mySocket.Send(byteMsg);
            mySocket.Shutdown(SocketShutdown.Both);
            mySocket.Close();

        }


        public string EncodeBase64(Encoding encode, string source)
        {
            string enstring = "";
            byte[] bytes = encode.GetBytes(source);
            try
            {
                enstring = Convert.ToBase64String(bytes);
            }
            catch
            {
                enstring = source;
            }
            return enstring;
        }

        /// <summary>
        /// Base64 Encode,with UTF8
        /// </summary>
        /// <param name="source">Words to be Encoded</param>
        /// <returns>Words after Encoded</returns>
        public string EncodeBase64(string source)
        {
            return EncodeBase64(Encoding.UTF8, source);
        }

        /// <summary>
        /// Base64 Decode
        /// </summary>
        /// <param name="codeName"></param>
        /// <param name="result">Secert words to be Decoded</param>
        /// <returns>Words after Decoded</returns>
        public string DecodeBase64(Encoding encode, string result)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }

        /// <summary>
        /// Base64 Decode，with UTF8
        /// </summary>
        /// <param name="result">Words to be Decoded</param>
        /// <returns>Words after Decoded</returns>
        public string DecodeBase64(string result)
        {
            return DecodeBase64(Encoding.UTF8, result);
        }
    }
}
