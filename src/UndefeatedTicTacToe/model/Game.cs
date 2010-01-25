using System;

namespace UndefeatedTicTacToe.model
{
	public class Game
	{
		protected Player SomePlayer { get; set; }
		protected Player SomeOtherPlayer { get; set; }
		public virtual Player[,] Board { get; protected set; }
		public virtual bool Over{ get; set; }
		protected Player NextPlayer { get; set; }
		public Player Winner { get; protected set; }
		public Player Loser { get; protected set; }

		public Game(Player somePlayer, Player someOtherPlayer, Player firstPlayer)
		{
			Board = new Player[3, 3];
			SomePlayer = somePlayer;
			SomeOtherPlayer = someOtherPlayer;
			NextPlayer = firstPlayer;

			NextPlayer.MakeMove(this);
		}

		public void EndTurn()
		{
			NextPlayer.MakeMove(this);
		}

		public virtual bool PlayMove(int xCoordinate, int yCoordinate, Player currentPlayer)
		{
			if (CoordinatesAreNotOnBoard(xCoordinate, yCoordinate))
				return false;

			var position = Board[xCoordinate, yCoordinate];

			if(position == null)
			{
                Board[xCoordinate, yCoordinate] = currentPlayer;

				DetermineIfMoveCausedWin(xCoordinate, yCoordinate, currentPlayer);

				SetNextPlayer(currentPlayer);

				return true;
			}

			return false;
		}

		void SetNextPlayer(Player currentPlayer)
		{
			NextPlayer = GetOpponent(currentPlayer);
		}

		void DetermineIfMoveCausedWin(int xCoordinate, int yCoordinate, Player currentPlayer)
		{
			LookForHorizontalAndVerticalThreeInARow(currentPlayer, xCoordinate, yCoordinate);

			LookForDiagonalThreeInARow(currentPlayer);
		}

		void LookForDiagonalThreeInARow(Player currentPlayer)
		{
			bool threeInARowExists = false;

			//check bottom left to top right
			if(Board[0,0] == currentPlayer && Board[1,1] == currentPlayer && Board[2,2] == currentPlayer)
				threeInARowExists = true;

			//check bottom right to top left
			if(Board[2,0] == currentPlayer && Board[1,1] == currentPlayer && Board[0,2] == currentPlayer)
				threeInARowExists = true;

			if(threeInARowExists)
				GameOver(currentPlayer);
		}

		void LookForHorizontalAndVerticalThreeInARow(Player currentPlayer, int xCoordinate, int yCoordinate)
		{
			int xcount = 0;
			int ycount = 0;

			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if(Board[i,j] == currentPlayer)
					{
						if(i == xCoordinate)
							xcount++;
						if(j == yCoordinate)
							ycount++;
					}
				}
			}

			if(xcount == 3 || ycount == 3)
			{
				GameOver(currentPlayer);
			}
		}

		void GameOver(Player currentPlayer)
		{
			Winner = currentPlayer;
			Loser = GetOpponent(currentPlayer);
			Over = true;
		}

		Player GetOpponent(Player currentPlayer)
		{
			return currentPlayer == SomePlayer ? SomeOtherPlayer : SomePlayer;
		}

		static bool CoordinatesAreNotOnBoard(int xCoordinate, int yCoordinate)
		{
			if(xCoordinate < 0 || yCoordinate < 0)
				return true;

			if(xCoordinate > 2 || yCoordinate > 2)
				return true;

			return false;
		}
	}
}