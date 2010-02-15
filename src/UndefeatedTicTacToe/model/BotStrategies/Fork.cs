using System.Collections.Generic;
using System.Linq;

namespace UndefeatedTicTacToe.model.BotStrategies
{
	public class Fork
	{
		public static bool ExistsForOpponent(IEnumerable<Coordinate> opponentMoves, IEnumerable<Coordinate> botMoves,
			IEnumerable<Coordinate> possibleNextMoves, out Coordinate coordinate)
		{
			coordinate = null;

			Coordinate greatestOpponentCoord = opponentMoves.OrderByDescending(move => move.XValue).First();

			IEnumerable<Coordinate> movesToMake = possibleNextMoves.Where(move => move.XValue >= greatestOpponentCoord.XValue);
			IEnumerable<Coordinate> sides = movesToMake.Where(move => (move.XValue % 2 == 1) || (move.YValue % 2 == 1)).OrderByDescending(move=>move.XValue);
			IEnumerable<Coordinate> corners = movesToMake.Where(move => (move.XValue % 2 == 0) && (move.YValue % 2 == 0)).OrderByDescending(move => move.XValue);

			if (opponentMoves.Where(move => move.XValue == 1 && move.YValue == 1).Any())
			{
				if (corners.Any())
				{
					coordinate = corners.First();
					return true;
				}
			}
			else
			{
				if (sides.Any())
				{
					coordinate = sides.First();
					return true;
				}
			}

			if(movesToMake.Any())
			{
				coordinate =
					possibleNextMoves
						.Where(move => (move.XValue%2 == 0) || (move.YValue%2 == 0))
						.OrderBy(move => move.XValue)
						.First();
				return true;
			}

			return false;
		}
	}
}
