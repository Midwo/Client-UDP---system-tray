using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_UDP_system_tray
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.RunWorkerAsync();
        }
        bool zamkniecie = false;
        delegate void SetTextCallBack(string text);

        private void SetText(string text)
        {
            if (textBox1.InvokeRequired)
            {
                SetTextCallBack f = new SetTextCallBack(SetText);
                this.Invoke(f, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }
        private void openForm()
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }
        private void closeForm()
        {
            zamkniecie = true;
            Close();
        }
        private void hideform()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }


        private void pokażToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openForm();
        }

        private void koniecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeForm();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (zamkniecie == false)
            {
                e.Cancel = true;
                hideform();
                if (backgroundWorker1.IsBusy == false)
                    backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                notifyIcon1.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            closeForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == false)
                backgroundWorker1.RunWorkerAsync();
            hideform();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            openForm();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipText = "Nowa wiadomosć...";
            notifyIcon1.ShowBalloonTip(30);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (UdpClient klient = new UdpClient(25000))
            {
                IPEndPoint IPserver = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 25000);
                Byte[] get = klient.Receive(ref IPserver);
                string txt = Encoding.ASCII.GetString(get);
                this.SetText(txt);
            }
        }
    }
}
