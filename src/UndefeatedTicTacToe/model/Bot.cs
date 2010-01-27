using System;
using System.Collections.Generic;
using System.Linq;

namespace UndefeatedTicTacToe.model
{
	public class Bot : IPlayer
	{
		protected int MovesPlayed { get; set; }

		public Bot()
		{
			MovesPlayed = 0;
		}

		public void MakeMove(Game game)
		{
			IEnumerable<Coordinate> opponentMoves = GetOpponentsMoves(game);
			IEnumerable<Coordinate> movesMade = GetMovesMade(game);
			IEnumerable<Coordinate> possibleNextMoves = GetNextPossibleMoves(game);

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
				PlayBestMove(movesMade, possibleNextMoves, game);

			MovesPlayed++;
		}

		void PlayBestMove(IEnumerable<Coordinate> movesMade, IEnumerable<Coordinate> possibleNextMoves, Game game)
		{
			int? nextXValue;
			int? nextYValue;

			if (TwoInARowExist(movesMade, possibleNextMoves, out nextXValue, out nextYValue))
				Play(nextXValue.Value, nextYValue.Value, game);
		}

		bool TwoInARowExist(IEnumerable<Coordinate> movesMade, IEnumerable<Coordinate> possibleNextMoves, out int? xCoordinate, out int? yCoordinate)
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

			xCoordinate = null;
			yCoordinate = null;
			return false;
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

		static IEnumerable<Coordinate> GetNextPossibleMoves(Game game)
		{
			return GetMoves(game, currentPlayer => currentPlayer == null);
		}

		IEnumerable<Coordinate> GetMovesMade(Game game)
		{
			return GetMoves(game, currentPlayer => (currentPlayer != null && currentPlayer == this));
		}

		IEnumerable<Coordinate> GetOpponentsMoves(Game game)
		{
			return GetMoves(game, currentPlayer => (currentPlayer != null && currentPlayer != this));
		}

		static IEnumerable<Coordinate> GetMoves(Game game, Predicate<IPlayer> logicToDetermineIfMoveIsAdded)
		{
			IList<Coordinate> moves = new List<Coordinate>();

			for (int i = 0; i < Game.BoardWidth; i++)
			{
				for (int j = 0; j < Game.BoardLength; j++)
				{
					IPlayer playerInCurrentCell = game.Board[i, j];

					if(logicToDetermineIfMoveIsAdded(playerInCurrentCell))
						moves.Add(new Coordinate(i,j));
				}
			}

			return moves;
		}

		void PlayDefaultCorner(Game game)
		{
			Play(0,2, game);
		}

		void PlayCenter(Game game)
		{
			Play(1,1,game);
		}

		void Play(int xcoordinate, int ycoordinate, Game game)
		{
			game.PlayMove(xcoordinate, ycoordinate, this);
			game.EndTurn();
		}
	}
}