

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
    public partial class GameForm : Form
    {
        private int currentSelection { get; set; }
        private bool isLocalPlayerTurn { get; set; }
        private List<System.Windows.Forms.Button> buttonList;
        private System.Windows.Forms.Button lastPicked;
        Game game;


        public GameForm()
        {
            InitializeComponent();

        }
  
        public void setGame(Game game)
        {
            this.game = game;
            this.isLocalPlayerTurn = game.IsLocalPlayerTurn;
            if (this.isLocalPlayerTurn)
            {
                this.gameButtonsTurnOn();
            }
        }
        private void initButtonList()
        {
            buttonList.Add(btn1);
            buttonList.Add(btn2);
            buttonList.Add(btn3);
            buttonList.Add(btn4);
            buttonList.Add(btn5);
            buttonList.Add(btn6);
            buttonList.Add(btn7);
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            gameButtonsTurnOff();
            this.initButtonList();


        }

        internal void gameButtonsTurnOff()
        {
           //for each button in buttonList
           foreach (System.Windows.Forms.Button button in buttonList)
            {
                button.Enabled = false;
            }
            resetToolStripMenuItem.Enabled = false;
        }
        internal void gameButtonsTurnOn()
        {
            //for each button in buttonList
            foreach (System.Windows.Forms.Button button in buttonList)
            {
                button.Enabled = true;
            }
            resetToolStripMenuItem.Enabled = true;
        }
        private void switchSelected(int row)
        {
            if (this.lastPicked != null)
                this.lastPicked.BackColor = Color.White;
            this.lastPicked = buttonList[row];
            this.currentSelection = row;
            this.lastPicked.BackColor = Color.Red;
        }

        private void row0_Click(object sender, EventArgs e)
        {
            this.switchSelected(0);
            /*
            string colour = turnColour(g.count());

            int row = g.checkValidSlot(0);
            int win = g.checkVictory();

            if (row == 5)
            {
                cell00.Image = new Bitmap(colour);
                btn1.Enabled = false;
            }
            else if (row == 4)
                cell10.Image = new Bitmap(colour);
            else if (row == 3)
                cell20.Image = new Bitmap(colour);
            else if (row == 2)
                cell30.Image = new Bitmap(colour);
            else if (row == 1)
                cell40.Image = new Bitmap(colour);
            else if (row == 0)
                cell50.Image = new Bitmap(colour);

            displayVictoryMessage(win);
            */
        }

     
        private void row1_Click(object sender, EventArgs e)
        {
            this.switchSelected(1);
            /*
            string colour = turnColour(g.count());

            int row = g.checkValidSlot(1);
            int win = g.checkVictory();

            if (row == 5)
            {
                cell01.Image = new Bitmap(colour);
                btn2.Enabled = false;
            }
            else if (row == 4)
                cell11.Image = new Bitmap(colour);
            else if (row == 3)
                cell21.Image = new Bitmap(colour);
            else if (row == 2)
                cell31.Image = new Bitmap(colour);
            else if (row == 1)
                cell41.Image = new Bitmap(colour);
            else if (row == 0)
                cell51.Image = new Bitmap(colour);

            displayVictoryMessage(win);
            */
        }

        private void row2_Click(object sender, EventArgs e)
        {
            this.switchSelected(2);
        }

        private void row3_Click(object sender, EventArgs e)
        {
           this.switchSelected(3);
        }

        private void row4_Click(object sender, EventArgs e)
        {
            this.switchSelected(4);
        }

        private void row5_Click(object sender, EventArgs e)
        {
            this.switchSelected(5);
        }

        private void row6_Click(object sender, EventArgs e)
        {
           this.switchSelected(6);
        }

      private void makeBtnWhite()
        {
            //for each button in buttonList set the background color to white
            foreach (System.Windows.Forms.Button button in buttonList)
            {
                button.BackColor = Color.White;
            }
        }

        public string turnColour(int k) // k is the value returned by the count function for the turn
        {
            if(k%2==0)
            {
                makeBtnWhite();
                return "blackChecker.png";
			}
            else
            {
                btn1.BackColor = Color.Tan;
                btn2.BackColor = Color.Tan;
                btn3.BackColor = Color.Tan;
                btn4.BackColor = Color.Tan;
                btn5.BackColor = Color.Tan;
                btn6.BackColor = Color.Tan;
                btn7.BackColor = Color.Tan;

                return "redChecker.png";
            }
        }

        // menu buttons
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {

            btn1.Enabled = true;
            btn2.Enabled = true;
            btn3.Enabled = true;
            btn4.Enabled = true;
            btn5.Enabled = true;
            btn6.Enabled = true;
            btn7.Enabled = true;
            startToolStripMenuItem.Enabled = false;
            resetToolStripMenuItem.Enabled = true;
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart(); // simply restart the application to reset everything
            Environment.Exit(0); // clean shutdown
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Game developed by Samuel Mediani and other students during an extracurricular school course on C#. \n\nTechnologies used: Windows Forms, Visual Studio. \n\nYear: 2022", this.Text);
        }
       
        // show the outcome of the game (according to win)
        private void displayVictoryMessage(int win)
        {
            if (win == 1)
            {
                MessageBox.Show("Player 1 won the match!", this.Text);
                this.Close();
            }
            else if (win == 2)
            {
                MessageBox.Show("Player 2 won the match!", this.Text);
                this.Close();
            }
            else if (win == 3)
            {
                MessageBox.Show("Draw!", this.Text);
                this.Close();
            }
        }
    }
}
