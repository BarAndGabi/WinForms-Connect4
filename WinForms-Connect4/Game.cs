using System;
using System.Data;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;

namespace WinForms_Connect4
{
    public class Game
    {
        public int Counter { get; private set; }
        public int Rows { get; }
        public int Columns { get; }
        public bool IsLocalPlayerTurn { get; private set; }
        public int[,] Board { get; }
        private GameForm gameForm;
        private LocalPlayer localPlayer;
        private ServerSide serverSide;

        public Game()
        {
            Counter = 0;
            Rows = 6;
            Columns = 7;
            Board = new int[Rows, Columns];
            IsLocalPlayerTurn = false;
            initBoard();
            gameForm = new GameForm();
            gameForm.setGame(this);
            localPlayer = new LocalPlayer();
            serverSide = new ServerSide();
        }

        private void initBoard()
        {
            // Initialize the board
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Board[i, j] = 0;
                }
            }
        }

        public int Count()
        {
            return ++Counter;
        }

        public int CheckValidSlot(int column)
        {
            for (int i = Rows - 1; i >= 0; i--)
            {
                if (Board[i, column] == 0)
                {
                    Board[i, column] = IsLocalPlayerTurn ? 1 : 2;
                    IsLocalPlayerTurn = !IsLocalPlayerTurn;
                    return i;
                }
            }

            return -1; // Invalid slot
        }

        public void Turn()
        {
           if(this.IsLocalPlayerTurn)
            {
                this.gameForm.gameButtonsTurnOn();
                //"wait" for apply btn

            }
            else
            {
                this.gameForm.gameButtonsTurnOff();
                //get turn result from server
                //retry until valid slot
            }
        }

        public int CheckVictory()
        {
            // Check rows
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col <= Columns - 4; col++)
                {
                    int player = Board[row, col];
                    if (player != 0 &&
                        Board[row, col + 1] == player &&
                        Board[row, col + 2] == player &&
                        Board[row, col + 3] == player)
                    {
                        return player;
                    }
                }
            }

            // Check columns
            for (int col = 0; col < Columns; col++)
            {
                for (int row = 0; row <= Rows - 4; row++)
                {
                    int player = Board[row, col];
                    if (player != 0 &&
                        Board[row + 1, col] == player &&
                        Board[row + 2, col] == player &&
                        Board[row + 3, col] == player)
                    {
                        return player;
                    }
                }
            }

            // Check diagonals (top-left to bottom-right)
            for (int row = 0; row <= Rows - 4; row++)
            {
                for (int col = 0; col <= Columns - 4; col++)
                {
                    int player = Board[row, col];
                    if (player != 0 &&
                        Board[row + 1, col + 1] == player &&
                        Board[row + 2, col + 2] == player &&
                        Board[row + 3, col + 3] == player)
                    {
                        return player;
                    }
                }
            }

            // Check diagonals (top-right to bottom-left)
            for (int row = 0; row <= Rows - 4; row++)
            {
                for (int col = Columns - 1; col >= 3; col--)
                {
                    int player = Board[row, col];
                    if (player != 0 &&
                        Board[row + 1, col - 1] == player &&
                        Board[row + 2, col - 2] == player &&
                        Board[row + 3, col - 3] == player)
                    {
                        return player;
                    }
                }
            }

            return 0; // No victory yet
        }

        internal Form getGameForm()
        {
           return this.gameForm;
        }
    }
}
