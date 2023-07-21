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
    public partial class ArchiveGamesForm : Form
    {
        public ArchiveGamesForm(LocalDBClassesDataContext db, int playerId)
        {
            InitializeComponent();
            var gameList = (from g in db.Games
                            where g.PlayerId == playerId
                            select g).ToList();

            // Add a null item to the beginning of the list
            gameList.Insert(0, new Game { Id = null });

            GamesCombo.ValueMember = "Id";
            GamesCombo.DataSource = gameList;
            GamesCombo.DisplayMember = "Id";
        }

        internal string GetGameId()
        {
            // Check if a game is selected
            if (GamesCombo.SelectedIndex > 0)
            {
                // Return the game id of the selected game
                return GamesCombo.SelectedValue.ToString();
            }

            // Return null if no game is selected
            return null;
        }
    }
}
