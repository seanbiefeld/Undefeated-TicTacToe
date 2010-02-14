using System;

namespace UndefeatedTicTacToe.model
{
	public interface IGame
	{
		IPlayer[,] Board { get; }
		bool Over { get; }
		IPlayer Winner { get; }
		IPlayer Loser { get; }
		bool Draw { get; }
		int BoardWidth { get; }
		int BoardLength { get; }
		void EndTurn();
		bool PlayMove(int xCoordinate, int yCoordinate, IPlayer currentIPlayer);
		event Action<IGame> GameOverEvent;
		bool MoveIsValid(int xCoordinate, int yCoordinate);
	}
}