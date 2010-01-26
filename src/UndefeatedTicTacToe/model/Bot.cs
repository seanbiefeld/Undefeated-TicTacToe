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

			if(MovesPlayed == 0)
			{
				if (opponentMoves.Count == 0)
					PlayDefaultCorner(game);

				if (OpponentPlayedCorner(opponentMoves))
					PlayCenter(game);

				if(OpponentPlayedCenter(opponentMoves))
					PlayDefaultCorner(game);
			}
			MovesPlayed++;
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

		Dictionary<int,int> GetOpponentsMoves(Game game)
		{
			Dictionary<int,int> opponentsMoves = new Dictionary<int, int>();

			for (int i = 0; i < Game.BoardWidth; i++)
			{
				for (int j = 0; j < Game.BoardLength; j++)
				{
					IPlayer playerInCurrentCell = game.Board[i, j];

					if(playerInCurrentCell != null && playerInCurrentCell != this)
						opponentsMoves.Add(i,j);
				}
			}

			return opponentsMoves;
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