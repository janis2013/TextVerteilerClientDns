﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace TextVerteilerClient
{
    static class Program
    {

        public static FormMain fmMain;
        public static FormEinstellungen fmEinstellungen;

        public static Mutex mutex;



        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {


            bool isNew;
            mutex = new Mutex(false, Application.ProductName + "_abcxyz16091994", out isNew);

            if (isNew)
            {
                //ok just this instance running
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                fmMain = new FormMain();

                fmEinstellungen = new FormEinstellungen();

                fmMain.IniFormMain();


                Application.Run();

   
            }
            else
            {
                MessageBox.Show("Eine Instanz des Programmes läuft bereits, bitte vorher beenden.", "Fehler", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                //wait, we just want 1 instance running... run to end
            }


            

        }

        public static void ExitAndSaveIp()
        {
            //Config.SaveIp(Program.fmEinstellungen.tbIP.Text);

            Application.ExitThread();
        }
    }

}
