/*
 * 
 * This Program is free for ANY use.
 * 
 * 
 * Program written by Janis Dähne
 * 
 * To change network stuff
 * change port: Port
 * 
 * 
 * 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using TextVerteilerClient.Networking;

using System.Net;
using System.Net.Sockets;
using System.Threading;



namespace TextVerteilerClient
{
    public partial class FormMain : Form
    {


        public static string IPADDRESS = "192.168.30.51";

       // public static int MaxReconnectionsTries = 12;

       // public const int CheckReconnectAfterXSeconds = 5;//aller 5 Sekunden Wiederverbinden versuchen 

        private int Port = 8008;

        public ClientContext Client { get; set; }

        public static double LeaveOpacity = 0.3D;

        public List<string> TextHistory;


        public int Pointer { get; set; }

        //muss mit dem Server übereinstimmen (darf nur pakete je 42000 bytes) sonst kommt nur ein teil der naricht an
        public static int MAXBits = 42000; // = 21.000 chars, da unicode 2 bytes

        public int LazyCounter { get; set; }//nach XX sec automatisch verstecken

        public int MaxLazy { get; set; }

        public UdpClientContext UdpClientcontext;

        public static int HistoryStackSize { get; set; }

        public static string ConnectionString { get; set; }


        public int CheckStatusCounter { get; set; } //0 to 10

       // public static int TryReconnectCounter = 0;

       // public int CheckReconnectionCounter = 0;



        public static bool Reconnecting = false;

        public static bool WaitForServer = true;

        public static List<string> KnownDns = new List<string>();

        ToolTip tip1 = new ToolTip();
        ToolTip tip2 = new ToolTip();
        ToolTip tip3 = new ToolTip();

        public FormMain()
        {
            InitializeComponent();

            tbMessage.AllowDrop = true;
        }

        public void FormMain_Load(object sender, EventArgs e)
        {
            //weil visible das hier auslöst
            ResetPosition();
            this.TopMost = true;
        }

        public void IniFormMain()
        {


            this.Opacity = 0;
            this.Show();
            this.Hide();
            this.Opacity = 1;

            TextHistory = new List<string>();

            ResetPosition();
            this.TopMost = true;
            this.CreateControl();
            this.TopMost = true;

            FormMain.HistoryStackSize = 20;

            tip1.UseFading = false;
            tip1.UseAnimation = false;
            tip1.ShowAlways = false;
            tip1.InitialDelay = 500;

            tip2.UseFading = false;
            tip2.UseAnimation = false;
            tip2.ShowAlways = false;
            tip2.InitialDelay = 500;

            tip3.UseFading = false;
            tip3.UseAnimation = false;
            tip3.ShowAlways = false;
            tip3.InitialDelay = 500;

            tip1.SetToolTip(btnKopieren, "Makiert den gesamten Text und kopiert ihn in die Zwischenablage.");
            //tip2.SetToolTip(btnNext, "Ruft den nächsten empfangenen Text ab.");
            //tip3.SetToolTip(btnPrevious, "Ruft den zuvor empfangenen Text ab.");

            //string ip = Config.LoadIp();

            //Program.fmEinstellungen.tbIP.Text = ip;

            //connect on startup
            // Program.fmEinstellungen.btnConnect_Click(null, new EventArgs());

            UdpClientcontext = new UdpClientContext();


        }


        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIconFmMain.ShowBalloonTip(1000);
            this.Visible = false;
            e.Cancel = true;

        }

        public void SetText(string Text)
        {
            if (tbMessage.InvokeRequired)
            {
                Action<string> set = new Action<string>(SetText);
                this.Invoke(set, new object[] { Text });
            }
            else
            {
                tbMessage.Clear();
                // tbMessage.Text = Text;
                // tbMessage.AppendText(Text);
                tbMessage.Rtf = Text;
            }

        }

        public void AppendText(string Text)
        {
            if (tbMessage.InvokeRequired)
            {
                Action<string> set = new Action<string>(SetText);
                this.Invoke(set, new object[] { Text });
            }
            else
            {
                tbMessage.Rtf += Text;
            }
        }

        public void SetFormText(string Text)
        {
            if (this.InvokeRequired)
            {
                Action<string> set = new Action<string>(SetFormText);
                this.Invoke(set, new object[] { Text });
            }
            else
            {
                this.Text = Text;
            }
        }

        public void SetFormVisibility(bool visible, bool noch_mal)
        {
            if (this.InvokeRequired || noch_mal)
            {
                Action<bool, bool> set = new Action<bool, bool>(SetFormVisibility);
                noch_mal = false;
                this.Invoke(set, new object[] { visible, noch_mal });
            }
            else
            {
                this.Visible = visible;
            }
        }


        public void AddServerIpToCombobox(string HostName)
        {
            if (Program.fmEinstellungen.cbServerIps.InvokeRequired)
            {
                Action<string> set = new Action<string>(AddServerIpToCombobox);
                this.Invoke(set, new object[] { HostName });
            }
            else
            {
                KnownDns.Add(HostName);
                if (Program.fmEinstellungen.cbServerIps.Items.Count == 0)
                {
                    Program.fmEinstellungen.cbServerIps.Items.Add(HostName);
                    Program.fmEinstellungen.cbServerIps.SelectedIndex = 0;
                    //connect
                    Program.fmEinstellungen.btnConnect_Click(this, new EventArgs());
                }
                else
                {
                    //just add
                    Program.fmEinstellungen.cbServerIps.Items.Add(HostName);
                }
            }
        }

        public void Connect(IPAddress RemoteIp, bool Reconnect)
        {
            Client = new ClientContext(ref TextHistory, MAXBits);
            Client.OnDataReceived += SetText;
            Client.OnDataReceived2 += SetFormVisibility;

            Client.BeginConnect(RemoteIp, this.Port, Reconnect);

        }


        public void DisconnectByButton()
        {
            if (Client != null)
            {
                Client.Close();
                Client = null;
            }
        }

        public void DisconnectByRemoteFault(string Try)
        {

            //Program.fmEinstellungen.btnDisconnect_Click(null, new EventArgs());

            SetFormText("Reconnecting...Try " + Try);

            //Program.fmEinstellungen.SetBtnConnectVisible(true);
            //Program.fmEinstellungen.SetBtnDisconnectVisible(false);

            Client.Close();
            Client = null;

            //if (TryReconnectCounter == 0)
            //{
            //    TryReconnectCounter = MaxReconnectionsTries; // 5
            //}

        }


        public void ResetPosition()
        {
            Point p = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width,
                   Screen.PrimaryScreen.WorkingArea.Bottom - this.Height);
            //((Screen.PrimaryScreen.Bounds.Bottom - Screen.PrimaryScreen.WorkingArea.Bottom) = taskbar height);
            this.Location = p;
        }

        private void SetEnterOpacity()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    if (btnNext.InvokeRequired)
                    {
                        //error ???
                        int id = System.Threading.Thread.CurrentThread.ManagedThreadId;
                        //Action set = new Action(SetEnterOpacity);
                        //this.Invoke(set);
                    }
                }
                else
                {
                    this.Opacity = 1.0;

                }

            }
            catch (Exception e)
            {

            }


        }

        private void SetLeaveOpacity()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    if (btnNext.InvokeRequired)
                    {
                        //error ???
                        int id = System.Threading.Thread.CurrentThread.ManagedThreadId;
                        //Action set = new Action(SetLeaveOpacity);
                        //this.Invoke(set);
                    }
                }
                else
                {
                    this.Opacity = LeaveOpacity;
                }

            }
            catch (Exception e)
            {

            }



        }

        public void StopReconnecting()
        {
            Reconnecting = false;
           // TryReconnectCounter = -1;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (this.DesktopBounds.IntersectsWith(new Rectangle(Cursor.Position.X, Cursor.Position.Y, 1, 1)))
            {
                SetEnterOpacity();
            }
            else
            {
                SetLeaveOpacity();
                int id = System.Threading.Thread.CurrentThread.ManagedThreadId;

                //nur wenn fenster auch wirklich sichtbar
                if (this.Visible == true)
                {
                    if (LazyCounter >= MaxLazy && MaxLazy > 0)
                    {
                        this.Visible = false;
                        LazyCounter = 0;
                    }

                    if (MaxLazy > 0)
                    {
                        LazyCounter++;
                    }
                    else
                    {
                        LazyCounter = 0;
                    }
                }
            }

            if (CheckStatusCounter >= 10)
            {
                if (Client != null)
                {
                    if (!Client.isConnected())
                    {

                        switch (FormMain.WaitForServer)
                        {
                            case true:
                                this.SetFormText("Waiting for Server " + KnownDns[UdpClientContext.SelectedIP]);
                                Reconnecting = true;
                                //DisconnectByButton();
                                break;

                            case false:
                               
                                //if (TryReconnectCounter == 0 && Reconnecting == false)
                                //{
                                //    Reconnecting = true;
                                //    DisconnectByRemoteFault("1/" + MaxReconnectionsTries);
                                //}
                                //else if (TryReconnectCounter == 0)
                                //{
                                //    //Reconnecting = false;
                                //    //TryReconnectCounter = -1;
                                //    StopReconnecting();
                                //}
                                //else
                                //{
                                //    if (Reconnecting)
                                //    {
                                //        DisconnectByRemoteFault(((MaxReconnectionsTries + 1) - TryReconnectCounter) + "/" + MaxReconnectionsTries);
                                //    }
                                //}

                                Program.fmEinstellungen.btnDisconnect_Click(this, new EventArgs());
                                break;
                            default:
                                break;
                        }
                       
                    }
                    else
                    {

                    }
                }

                this.CheckStatusCounter = 0;
            }


            //if (TryReconnectCounter > 0)
            //{
            //    if (CheckReconnectionCounter * 100 >= CheckReconnectAfterXSeconds * 1000)
            //    {
            //        //try to connect
            //        Program.fmEinstellungen.Reconnect(((MaxReconnectionsTries + 1) - TryReconnectCounter) + "/" + MaxReconnectionsTries);
            //        TryReconnectCounter--;
            //        CheckReconnectionCounter = 0;
            //        this.CheckStatusCounter = 0; //to save this 1 second
            //    }
            //    CheckReconnectionCounter++;
            //}

            this.CheckStatusCounter++;

        }

        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.fmEinstellungen.Visible = true;
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shutdown();
        }

        public void Shutdown()
        {
            DisconnectByButton();
            notifyIconFmMain.Dispose();
            Program.ExitAndSaveIp();
        }

        private void notifyIconFmMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.Visible == true)
                {
                    this.Visible = false;
                }
                else
                {
                    this.Visible = true;
                }

            }
        }


        public void SetNewPointerPos(int Position)
        {
            this.Pointer = Position - 1;
            if (Position == 2) // jetzt haben wir 2 einträge zum navigieren
            {
                if (btnPrevious.InvokeRequired)
                {
                    Action<int> set = new Action<int>(SetNewPointerPos);
                    this.Invoke(set, new object[] { Position });
                }
                else
                {
                    btnPrevious.Enabled = true;
                }
            }
            else if (Position > 2)
            {
                if (btnPrevious.InvokeRequired)
                {
                    Action<int> set = new Action<int>(SetNewPointerPos);
                    this.Invoke(set, new object[] { Position });
                }
                else
                {
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = false;
                }

            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            Pointer--;
            SetTextFromHistory();
            if (Pointer == 0)
            {
                btnPrevious.Enabled = false;
                btnNext.Enabled = true;
            }
            else
            {
                btnPrevious.Enabled = true;
                btnNext.Enabled = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Pointer++;
            SetTextFromHistory();
            if (Pointer == TextHistory.Count - 1)
            {
                btnNext.Enabled = false;
                btnPrevious.Enabled = true;
            }
            else
            {
                btnPrevious.Enabled = true;
                btnNext.Enabled = true;
            }
        }

        private void SetTextFromHistory()
        {
            try
            {
                if (Pointer >= 0 && Pointer < this.TextHistory.Count)
                {
                    this.tbMessage.Clear();
                    tbMessage.Rtf = this.TextHistory[Pointer];
                    tbMessage.SelectAll();
                }
            }
            catch (Exception e)
            {

            }


        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                tbMessage.SelectAll();
            }

        }



        private void btnKopieren_Click(object sender, EventArgs e)
        {
            tbMessage.SelectAll();
            tbMessage.Copy();
        }



    }
}
