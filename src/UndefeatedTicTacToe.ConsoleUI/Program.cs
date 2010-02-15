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
}
