using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinForms_Connect4
{
    internal class LocalPlayer
    {
        private LocalDBClassesDataContext db = new LocalDBClassesDataContext();
        public int id { get; set; }//number 
        public int currentGameId { get; set; }

        //constructor
        public LocalPlayer()
        {
            //connect to account in the website and get the player's name and id 
            this.LogIn();
            this.showGamesTable();
        }

        public void addGameToDB(Connect4Game game)
        {
            //add game id, player id, true or false if he won
            this.currentGameId = game.GetID();


        }

        public void showGamesTable()
        {
            //fetch all games from the database
            var games = db.Games.ToList();

            if (games.Any())
            {
                //build a message with the game information
                StringBuilder message = new StringBuilder();
                message.AppendLine("Games Table:");
                message.AppendLine("Game ID\tPlayer ID\tPlayer Won");

                foreach (var game in games)
                {
                    message.AppendLine($"{game.Id}\t\t{game.PlayerId}\t\t{game.PlayerWon}");
                }

                //display the message box with the games information
                MessageBox.Show(message.ToString(), "Games Table", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No games found in the table.", "Games Table", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void LogIn()
        {
            //connect to account on the website and get the player's id 
            // Implement your login logic here.
            // For this example, I'm just setting a dummy player ID.
            id = 1;
        }

        internal void AddTurnToDB(int gameId, int currentTurn, int col, bool isLocalPlayerTurn)
        {
            try
            {
                // Create a new SqlCommand to execute the INSERT query
                using (SqlConnection connection = new SqlConnection(db.Connection.ConnectionString))
                {
                    connection.Open();

                    // Construct the INSERT query
                    string insertQuery = "INSERT INTO Turns (GameId, IsPlayerTurn, Played) VALUES (@GameId, @IsPlayerTurn, @Played)";

                    // Create the SqlCommand and set parameters
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@GameId", gameId);
                        command.Parameters.AddWithValue("@IsPlayerTurn", isLocalPlayerTurn);
                        command.Parameters.AddWithValue("@Played", col);

                        // Execute the INSERT query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Turn added successfully.", "Turn Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to add turn to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding turn to the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void showTurnsTable()
        {
            var turns = db.Turns.ToList();
            if (turns.Any())
            {
                //build a message with the turn information
                StringBuilder message = new StringBuilder();
                message.AppendLine("Turns Table:");
                message.AppendLine("Game ID\tIs Player Turn\tPlayed");
                foreach (var turn in turns)
                {
                    message.AppendLine($"{turn.GameId}\t\t{turn.IsPlayerTurn}\t\t{turn.Played}");
                }
                //display the message box with the turn information
                MessageBox.Show(message.ToString(), "Turns Table", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No turns found in the table.", "Turns Table", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }
    }
}
