using System.Collections.Generic;
using System.Linq;

namespace UndefeatedTicTacToe.model.BotStrategies
{
	public class Fork
	{
		public static bool Exists(IEnumerable<Coordinate> movesMade,
			IEnumerable<Coordinate> possibleNextMoves, IGame game, Bot bot,
			out Coordinate coordinate)
		{
			coordinate = null;

			IList<Coordinate> corners = new List<Coordinate>();
			IList<Coordinate> sides = new List<Coordinate>();

			foreach (Coordinate move in possibleNextMoves)
			{
				//check for corners
				if (((move.XValue % 2) == 0) && ((move.YValue % 2) == 0))
					corners.Add(move);
				else
					sides.Add(move);
			}

			foreach (Coordinate corner in corners)
			{
				foreach (Coordinate side in sides)
				{
					if (corner.XValue == side.XValue || corner.YValue == side.YValue)
					{
						Coordinate currentCorner = corner;
						Coordinate currentSide = side;
						var thirdEmptyQuery =
							possibleNextMoves.Where
								(c => (c.XValue == currentCorner.XValue || c.YValue == currentCorner.YValue)
								&& (c != currentCorner) && (c != currentSide));

						var relatedMovesMade =
							movesMade.Where
								(c => (c.XValue == currentCorner.XValue || c.YValue == currentCorner.YValue)
								&& (c != currentCorner) && (c != currentSide));

						bool relatedMovesMadeAreBots = true;

						foreach (Coordinate move in relatedMovesMade)
						{
							relatedMovesMadeAreBots &= (game.Board[move.XValue, move.YValue] == bot);
						}

						if (thirdEmptyQuery.Any() && relatedMovesMadeAreBots)
						{
							coordinate = corner;
							return true;
						}
					}
				}
			}

			return false;
		}
	}
}
