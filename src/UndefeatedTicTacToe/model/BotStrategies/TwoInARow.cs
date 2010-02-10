using System;
using System.Collections.Generic;
using System.Linq;

namespace UndefeatedTicTacToe.model.BotStrategies
{
	public class TwoInARow
	{
		public static bool Exist
		(
			IEnumerable<Coordinate> movesMade,
			IEnumerable<Coordinate> possibleNextMoves,
			out Coordinate coordinate
		)
		{
			for (int i = 0; i < 3; i++)
			{
				//check horizontal moves
				int yValue = i;
				if (VerticalOrHorizontalThreeInARowIsPossible
					(move=>move.YValue == yValue,
					movesMade, 
					possibleNextMoves,
					out coordinate))
					return true;

				//check vertical moves
				int xValue = i;
				if (VerticalOrHorizontalThreeInARowIsPossible
					(move => move.XValue == xValue,
					movesMade,
					possibleNextMoves, 
					out coordinate))
					return true;
			}

			Dictionary<Coordinate, bool> bottomRightToTopLeftChecks = new Dictionary<Coordinate, bool>
			{
				{new Coordinate(0, 0), false},
				{new Coordinate(1, 1), false},
				{new Coordinate(2, 2), false}
			};

			if (CanWinDiagonally
				(bottomRightToTopLeftChecks, 
				movesMade, 
				possibleNextMoves, 
				out coordinate))
				return true;

			Dictionary<Coordinate, bool> bottomLeftToTopRightChecks = new Dictionary<Coordinate, bool>
			{
				{new Coordinate(2, 0), false},
				{new Coordinate(1, 1), false},
				{new Coordinate(0, 2), false}
			};

			if (CanWinDiagonally
				(bottomLeftToTopRightChecks, 
				movesMade, 
				possibleNextMoves, 
				out coordinate))
				return true;

			coordinate = null;
			return false;
		}

		static bool VerticalOrHorizontalThreeInARowIsPossible
		(
			Func<Coordinate, bool> coordinateCheck, 
			IEnumerable<Coordinate> movesMade, 
			IEnumerable<Coordinate> possibleNextMoves, 
			out Coordinate coordinate
		)
		{
			if (movesMade.Where(coordinateCheck).Count() == 2)
			{
				var results = possibleNextMoves.Where(coordinateCheck);

				if (results.Any())
				{
					var nextMove = results.First();
					coordinate = new Coordinate(nextMove.XValue, nextMove.YValue);
					return true;
				}
			}

			coordinate = null;
			return false;
		}

		static bool CanWinDiagonally
		(
			Dictionary<Coordinate, bool> checks, 
			IEnumerable<Coordinate> movesMade, 
			IEnumerable<Coordinate> possibleNextMoves,
			out Coordinate coordinateToWin
		)
		{
			var coordinates = checks.Keys.ToArray();

			foreach (Coordinate coord in coordinates)
			{
				Coordinate currentCoordinate = coord;

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
						coordinateToWin = new Coordinate(coordinateToPlay.XValue, coordinateToPlay.YValue);
						return true;
					}
				}
			}

			coordinateToWin = null;
			return false;
		}

	}
}