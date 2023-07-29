using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms_Connect4
{
    public class Connect4Game
    {
        private string ID { get; set; }
        public int Rows { get; }
        public int Columns { get; }
        public bool IsLocalPlayerTurn { get; private set; }
        public int[,] Board { get; }
        private GameForm gameForm;
        private ServerSide serverSide;
        private LocalPlayer localPlayer;
        private int currentTurn { get; set;}


        public Connect4Game()
        {
            this.ID = "new game";
            this.Rows = 6;
            this.Columns = 7;
            this.Board = new int[Rows, Columns];
            this.IsLocalPlayerTurn = true;
            this.gameForm = new GameForm();
            this.gameForm.SetGame(this);
            this.serverSide = new ServerSide(this);
            this.InitBoard();
            this.localPlayer = new LocalPlayer();
            this.currentTurn = 0;


        }
        public string GenerateRandomId()
        {
            // Generate a new GUID (Globally Unique Identifier)
            Guid guid = Guid.NewGuid();

            // Convert the GUID to a string and remove any hyphens or curly braces
            string randomId = guid.ToString("N");

            // Return the generated random ID
            return randomId;
        }

        public string GetID()
        {
            return this.ID;

        }
        private void InitBoard()
        {
            // Initialize the board
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    this.Board[i, j] = 0;
                }
            }
        }

        public int CheckValidSlot(int column)
        {
            for (int i = this.Rows - 1; i >= 0; i--)
            {
                if (this.Board[i, column] == 0)
                {
                    this.Board[i, column] = this.IsLocalPlayerTurn ? 1 : 2;
                    return i;
                }
            }

            return -1; // Invalid slot
        }
     

       

        public async  void Turn()
        {
            this.IsLocalPlayerTurn = !this.IsLocalPlayerTurn;

            if (this.IsLocalPlayerTurn)
            {
                this.gameForm.GameButtonsTurnOn();
                // "wait" for apply btn
            }
            else
            {
                this.gameForm.GameButtonsTurnOff();
                HttpResponseMessage response = await this.serverSide.getNextTurn();
                string responseBody = await response.Content.ReadAsStringAsync();
                //convert response body to int
                int col = JsonConvert.DeserializeObject<int>(responseBody);
                this.Apply(col);

                // get turn result from server
                // retry until valid slot
            }
        }

       internal async void testServer()
        {
            HttpResponseMessage response = await this.serverSide.getNextTurn();
            string responseBody = await response.Content.ReadAsStringAsync();
            MessageBox.Show(responseBody);
           

            
            
        }

        internal void Apply(int col)
        {
            int row = -1; // the row of available slot in the given column
            int win = -1; // the player who won or tie
            row = this.CheckValidSlot(col);
            if(row == -1)
            {
                MessageBox.Show("Invalid slot");
                return;
            }
            this.gameForm.UpdateBoard(row, col);
            //add turn to local db
            this.localPlayer.AddTurnToDB(this.ID, this.currentTurn, col,this.IsLocalPlayerTurn);
            ++this.currentTurn;
            win = this.CheckVictory();
            if (win == 0)
            {
                Turn();
            }
            else
            {
                // handle winner to db
                // alert box with winner
                //this.localPlayer.showTurnsTable();
                this.gameForm.DisplayVictoryMessageFromGame(win);
                this.localPlayer.UpdateGameIsFinished(this.ID, win);
                if (this.localPlayer.ChooseGameFromArchive())
                {
                    //load game from archive
                }
                else
                {
                    //exit or new game
                    this.StartGame();
                }
               
            }
        }
        internal void ApplyFromArchive(int col)
        {
            int row = -1; // the row of available slot in the given column
            row = this.CheckValidSlot(col);
            if (row == -1)
            {
                MessageBox.Show("Invalid slot from archive - "+ col.ToString());
                return;
            }
            this.gameForm.UpdateBoard(row, col);
            int win = this.CheckVictory();
            if(win==1 || win == 2)
            {
                this.gameForm.DisplayVictoryMessageFromGame(win);
                this.gameForm.GameButtonsTurnOff();
            }
            this.IsLocalPlayerTurn = !this.IsLocalPlayerTurn;


        }

        private void initNewGame()
        {
            this.gameForm.newGame();
            this.InitBoard();
            this.IsLocalPlayerTurn = true;
            this.gameForm.GameButtonsTurnOn();
            this.ID = GenerateRandomId();


        }


        public List<int> getFreeCols()
        {
           List<int> freeCols = new List<int>();
            for (int i = 0; i < this.Columns; i++)
            {
                for (int j = this.Rows - 1; j >= 0; j--)
                {
                    if (this.Board[j, i] == 0)
                    {
                        freeCols.Add(i);
                        break;
                    }
                }
            }
            return freeCols;
        }
        public int CheckVictory()
        {
            // Check rows
            for (int row = 0; row < this.Rows; row++)
            {
                for (int col = 0; col <= this.Columns - 4; col++)
                {
                    int player = this.Board[row, col];
                    if (player != 0 &&
                        this.Board[row, col + 1] == player &&
                        this.Board[row, col + 2] == player &&
                        this.Board[row, col + 3] == player)
                    {
                        return player;
                    }
                }
            }

            // Check columns
            for (int col = 0; col < this.Columns; col++)
            {
                for (int row = 0; row <= this.Rows - 4; row++)
                {
                    int player = this.Board[row, col];
                    if (player != 0 &&
                        this.Board[row + 1, col] == player &&
                        this.Board[row + 2, col] == player &&
                        this.Board[row + 3, col] == player)
                    {
                        return player;
                    }
                }
            }

            // Check diagonals (top-left to bottom-right)
            for (int row = 0; row <= this.Rows - 4; row++)
            {
                for (int col = 0; col <= this.Columns - 4; col++)
                {
                    int player = this.Board[row, col];
                    if (player != 0 &&
                        this.Board[row + 1, col + 1] == player &&
                        this.Board[row + 2, col + 2] == player &&
                        this.Board[row + 3, col + 3] == player)
                    {
                        return player;
                    }
                }
            }

            // Check diagonals (top-right to bottom-left)
            for (int row = 0; row <= this.Rows - 4; row++)
            {
                for (int col = this.Columns - 1; col >= 3; col--)
                {
                    int player = this.Board[row, col];
                    if (player != 0 &&
                        this.Board[row + 1, col - 1] == player &&
                        this.Board[row + 2, col - 2] == player &&
                        this.Board[row + 3, col - 3] == player)
                    {
                        return player;
                    }
                }
            }

            return 0; // No victory yet
        }

        internal Form GetGameForm()
        {
            return this.gameForm;
        }

        internal void StartGame()
        {
            this.IsLocalPlayerTurn = true;
            this.gameForm.GameButtonsTurnOn();
            this.localPlayer.LogIn(this.serverSide.PlayerLogIn());
            this.gameForm.SetPlayerName(this.localPlayer.id);
            this.initNewGame();
              this.localPlayer.addGameToDB(this);
            if(this.localPlayer.ChooseGameFromArchive())
            {
               this.LoadGameFromArchive(); 
            }
        }

        private void LoadGameFromArchive()
        {
           //get from localplayer list of turns and apply them
            List<Turn> turns = this.localPlayer.GetTurnsFromDB();
            foreach (Turn turn in turns)
            {
                this.ApplyFromArchive(turn.Played);
                if (this.CheckVictory() != 0)
                {
                    this.gameForm.DisplayVictoryMessageFromGame(this.CheckVictory());
                    this.gameForm.GameButtonsTurnOff();
                    break;
                }
            }

        }

        public string BoardToJson()
        {
            // Create a two-dimensional array to represent the board
            int[][] boardArray = new int[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                boardArray[i] = new int[Columns];
                for (int j = 0; j < Columns; j++)
                {
                    boardArray[i][j] = Board[i, j];
                }
            }

            // Serialize the board array to JSON
            string json = JsonConvert.SerializeObject(boardArray);
            return json;
        }

        internal List<int> getAvailableColumns()
        {
            //return list of available columns to play
            List<int> availableCols = new List<int>();
            for (int i = 0; i < this.Columns; i++)
            {
                for (int j = this.Rows - 1; j >= 0; j--)
                {
                    if (this.Board[j, i] == 0)
                    {
                        availableCols.Add(i);
                        break;
                    }
                }
            }
            return availableCols;
        }
    }
}

