using System.Collections.Generic;

namespace UndefeatedTicTacToe.model.BotStrategies
{
	public class Fork
	{
		public static bool Exists(IEnumerable<Coordinate> possibleNextMoves, out Coordinate coordinate)
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
					if(corner.XValue == side.XValue || corner.YValue == side.YValue)
					{
						coordinate = corner;
						return true;
					}
				}
			}

			return false;
		}
	}
}
