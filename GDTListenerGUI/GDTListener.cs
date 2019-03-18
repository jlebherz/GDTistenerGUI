using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Ini;

namespace GDTListenerGUI
{
    public partial class GDTListener : Form
    {

        System.Timers.Timer T1 = new System.Timers.Timer();
        IniFile ini = new IniFile(".\\settings.ini");

        public GDTListener(string[] args)
        {
            InitializeComponent();

            bool Started = false;
            if(args.Length>0)if(Started == false && args[0] == "--autostart")
            {
                double CycleTime = Convert.ToDouble(ini.IniReadValue("GDTListenerService", "CycleTime"));
                buttonStart.Enabled = false;
                buttonStop.Enabled = true;
                T1.Interval = (CycleTime);
                T1.AutoReset = true;
                T1.Enabled = true;
                T1.Start();
                T1.Elapsed += new System.Timers.ElapsedEventHandler(T1_Elapsed);
                Started = true;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            double CycleTime = Convert.ToDouble(ini.IniReadValue("GDTListenerService", "CycleTime"));
            buttonStart.Enabled = false;
            buttonStop.Enabled = true;            
            T1.Interval = (CycleTime);
            T1.AutoReset = true;
            T1.Enabled = true;
            T1.Start();
            T1.Elapsed += new System.Timers.ElapsedEventHandler(T1_Elapsed);
            
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = true;
            buttonStop.Enabled = false;
            T1.Stop();
        }

        private void T1_Elapsed(object sender, EventArgs e)
        {
            //labelLast.Text = DateTime.Now.ToString();

            //label1.Text = DateTime.Now.ToString();



            //  labelLast.Text = DateTime.Now.ToString("HH:mm:ss");
            //  labelLast.Text = "HALLO";

            Worker(DateTime.Now.ToString());
        }

        void Worker(string TimeStamp)
        {

            //label1.Text = TimeStamp;
            string GDTFile = ini.IniReadValue("GDTListenerService", "GDTFile");
            string ApplicationFile = ini.IniReadValue("GDTListenerService", "ApplicationFile");
            int WaitTime = Convert.ToInt32(ini.IniReadValue("GDTListenerService", "WaitTime"));
            string MoveMode = ini.IniReadValue("GDTListenerService", "MoveMode");
            string RemoteGDTFileIN = ini.IniReadValue("GDTListenerService", "RemoteGDTFileIN");
            string LocalGDTFileIN = ini.IniReadValue("GDTListenerService", "LocalGDTFileIN");
            string RemoteGDTFileOut = ini.IniReadValue("GDTListenerService", "RemoteGDTFileOut");
            string LocalGDTFileOut = ini.IniReadValue("GDTListenerService", "LocalGDTFileOut");


            if (File.Exists(GDTFile) && MoveMode == "false")
            {
                Process.Start(ApplicationFile);
                T1.Stop();
                Thread.Sleep(WaitTime);
                T1.Start();
            }

            if (File.Exists(RemoteGDTFileIN) && MoveMode == "true")
            {
                File.Move(RemoteGDTFileIN, LocalGDTFileIN);
                Process.Start(ApplicationFile);
                T1.Stop();
                Thread.Sleep(WaitTime);
                if(buttonStop.Enabled == true)T1.Start();
            }

            if (File.Exists(LocalGDTFileOut) && MoveMode == "true")
            {
                File.Move(LocalGDTFileOut, RemoteGDTFileOut);
            }

            FileStream TS = new FileStream(@".\log.txt", FileMode.OpenOrCreate);
            string lines = "Last Run " + DateTime.Now.ToString();
            System.IO.StreamWriter file = new System.IO.StreamWriter(TS);
            file.WriteLine(lines);
            file.Close();
            TS.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://iq-5.de");
        }
    }
}
