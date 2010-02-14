using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.ConsoleUI
{
	public interface IConsoleView
	{
		void ShowSpecifyUserMessage();
		bool ValidFirstPlayer { get; set; }
		string PlayersMove { get; set; }
		void RetrievePlayersMove();
		void ShowBoard(IPlayer[,] board);
		void ShowGameOverMessage(IGame game);
		void ShowDrawMessage(IGame game);
		void ShowPlayAgainMessage();
		string PlayAgain{ get; set; }
		void StartNewGame();
		void EndGame();
	}
}