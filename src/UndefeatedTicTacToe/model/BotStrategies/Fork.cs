using System.Collections.Generic;

namespace UndefeatedTicTacToe.model.BotStrategies
{
	public class Fork
	{
		public static bool Exists(IEnumerable<Coordinate> possibleNextMoves, out int? xValue, out int? yValue)
		{
			bool forkExists = false;

			if (TwoInARow.Exist(possibleNextMoves, possibleNextMoves, out xValue, out yValue))

			xValue = null;
			yValue = null;
			return forkExists;
		}
	}
}
