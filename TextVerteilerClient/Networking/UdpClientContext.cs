using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace TextVerteilerClient.Networking
{
    public class UdpClientContext
    {


        public UdpClient UdpServerSearcher;

        public static string Broadscast = "224.0.0.255"; // 224.0.0.255-239.255.255.255 port 2222

        public static int BroadcastPort = 2222; //egal... da nur theoretischer (multicast)client

        public static List<string> KnownIPs = new List<string>();

        public static int SelectedIP;

        public static IPEndPoint BroadcastServer = new IPEndPoint(IPAddress.Parse(Broadscast), BroadcastPort);

        AsyncCallback BeginReceiveUPDserver;
        

        public UdpClientContext()
        {
            BeginReceiveUPDserver = new AsyncCallback(OnBeginReceiveUPDserverFinished);


            UdpServerSearcher = new UdpClient(BroadcastServer.Port, AddressFamily.InterNetwork);
            UdpServerSearcher.JoinMulticastGroup(BroadcastServer.Address, 1); //1 router hops for just local network

            UdpServerSearcher.BeginReceive(BeginReceiveUPDserver, null);

        }


        public void OnBeginReceiveUPDserverFinished(IAsyncResult result)
        {
            try
            {
                byte[] ReplyMessage = UdpServerSearcher.EndReceive(result, ref BroadcastServer);
                MessageType empty;
                string answer = ReplyMessage.GetString(out empty);
                string ip = "";

                if (answer.Contains("OK"))
                {
                    ip = answer.Split(':')[1];

                    //check if ip is known

                    int index = (KnownIPs.FindIndex((sip) => sip == ip));

                    if (FormMain.WaitForServer && FormMain.Reconnecting)
                    {
                        if (index == UdpClientContext.SelectedIP)
                        {
                            Program.fmEinstellungen.btnConnect_Click(this, new EventArgs());
                        }
                    }

                    if (index == -1) // not there
                    {
                        KnownIPs.Add(ip);
                        //add to combobox
                        IPAddress ReadlIP;

                        bool r = IPAddress.TryParse(ip, out ReadlIP);

                        if (r)
                        {

                            string hostname = Dns.GetHostEntry(ip).HostName;
                            Program.fmMain.AddServerIpToCombobox(hostname);

                            

                        }
                    }
                    //found a server...
                    //Program.fmEinstellungen.btnConnect_Click(null, new EventArgs());


                }
            }
            catch (Exception e)
            {

            }
            //for more than 1 server
            UdpServerSearcher.BeginReceive(BeginReceiveUPDserver, null);



        }


    }
}
