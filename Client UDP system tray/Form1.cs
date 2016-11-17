using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        }
        bool zamkniecie = false;

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
            hideform();
        }
    }
}
