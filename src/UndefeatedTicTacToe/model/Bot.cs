using System;
using System.Collections.Generic;
using System.Linq;
using UndefeatedTicTacToe.model.BotStrategies;

namespace UndefeatedTicTacToe.model
{
	public class Bot : IPlayer
	{
		protected int MovesPlayed { get; set; }

		public Bot()
		{
			MovesPlayed = 0;
		}

		public void MakeMove(IGame game)
		{
			IEnumerable<Coordinate> opponentMoves = GetOpponentsMoves(game);
			IEnumerable<Coordinate> myMovesMade = GetMyMovesMade(game);
			IEnumerable<Coordinate> possibleNextMoves = GetNextPossibleMoves(game);
			IEnumerable<Coordinate> allMovesMade = myMovesMade.Concat(opponentMoves);
			
			if(MovesPlayed == 0)
			{
				if (!opponentMoves.Any())
					PlayDefaultCorner(game);

				if (OpponentPlayedCorner(opponentMoves))
					PlayCenter(game);

				if(OpponentPlayedCenter(opponentMoves))
					PlayDefaultCorner(game);

				if(OpponentPlayedEdge(opponentMoves))
					PlayCenter(game);
			}
			else
				PlayBestMove(myMovesMade, opponentMoves, possibleNextMoves,allMovesMade, game);
		}

		void PlayBestMove(IEnumerable<Coordinate> myMovesMade,
			IEnumerable<Coordinate> opponentMovesMade,
			IEnumerable<Coordinate> possibleNextMoves,
			IEnumerable<Coordinate> allMovesMade,
			IGame game)
		{
			Coordinate coordinate;

			if (TwoInARow.Exist(myMovesMade, possibleNextMoves, out coordinate))
				Play(coordinate.XValue, coordinate.YValue, game);
			else if (TwoInARow.Exist(opponentMovesMade, possibleNextMoves, out coordinate))
				Play(coordinate.XValue, coordinate.YValue, game);
			else if (Fork.ExistsForOpponent(opponentMovesMade, possibleNextMoves, out coordinate))
				Play(coordinate.XValue, coordinate.YValue, game);
			else if (Fork.Exists(allMovesMade, possibleNextMoves,game, this, out coordinate))
				Play(coordinate.XValue, coordinate.YValue, game);
			else
				PlayOpenCornerOrSide(possibleNextMoves, game);
		}

		void PlayOpenCornerOrSide(IEnumerable<Coordinate> possibleNextMoves, IGame game)
		{
			IEnumerable<Coordinate> corners = possibleNextMoves.Where(move => (move.XValue % 2 == 0) && (move.YValue % 2 == 0));

			if (corners.Any())
			{
				var corner = corners.First();
				Play(corner.XValue, corner.YValue, game);
			}

			IEnumerable<Coordinate> sides = possibleNextMoves.Where(move => (move.XValue % 2 == 1) || (move.YValue % 2 == 1));

			if (sides.Any())
			{
				var side = sides.First();
				Play(side.XValue, side.YValue, game);
			}
		}

		static bool OpponentPlayedEdge(IEnumerable<Coordinate> opponentMoves)
		{
			return !(OpponentPlayedCenter(opponentMoves) && OpponentPlayedCorner(opponentMoves));
		}

		static bool OpponentPlayedCenter(IEnumerable<Coordinate> opponentMoves)
		{
			return opponentMoves.Where(move => move.XValue == 1 && move.YValue == 1).Any();
		}

		static bool OpponentPlayedCorner(IEnumerable<Coordinate> opponentMoves)
		{
			foreach (Coordinate opponentMove in opponentMoves)
			{
				if(opponentMove.YValue == 0 || opponentMove.YValue == 2)
				{
					if(opponentMove.XValue == 0 || opponentMove.XValue == 2)
						return true;
				}
			}

			return false;
		}

		static IEnumerable<Coordinate> GetNextPossibleMoves(IGame game)
		{
			return GetMoves(game, currentPlayer => currentPlayer == null);
		}

		IEnumerable<Coordinate> GetMyMovesMade(IGame game)
		{
			return GetMoves(game, currentPlayer => (currentPlayer != null && currentPlayer == this));
		}

		IEnumerable<Coordinate> GetOpponentsMoves(IGame game)
		{
			return GetMoves(game, currentPlayer => (currentPlayer != null && currentPlayer != this));
		}

		static IEnumerable<Coordinate> GetMoves(IGame game, Predicate<IPlayer> logicToDetermineIfMoveIsAdded)
		{
			IList<Coordinate> moves = new List<Coordinate>();

			for (int i = 0; i < game.BoardWidth; i++)
			{
				for (int j = 0; j < game.BoardLength; j++)
				{
					IPlayer playerInCurrentCell = game.Board[i, j];

					if(logicToDetermineIfMoveIsAdded(playerInCurrentCell))
						moves.Add(new Coordinate(i,j));
				}
			}

			return moves;
		}

		void PlayDefaultCorner(IGame game)
		{
			MovesPlayed++;
			Play(0,2, game);
		}

		void PlayCenter(IGame game)
		{
			MovesPlayed++;
			Play(1,1,game);
		}

		void Play(int xcoordinate, int ycoordinate, IGame game)
		{
			if (game.PlayMove(xcoordinate, ycoordinate, this))
			{
				MovesPlayed++;
				game.EndTurn();
			}
		}
	}
}