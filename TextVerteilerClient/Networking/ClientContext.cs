using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;
using System.Net;

using System.Windows.Forms;

namespace TextVerteilerClient.Networking
{
    public class ClientContext
    {
        public Socket socket { get; set; }

        private byte[] Buffer;

        public AsyncCallback BeginConnectCallback { get; private set; }

        public AsyncCallback BeginReceiveFromCallback { get; private set; }

        IPEndPoint Server;

        public List<string> TextStack { get; private set; }

        public event Action<string> OnDataReceived;

        /// <summary>
        /// To make form visible.
        /// </summary>
        public event Action<bool, bool> OnDataReceived2;

        private StringBuilder CurrentString = new StringBuilder();

        private MessageType LastReceivedMessageType;



        public ClientContext(ref List<string> TextHistory, int BufferSize)
        {


            this.TextStack = TextHistory;

            Server = new IPEndPoint(IPAddress.Loopback, 8008);

            BeginConnectCallback = new AsyncCallback(OnConnect);

            BeginReceiveFromCallback = new AsyncCallback(OnTextReceived);


            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            socket.ReceiveBufferSize = BufferSize;

        }


        public void BeginConnect(IPAddress ipaddress, int port)
        {
            try
            {
                socket.BeginConnect(ipaddress, port, BeginConnectCallback, null);
            }
            catch (Exception e)
            {
            }

        }

        public void Close()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch (Exception e)
            {
            }
        }


        public void OnConnect(IAsyncResult result)
        {
            try
            {
                //ok finished
                socket.EndConnect(result);

                FormMain.ConnectionString = "Connected to " + Program.fmEinstellungen.GetCurrentHostName();
                //Program.fmMain.SetFormText("Connected to " + socket.RemoteEndPoint.ToString().Split(':')[0]);
                Program.fmMain.SetFormText("Connected to " + Program.fmEinstellungen.GetCurrentHostName());

                StartReceive();
            }
            catch (Exception e)
            {
                Program.fmMain.SetFormText("Verbindungsfehler");
                Program.fmEinstellungen.SetBtnConnectVisible(true);
                Program.fmEinstellungen.SetBtnDisconnectVisible(false);
            }

        }

        public void StartReceive()
        {
            try
            {
                EndPoint p = socket.RemoteEndPoint;
                if (socket.Connected)
                {
                    //muss großer oder gleich der naricht sein....
                    Buffer = new byte[socket.ReceiveBufferSize];

                    socket.BeginReceiveFrom(Buffer, 0, Buffer.Length, SocketFlags.None,
                           ref p, BeginReceiveFromCallback, null);
                }
            }
            catch (Exception)
            {
                
            }
        }

        public void OnTextReceived(IAsyncResult result)
        {

            MessageType isSpltttedMessage = MessageType.plain;
            try
            {

                EndPoint p = socket.RemoteEndPoint;

                if (socket.Connected)
                {
                    socket.EndReceiveFrom(result, ref p);

                    string s = Buffer.GetString(out isSpltttedMessage);

                    if (isSpltttedMessage == MessageType.multiple)
                    {
                        //wait for other pieces
                        this.CurrentString.Append(s);
                        Program.fmMain.SetFormText("Receiving... | " + FormMain.ConnectionString);
                    }
                    else if (isSpltttedMessage == MessageType.last)
                    {

                        this.CurrentString.Append(s);

                        if (this.TextStack.Count() < FormMain.HistoryStackSize)
                        {
                            this.TextStack.Add(CurrentString.ToString());
                        }
                        else if (this.TextStack.Count() >= FormMain.HistoryStackSize)
                        {
                            //am anfang löschen bis passt
                            while (this.TextStack.Count() >= FormMain.HistoryStackSize)
                            {
                                this.TextStack.RemoveAt(0);
                            }

                            this.TextStack.Add(CurrentString.ToString());
                        }

                        Program.fmMain.SetNewPointerPos(TextStack.Count);

                        if (OnDataReceived != null)
                        {
                            OnDataReceived(CurrentString.ToString());
                        }
                        if (OnDataReceived2 != null)
                        {
                            OnDataReceived2(true, false);
                        }

                        Program.fmMain.SetFormText(FormMain.ConnectionString);
                        this.CurrentString = new StringBuilder();
                    }
                    else
                    {
                        if (s != "")
                        {

                            if (this.TextStack.Count() < FormMain.HistoryStackSize)
                            {
                                this.TextStack.Add(s);
                            }
                            else if (this.TextStack.Count() >= FormMain.HistoryStackSize)
                            {
                                //am anfang löschen bis passt
                                while (this.TextStack.Count() >= FormMain.HistoryStackSize)
                                {
                                    this.TextStack.RemoveAt(0);
                                }

                                this.TextStack.Add(s);
                            }


                            Program.fmMain.SetNewPointerPos(TextStack.Count);

                            //Program.fmMain.SetFormVisibility(true,true);

                            if (OnDataReceived != null)
                            {
                                OnDataReceived(s);
                            }
                            if (OnDataReceived2 != null)
                            {
                                OnDataReceived2(true, false);
                            }
                        }
                    }

                    //StartReceive(); //receive next

                }
            }
            catch (Exception e)
            {

            }


            LastReceivedMessageType = isSpltttedMessage;

            StartReceive(); //receive next

        }



        public bool isConnected()
        {
            if (socket != null && socket.Connected)
            {
                bool part1 = socket.Poll(1000, SelectMode.SelectRead);
                bool part2 = (socket.Available == 0);
                if (part1 & part2)
                    return false;
                else
                    return true;
            }
            else
            {
                return false;
            }
        }


    }

    public enum MessageType
    {
        plain,

        //more than 1
        multiple,
        last
    }

    public static class ExtString
    {
        //[System.Diagnostics.DebuggerHidden()]
        public static string GetString(this byte[] Text,out MessageType SplittetMessage)
        {

            StringBuilder b = new StringBuilder();

            if (Text[0] == 2) //wegen isSplittetMessage bytes
            {
                SplittetMessage = MessageType.multiple;
                for (int i = 2; i < Text.Length; i++)
                {
                    if (Text[i] != 0)
                    {
                        char c = (char)Text[i];
                        b.Append(c);
                    }
                }
            }
            else if (Text[0] == 3)
            {
                SplittetMessage = MessageType.last;
                for (int i = 2; i < Text.Length; i++)
                {
                    if (Text[i] != 0)
                    {
                        char c = (char)Text[i];
                        b.Append(c);
                    }
                }
            }
            else
            {
                SplittetMessage = MessageType.plain;
                for (int i = 0; i < Text.Length; i++)
                {
                    if (Text[i] != 0)
                    {
                        char c = (char)Text[i];
                        b.Append(c);
                    }
                }
            }

            

            return b.ToString();

        }

    }
}
