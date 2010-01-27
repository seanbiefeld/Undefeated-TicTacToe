using System;
using System.Collections.Generic;

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
			Dictionary<int, int> opponentMoves = GetOpponentsMoves(game);
			Dictionary<int, int> movesMade = GetMovesMade(game);
			Dictionary<int, int> possibleNextMoves = GetNextPossibleMoves(game);

			if(MovesPlayed == 0)
			{
				if (opponentMoves.Count == 0)
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

		void PlayBestMove(Dictionary<int, int> movesMade, Dictionary<int, int> possibleNextMoves, Game game)
		{
			int nextXCoordinate;
			int nextYCoordinate;

			//if (TwoInARowExist(movesMade, possibleNextMoves, out nextXCoordinate, out nextYCoordinate))
				//Play(nextXCoordinate, nextYCoordinate, game);
		}

		//bool TwoInARowExist(Dictionary<int, int> movesMade, Dictionary<int, int> possibleNextMoves, out int xCoordinate, out int yCoordinate)
		//{
			
		//}

		static bool OpponentPlayedEdge(Dictionary<int, int> opponentMoves)
		{
			return !(OpponentPlayedCenter(opponentMoves) && OpponentPlayedCorner(opponentMoves));
		}

		static bool OpponentPlayedCenter(Dictionary<int, int> opponentMoves)
		{
			foreach (KeyValuePair<int, int> opponentMove in opponentMoves)
			{
				if(opponentMove.Key == 1 && opponentMove.Value == 1)
					return true;
			}

			return false;
		}

		static bool OpponentPlayedCorner(Dictionary<int, int> opponentMoves)
		{
			foreach (KeyValuePair<int, int> opponentMove in opponentMoves)
			{
				if(opponentMove.Value == 0 || opponentMove.Value == 2)
				{
					if(opponentMove.Key == 0 || opponentMove.Key == 2)
						return true;
				}
			}

			return false;
		}

		Dictionary<int, int> GetNextPossibleMoves(Game game)
		{
			return GetMoves(game, currentPlayer => currentPlayer == null);
		}

		Dictionary<int, int> GetMovesMade(Game game)
		{
			return GetMoves(game, currentPlayer => (currentPlayer != null && currentPlayer == this));
		}

		Dictionary<int,int> GetOpponentsMoves(Game game)
		{
			return GetMoves(game, currentPlayer => (currentPlayer != null && currentPlayer != this));
		}

		Dictionary<int, int> GetMoves(Game game, Predicate<IPlayer> logicToDetermineIfMoveIsAdded)
		{
			Dictionary<int, int> moves = new Dictionary<int, int>();

			for (int i = 0; i < Game.BoardWidth; i++)
			{
				for (int j = 0; j < Game.BoardLength; j++)
				{
					IPlayer playerInCurrentCell = game.Board[i, j];

					if(logicToDetermineIfMoveIsAdded(playerInCurrentCell))
						moves.Add(i,j);
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