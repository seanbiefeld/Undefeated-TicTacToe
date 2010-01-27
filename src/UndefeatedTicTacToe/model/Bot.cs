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
			int nextXCoordinate;
			int nextYCoordinate;

			//if (TwoInARowExist(movesMade, possibleNextMoves, out nextXCoordinate, out nextYCoordinate))
				//Play(nextXCoordinate, nextYCoordinate, game);
		}

		//bool TwoInARowExist(Dictionary<int, int> movesMade, Dictionary<int, int> possibleNextMoves, out int xCoordinate, out int yCoordinate)
		//{
			
		//}

		static bool OpponentPlayedEdge(IEnumerable<Coordinate> opponentMoves)
		{
			return !(OpponentPlayedCenter(opponentMoves) && OpponentPlayedCorner(opponentMoves));
		}

		static bool OpponentPlayedCenter(IEnumerable<Coordinate> opponentMoves)
		{
			foreach (Coordinate opponentMove in opponentMoves)
			{
				if(opponentMove.XValue == 1 && opponentMove.YValue == 1)
					return true;
			}

			return false;
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