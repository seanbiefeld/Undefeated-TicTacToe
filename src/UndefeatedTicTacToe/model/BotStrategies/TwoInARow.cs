using System.Collections.Generic;
using System.Linq;

namespace UndefeatedTicTacToe.model.BotStrategies
{
	public class TwoInARow
	{
		public static bool Exist(IEnumerable<Coordinate> movesMade, IEnumerable<Coordinate> possibleNextMoves, out int? xCoordinate, out int? yCoordinate)
		{
			for (int i = 0; i < 3; i++)
			{
				//check horizontal moves
				int yValue = i;
				if (movesMade.Where(move => move.YValue == yValue).Count() == 2)
				{
					var nextMove = possibleNextMoves.Where(move => move.YValue == yValue).First();
					xCoordinate = nextMove.XValue;
					yCoordinate = nextMove.YValue;
					return true;
				}

				//check vertical moves
				int xValue = i;
				if (movesMade.Where(move => move.XValue == xValue).Count() == 2)
				{
					var nextMove = possibleNextMoves.Where(move => move.XValue == xValue).First();
					xCoordinate = nextMove.XValue;
					yCoordinate = nextMove.YValue;
					return true;
				}
			}

			Dictionary<Coordinate, bool> bottomRightToTopLeftChecks = new Dictionary<Coordinate, bool>
			{
				{new Coordinate(0, 0), false},
				{new Coordinate(1, 1), false},
				{new Coordinate(2, 2), false}
			};

			if (CanWinDiagonally(bottomRightToTopLeftChecks, movesMade, possibleNextMoves, out xCoordinate, out yCoordinate))
				return true;

			Dictionary<Coordinate, bool> bottomLeftToTopRightChecks = new Dictionary<Coordinate, bool>
			{
				{new Coordinate(2, 0), false},
				{new Coordinate(1, 1), false},
				{new Coordinate(0, 2), false}
			};

			if (CanWinDiagonally(bottomLeftToTopRightChecks, movesMade, possibleNextMoves, out xCoordinate, out yCoordinate))
				return true;

			xCoordinate = null;
			yCoordinate = null;
			return false;
		}

		static bool CanWinDiagonally(Dictionary<Coordinate, bool> checks, IEnumerable<Coordinate> movesMade, IEnumerable<Coordinate> possibleNextMoves, out int? xCoordinate, out int? yCoordinate)
		{
			var coordinates = checks.Keys.ToArray();

			foreach (Coordinate coordinate in coordinates)
			{
				Coordinate currentCoordinate = coordinate;

				if (movesMade.Where(move => move.XValue == currentCoordinate.XValue
				&& move.YValue == currentCoordinate.YValue).Any())
					checks[currentCoordinate] = true;
			}

			if (checks.Where(check => check.Value == false).Count() == 1)
			{
				var moveToplay = checks.Where(check => check.Value == false).First();

				if (moveToplay.Key != null)
				{
					Coordinate coordinateToPlay = moveToplay.Key;

					if (possibleNextMoves.Contains(coordinateToPlay))
					{
						xCoordinate = coordinateToPlay.XValue;
						yCoordinate = coordinateToPlay.YValue;
						return true;
					}
				}
			}

			xCoordinate = null;
			yCoordinate = null;
			return false;
		}

	}
}