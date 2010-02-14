namespace UndefeatedTicTacToe.ConsoleUI
{
	public interface IConsoleView
	{
		void ShowSpecifyUserMessage();
		bool GameOver { get; set; }
		bool ValidFirstPlayer { get; set; }
	}
}