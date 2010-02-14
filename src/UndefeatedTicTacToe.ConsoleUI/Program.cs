using System;
using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.ConsoleUI
{
	public class Program
	{
		public static ConsoleView View { get; set; }

		static void Main()
		{
			View = new ConsoleView();
		}
	}

	public class ConsoleView : IConsoleView
	{
		public static ConsolePresenter Presenter
		{
			get;
			set;
		}

		public ConsoleView()
		{
			InitializeGame();
		}

		void InitializeGame()
		{
			Presenter = new ConsolePresenter(this);

			Console.WriteLine("Please provide first player's name \"me\" or \"bot\":");

			while (!ValidFirstPlayer)
			{
				string firstPlayer = Console.ReadLine();

				Presenter.Initialize(firstPlayer);
			}
		}

		public void ShowSpecifyUserMessage()
		{
			Console.WriteLine("You must specify user who goes first \"me\" or \"bot\":");
		}

		public bool ValidFirstPlayer
		{
			get; set;
		}

		public string PlayersMove
		{
			get; set;
		}

		public void RetrievePlayersMove()
		{
			Console.WriteLine("Please specify your move in \"x,y\" format:");
			PlayersMove = Console.ReadLine();
		}

		public void ShowBoard(IPlayer[,] board)
		{
			string boardSeparator = "   -----------";
			string boardView = string.Format
			(
				"{10} 2  {0} | {1} | {2} {10}{9}{10} 1  {3} | {4} | {5} {10}{9}{10} 0  {6} | {7} | {8} {10}{10}    0   1   2 {10}",
				GetPlayerRepresentation(board[0,2]),
				GetPlayerRepresentation(board[1,2]),
				GetPlayerRepresentation(board[2,2]),
				GetPlayerRepresentation(board[0,1]),
				GetPlayerRepresentation(board[1,1]),
				GetPlayerRepresentation(board[2,1]),
				GetPlayerRepresentation(board[0,0]),
				GetPlayerRepresentation(board[1,0]),
				GetPlayerRepresentation(board[2,0]),
				boardSeparator,
				Environment.NewLine
			);

			Console.Write(boardView);
		}

		public void ShowGameOverMessage(IGame game)
		{
			Console.WriteLine(string.Format("Game Over {0} won!", GetPlayerName(game.Winner)));
		}

		static string GetPlayerName(IPlayer player)
		{
			if (player is Bot)
				return "Bot";

			return "You";
		}

		public void ShowDrawMessage(IGame game)
		{
			Console.WriteLine("Game Over, it was a draw.");
		}

		public void ShowPlayAgainMessage()
		{
			Console.WriteLine("Type \"yes\" if you would like to play again.");
			PlayAgain = Console.ReadLine();
		}

		public string PlayAgain
		{
			get; set;
		}

		public void StartNewGame()
		{
			InitializeGame();
		}

		public void EndGame()
		{
			Environment.Exit(0);
		}

		static string GetPlayerRepresentation(IPlayer player)
		{
			if (player is Bot)
				return "X";

			if(player is Player)
				return "O";

			return " ";
		}
	}
}
