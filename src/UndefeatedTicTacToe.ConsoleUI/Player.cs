using System;
using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.ConsoleUI
{
	public class Player : IPlayer
	{
        public event Func<IGame, Coordinate> MoveNeeded;

		public void MakeMove(IGame game)
		{
			Coordinate playersMove = null;

			if (MoveNeeded != null)
			{
				playersMove = MoveNeeded(game);
			}

			if (playersMove != null)
			{
				game.PlayMove(playersMove.XValue, playersMove.YValue, this);
				game.EndTurn();
			}
		}
	}
}