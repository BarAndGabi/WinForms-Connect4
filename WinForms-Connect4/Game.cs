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
			this.Counter = 0;
			this.Rows = 6;
			this.Columns = 7;
			this.Board = new int[Rows, Columns];
			this.IsLocalPlayerTurn = true;
			this.initBoard();
			this.gameForm = new GameForm();
			this.gameForm.SetGame(this);
			this.localPlayer = new LocalPlayer();
			this.serverSide = new ServerSide();
		}

		private void initBoard()
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

		public int Count()
		{
			return ++this.Counter;
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

		public void Turn()
		{
			this.IsLocalPlayerTurn = !this.IsLocalPlayerTurn;
			if (this.IsLocalPlayerTurn)
			{
				this.gameForm.GameButtonsTurnOn();
				//"wait" for apply btn

			}
			else
			{
				this.gameForm.GameButtonsTurnOff();
				//get turn result from server
				//retry until valid slot
			}
		}
		internal void apply(int col)
		{
			int row = -1;//the row of available slot in the given column
			int win = -1;//the player who won or tie
			row = this.CheckValidSlot(col);
			win = this.CheckVictory();
			if (win == 0)
			{
				this.gameForm.UpdateBoard(row, col);
				//if there is no space on board or there is a winner go to next turn
				Turn();
			}
			else //handle winner
			{
				//alert box with winner
				this.gameForm.UpdateBoard(row, col);
				this.gameForm.DisplayVictoryMessageFromGame(win);
				//exit or new game
			}
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

		}
	}
}
