using System;
using System.Collections.Generic;
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
        public string  currentGameId { get; set; }
        private ArchiveGamesForm archiveGamesForm;


        //constructor
        public LocalPlayer()
        {
            //connect to account in the website and get the player's name and id 
            this.id=-1;

        }

        public void addGameToDB(Connect4Game game)
        {
            try
            {
                db.Games.InsertOnSubmit(new Game
                {
                    Id = game.GetID(),
                    PlayerId = id,
                    GameFinished = false,
                    PlayerWon = false,
                    StartTime = DateTime.Now,
                    TimePlayedSeconds = 0
                });

                db.SubmitChanges();
       
            }
            catch (Exception ex)
            {
                // Handle the exception or log the error message
                MessageBox.Show("An error occurred while adding the game to the database: " + ex.Message);
            }
        }

        //function to show all data from db to messagebox
        internal void PrintGames()
        {
            string games = "";
            //for each game print all the data and its turns
            foreach (var game in db.Games)
            {
                games += "game id : " + game.Id.Substring(0, 3) + "\tplayer id:" + game.PlayerId + "\tgame finished ?:" + game.GameFinished + "\tplayer won ?:" + game.PlayerWon + "\ttime played:" + game.TimePlayedSeconds + "\n";
                foreach (var turn in db.Turns)
                {
                    if (turn.GameId == game.Id)
                    {
                        games += "\tturn" + turn.Id + ") " + "game id : " + turn.GameId.Substring(0, 3) + "\tplayer ?:" + turn.IsPlayerTurn + "\tcol played : " + turn.Played + "\n";
                    }
                }
            }
            MessageBox.Show(games);
        }



        public void LogIn(int playerId)
        {
            //connect to account on the website and get the player's id 
            // Implement your login logic here.
            // For this example, I'm just setting a dummy player ID.
            this.id = playerId;

        }

        internal void AddTurnToDB(string gameId, int currentTurn, int col, bool isLocalPlayerTurn)
        {
            db.Turns.InsertOnSubmit(new Turn
            {
                Id = currentTurn,
                GameId = gameId,
                IsPlayerTurn = isLocalPlayerTurn,
                Played = col
            });
            db.SubmitChanges();
            //PrintTurns();
        }
        internal void UpdateGameInDB(string gameId, bool gameFinished, bool playerWon)
        {
            var game = db.Games.Where(g => g.Id == gameId).FirstOrDefault();
            game.GameFinished = gameFinished;
            game.PlayerWon = playerWon;
            game.TimePlayedSeconds = (int)(DateTime.Now - game.StartTime).TotalSeconds;
            db.SubmitChanges();
            //PrintGames();
        }


       //function to print all turns from db to messagebox 
       internal void PrintTurns()
        {
          string turns = "";
            foreach (var turn in db.Turns)
            {
                turns += "turn"+turn.Id + ") " +"game id : "+ turn.GameId.Substring(0, 3) + "\tplayer ?:" + turn.IsPlayerTurn + "\tcol played : " + turn.Played + "\n";
            }
            MessageBox.Show(turns);
        }
       
  

        internal bool ChooseGameFromArchive()
        {//open the archive form and  wait for the platyer choose a game to play 
            this.archiveGamesForm = new ArchiveGamesForm(db,this.id);
            archiveGamesForm.ShowDialog();
            this.currentGameId = archiveGamesForm.GetGameId();
            if (this.currentGameId == null)
            {
                return false;
            }
            return true;

          
        }

        internal void UpdateGameIsFinished(string iD, int win)
        {
            //update the game in the db to finished and if the player won
            bool gameFinished = true;
            bool playerWon = false;
            if (win == 1)
            {
                playerWon = true;
            }
            UpdateGameInDB(iD, gameFinished, playerWon);
        }

        internal List<Turn> GetTurnsFromDB()
        {
           //get list of turns in current game from db
            List<Turn> turns = new List<Turn>();
            foreach (var turn in db.Turns)
            {
                if (turn.GameId == this.currentGameId)
                {
                    turns.Add(turn);
                }
            }
            return turns;
        }
    }
}
