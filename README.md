# Connect4 Game

This repository contains a C# implementation of the classic Connect4 game. The game is implemented as a Windows Forms application, allowing users to play against each other locally or against a server-side opponent.

## How to Play

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio.
3. Build the project to ensure all dependencies are resolved.

## Dependencies

This project has the following dependencies:

- Newtonsoft.Json: A popular JSON library used for serialization and deserialization of objects. Make sure it's installed to avoid any build errors.

## Game Overview

The `Connect4Game` class is the core of the game. Here's an overview of its functionalities:

### Properties

- `ID`: A unique identifier for each game session.
- `Rows`: The number of rows on the game board.
- `Columns`: The number of columns on the game board.
- `IsLocalPlayerTurn`: A boolean indicating if it's the local player's turn.
- `Board`: A two-dimensional integer array representing the game board.
- `currentTurn`: An integer keeping track of the current turn.

### Methods

- `GenerateRandomId()`: Generates a random GUID to assign a unique ID to the game session.
- `GetID()`: Returns the ID of the game session.
- `InitBoard()`: Initializes the game board with empty slots (zeros).
- `CheckValidSlot(int column)`: Checks if a move is valid in the given column and updates the game board accordingly.
- `Turn()`: Handles the turn of the players, either local or server-side opponent.
- `Apply(int col)`: Applies the player's move to the game board and checks for a victory condition.
- `CheckVictory()`: Checks for a victory condition (four consecutive pieces in a row, column, or diagonal).
- `getFreeCols()`: Returns a list of available columns to play a move.
- `BoardToJson()`: Serializes the game board as a JSON string.

### GameForm

The game is presented through the `GameForm` class, which handles the GUI interactions and provides a user-friendly interface to play Connect4.

## Starting a New Game

To start a new game, follow these steps:

1. Call the `StartGame()` method of the `Connect4Game` class to initialize the game.
2. The game form will display the player name and available columns for the first turn.
3. If there are any previous archived games, the user will be prompted to load one.
4. If no archived game is loaded, the player can start a new game by clicking on an available column.

## Loading from Archive

The `LoadGameFromArchive()` method allows the player to load a previously archived game. It retrieves a list of previous turns from the database and applies them to the game board.

## Server Interaction

The game interacts with a server-side opponent to play turns when it's not the local player's turn. The communication with the server happens through the `ServerSide` class, which is used to get the next turn from the server. The server response is deserialized to an integer representing the column where the server opponent will play its turn.

## Miscellaneous

- The `LocalPlayer` class manages local player-related functionality, including handling turns, adding games to the database, and loading archived games.

## Note

Please ensure that the required dependencies are installed and that the server-side opponent is running correctly for smooth gameplay.

Have fun playing Connect4!
