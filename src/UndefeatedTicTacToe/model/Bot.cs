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
			if (MovesPlayed == 0 && GetOpponentsMoves(game).Count == 0)
				MakeFirstMove(game);
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

		void MakeFirstMove(Game game)
		{
			game.PlayMove(0, 2, this);
		}
	}
}