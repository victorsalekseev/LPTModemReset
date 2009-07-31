using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace ModemReset
{
    public partial class FormModem : Form
    {
        NIcon nic;
        int adress = 888;
        public FormModem()
        {
            InitializeComponent();
            Init();
        }

        public FormModem(int addrr)
        {
            InitializeComponent();
            adress = addrr;
            Init();
        }

        private void Init()
        {
            this.checkBoxD0.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            this.checkBoxD1.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            this.checkBoxD2.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            this.checkBoxD3.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            this.checkBoxD4.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            this.checkBoxD5.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            this.checkBoxD6.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            this.checkBoxD7.CheckedChanged += new EventHandler(checkBox_CheckedChanged);

            this.Load += new EventHandler(FormModem_Load);
            this.FormClosing += new FormClosingEventHandler(FormModem_FormClosing);
            this.выходToolStripMenuItem.Click += new EventHandler(выходToolStripMenuItem_Click);
            this.Icon = global::ModemReset.Properties.Resources.Icon;
            timer_reset.Enabled = true;
            timer_reset.Interval = 5000;
            timer_reset.Tick += new EventHandler(timer_reset_Tick);
            Reset_DS();
        }

        void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            PortNumCalc();
        }

        private void PortNumCalc()
        {
            int value = 0;

            if (checkBoxD0.Checked)
            {
                value += (int)Math.Pow(2, 0);
                LoadPict(true, pictureBoxD0);
            }
            else
                LoadPict(false, pictureBoxD0);
            value += 0;

            if (checkBoxD1.Checked)
            {
                value += (int)Math.Pow(2, 1);
                LoadPict(true, pictureBoxD1);
            }
            else
                LoadPict(false, pictureBoxD1);
            value += 0;

            if (checkBoxD2.Checked)
            {
                value += (int)Math.Pow(2, 2);
                LoadPict(true, pictureBoxD2);
            }
            else
                LoadPict(false, pictureBoxD2);
            value += 0;

            if (checkBoxD3.Checked)
            {
                value += (int)Math.Pow(2, 3);
                LoadPict(true, pictureBoxD3);
            }
            else
                LoadPict(false, pictureBoxD3);
            value += 0;

            if (checkBoxD4.Checked)
            {
                value += (int)Math.Pow(2, 4);
                LoadPict(true, pictureBoxD4);
            }
            else
                LoadPict(false, pictureBoxD4);
            value += 0;

            if (checkBoxD5.Checked)
            {
                value += (int)Math.Pow(2, 5);
                LoadPict(true, pictureBoxD5);
            }
            else
                LoadPict(false, pictureBoxD5);
            value += 0;

            if (checkBoxD6.Checked)
            {
                value += (int)Math.Pow(2, 6);
                LoadPict(true, pictureBoxD6);
            }
            else
                LoadPict(false, pictureBoxD6);
            value += 0;

            if (checkBoxD7.Checked)
            {
                value += (int)Math.Pow(2, 7);
                LoadPict(true, pictureBoxD7);
            }
            else
                LoadPict(false, pictureBoxD7);
            value += 0;

            PortAccess.Output(adress, value);
        }

        private void Reset_DS() // Makes all the data pins low so the LED's turned off
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "report.log"), true, Encoding.UTF8))
            {
                sw.WriteLine("RST IN: " + DateTime.Now.Ticks.ToString());
                sw.Close();
            }
            PortAccess.Output(adress, 0);
            Thread.Sleep(1000);
        }

        private void Reset_DS_UI()
        {
            this.checkBoxD0.Checked = false;
            this.checkBoxD1.Checked = false;
            this.checkBoxD2.Checked = false;
            this.checkBoxD3.Checked = false;
            this.checkBoxD4.Checked = false;
            this.checkBoxD5.Checked = false;
            this.checkBoxD6.Checked = false;
            this.checkBoxD7.Checked = false;

            LoadPict(false, pictureBoxD0);
            LoadPict(false, pictureBoxD1);
            LoadPict(false, pictureBoxD2);
            LoadPict(false, pictureBoxD3);
            LoadPict(false, pictureBoxD4);
            LoadPict(false, pictureBoxD5);
            LoadPict(false, pictureBoxD6);
            LoadPict(false, pictureBoxD7);
        }

        private void LoadPict(bool IsOn, PictureBox pb)
        {
            if (IsOn)
            {
                pb.Image = global::ModemReset.Properties.Resources.p_on;
            }
            else
            {
                pb.Image = global::ModemReset.Properties.Resources.p_off;
            }
        }

        void timer_reset_Tick(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "report.log"));
            if (DateTime.Now > fi.LastWriteTime.AddSeconds(Convert.ToDouble(numericUpDown_reset.Value)))
            {
                //reset!!!
                Reset_DS();
            }
            PortNumCalc();
        }

        void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nic.CloseApp();
        }

        void FormModem_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        void FormModem_Load(object sender, EventArgs e)
        {
            nic = new NIcon(contextMenuStrip, this);
        }
    }
}