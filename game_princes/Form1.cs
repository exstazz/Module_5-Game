using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game_princes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Program program = new Program();
            program.set_cursor();
            Process.Start("game_princes.exe");
            Process.GetCurrentProcess().Kill();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Program program = new Program();
            program.set_cursor();
            MessageBox.Show("Нажмите 'ОК' для подтверждения выхода");
            Process.GetCurrentProcess().Kill();
        }
    }
}
