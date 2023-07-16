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
        private bool isLocalPlayerTurn { get; set; }
        private List<System.Windows.Forms.Button> rowButtonList;
        private System.Windows.Forms.Button lastPicked;
        internal int currentSelectedRow { get; set; }
        Game game;

        public GameForm()
        {
            InitializeComponent();
        }

        public void SetGame(Game game)
        {
            this.game = game;
            this.isLocalPlayerTurn = game.IsLocalPlayerTurn;
            if (this.isLocalPlayerTurn)
            {
                GameButtonsTurnOn();
            }
        }

        private void InitButtonList()
        {
            this.rowButtonList = new List<System.Windows.Forms.Button>
            {
                row0,
                row1,
                row2,
                row3,
                row4,
                row5,
                row6
            };
            this.MakeBtnWhite();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

            InitButtonList();
            GameButtonsTurnOff();
        }

        internal void GameButtonsTurnOff()
        {
            foreach (System.Windows.Forms.Button button in rowButtonList)
            {
                button.Enabled = false;
            }
  
        }

        internal void GameButtonsTurnOn()
        {
            foreach (System.Windows.Forms.Button button in rowButtonList)
            {
                button.Enabled = true;
            }
           
        }

        private void SwitchSelected(int row)
        {
            if (this.lastPicked != null)
                this.lastPicked.BackColor = Color.White;
            this.lastPicked = rowButtonList[row];
            this.lastPicked.BackColor = Color.Red;
            this.currentSelectedRow = row;
        }

        private void Row_Click(object sender, EventArgs e, int row)
        {
            SwitchSelected(row);
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

        private void row0_Click(object sender, EventArgs e)
        {
            Row_Click(sender, e, 0);
        }

        private void row1_Click(object sender, EventArgs e)
        {
            Row_Click(sender, e, 1);
        }

        private void row2_Click(object sender, EventArgs e)
        {
            Row_Click(sender, e, 2);
        }

        private void row3_Click(object sender, EventArgs e)
        {
            Row_Click(sender, e, 3);
        }

        private void row4_Click(object sender, EventArgs e)
        {
            Row_Click(sender, e, 4);
        }

        private void row5_Click(object sender, EventArgs e)
        {
            Row_Click(sender, e, 5);
        }

        private void row6_Click(object sender, EventArgs e)
        {
            Row_Click(sender, e, 6);
        }

        private void MakeBtnWhite()
        {
            foreach (System.Windows.Forms.Button button in rowButtonList)
            {
                button.BackColor = Color.White;
            }
        }

        public string TurnColour(int k) // k is the value returned by the count function for the turn
        {
            if (this.game.IsLocalPlayerTurn)
                return "blackChecker.png";
            else
                return "redChecker.png";

        }

        // menu buttons
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.game.StartGame();
        }
        // show the outcome of the game (according to win)
        private void DisplayVictoryMessage(int win)
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

        private void apply_Click(object sender, EventArgs e)
        {

        }
    }
}
