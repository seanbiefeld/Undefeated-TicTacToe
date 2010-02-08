using System;
using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.ConsoleUI
{
	class Program
	{
		static void Main()
		{
			Player player = new Player();
			Bot bot = new Bot();

			Game game = new Game(player, bot, player);
			
			while (!game.Over)
			{
				Console.Write(BoadBuilder.Build(game.Board));
			}
		}
	}

	internal static class BoadBuilder
	{
		public static string Build(IPlayer[,] board)
		{
			return string.Empty;
		}
	}
}
