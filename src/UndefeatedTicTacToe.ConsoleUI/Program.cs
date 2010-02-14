using System;

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
			Presenter = new ConsolePresenter(this);

			Console.WriteLine("Please provide first player's name");

			while (!ValidFirstPlayer)
			{
				string firstPlayer = Console.ReadLine();

				Presenter.Initialize(firstPlayer);
			}
		}

		public void ShowSpecifyUserMessage()
		{
			Console.WriteLine("You must specify user who goes first \"me\" or \"bot\"");
		}

		public bool GameOver
		{
			get; set;
		}

		public bool ValidFirstPlayer
		{
			get; set;
		}
	}
}
