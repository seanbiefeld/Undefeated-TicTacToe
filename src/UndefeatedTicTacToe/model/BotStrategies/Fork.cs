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

			IEnumerable<Coordinate> cornerMoves = possibleNextMoves.Where(move => (move.XValue % 2 == 0) && (move.YValue % 2 == 0));
			IEnumerable<Coordinate> opponentSideMoves = opponentMoves.Where(move => (move.XValue%2 == 1) || (move.YValue%2 == 1));

			//try to find corners on same y axis
			foreach (Coordinate opponentSideMove in opponentSideMoves)
			{
				foreach (Coordinate cornerMove in cornerMoves)
				{
					if(cornerMove.YValue == opponentSideMove.YValue)
					{
						coordinate = cornerMove;
						return true;
					}
				}
			}

			//try to find corners on same x axis
			foreach (Coordinate opponentSideMove in opponentSideMoves)
			{
				foreach (Coordinate cornerMove in cornerMoves)
				{
					if (cornerMove.XValue == opponentSideMove.XValue)
					{
						coordinate = cornerMove;
						return true;
					}
				}
			}

			return false;
		}
	}
}
