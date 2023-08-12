using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_Connect4
{
    public partial class loginForm : Form
    {
        public string ip { get; set; }
        public bool playLocal { get; set; }
        public loginForm(string ip)
        {
            InitializeComponent();
            //set the userid input to be 0-1000
            this.userIdInput.Minimum = 0;
            this.userIdInput.Maximum = 1000;
            this.ip = ip;
            this.playLocal = false;
        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            //open website
            System.Diagnostics.Process.Start("https://"+this.ip+":7148/SignInPage");
        }

        private void logInButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.playLocal = true;
            this.Close();
        }
    }
}
