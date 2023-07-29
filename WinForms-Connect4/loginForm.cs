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
        public loginForm()
        {
            InitializeComponent();
            //set the userid input to be 0-1000
            this.userIdInput.Minimum = 0;
            this.userIdInput.Maximum = 1000;
        }

        private void signUpButton_Click(object sender, EventArgs e)
        {
            //open website www.google.com
            System.Diagnostics.Process.Start("http://www.google.com");
        }

        private void logInButton_Click(object sender, EventArgs e)
        {

        }

    }
}
