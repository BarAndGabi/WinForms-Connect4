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
          db.Games.InsertOnSubmit(new Game
          {
                Id = game.GetID(),
                PlayerId = id,
                GameFinished = false,
                PlayerWon = false
            });
            db.SubmitChanges();
            
       

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
