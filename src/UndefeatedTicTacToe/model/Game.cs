namespace UndefeatedTicTacToe.model
{
	public class Game : IGame
	{
		protected IPlayer SomePlayer { get; set; }
		protected IPlayer SomeOtherPlayer { get; set; }
		public virtual IPlayer[,] Board { get; protected set; }
		public virtual bool Over{ get; protected set; }
		protected IPlayer NextPlayer { get; set; }
		public IPlayer Winner { get; protected set; }
		public IPlayer Loser { get; protected set; }
		public bool Draw { get; protected set; }
		public int BoardWidth { get { return 3; } }
		public int BoardLength { get { return 3; } }
		
		public Game(IPlayer someIPlayer, IPlayer someOtherIPlayer, IPlayer firstIPlayer)
		{
			Board = new IPlayer[BoardWidth, BoardLength];
			SomePlayer = someIPlayer;
			SomeOtherPlayer = someOtherIPlayer;
			NextPlayer = firstIPlayer;

			NextPlayer.MakeMove(this);
		}

		public virtual void EndTurn()
		{
			NextPlayer.MakeMove(this);
		}

		public virtual bool PlayMove(int xCoordinate, int yCoordinate, IPlayer currentIPlayer)
		{
			if (CoordinatesAreNotOnBoard(xCoordinate, yCoordinate))
				return false;

			var position = Board[xCoordinate, yCoordinate];

			if(position == null)
			{
                Board[xCoordinate, yCoordinate] = currentIPlayer;

				DetermineIfMoveCausedWin(xCoordinate, yCoordinate, currentIPlayer);

				DetermineIfMoveCausedDraw();

				SetNextIPlayer(currentIPlayer);

				return true;
			}

			return false;
		}

		void SetNextIPlayer(IPlayer currentIPlayer)
		{
			NextPlayer = GetOpponent(currentIPlayer);
		}

		void DetermineIfMoveCausedWin(int xCoordinate, int yCoordinate, IPlayer currentIPlayer)
		{
			LookForHorizontalAndVerticalThreeInARow(currentIPlayer, xCoordinate, yCoordinate);

			LookForDiagonalThreeInARow(currentIPlayer);
		}

		void LookForDiagonalThreeInARow(IPlayer currentIPlayer)
		{
			bool threeInARowExists = false;

			//check bottom left to top right
			if(Board[0,0] == currentIPlayer && Board[1,1] == currentIPlayer && Board[2,2] == currentIPlayer)
				threeInARowExists = true;

			//check bottom right to top left
			if(Board[2,0] == currentIPlayer && Board[1,1] == currentIPlayer && Board[0,2] == currentIPlayer)
				threeInARowExists = true;

			if(threeInARowExists)
				GameOver(currentIPlayer);
		}

		void LookForHorizontalAndVerticalThreeInARow(IPlayer currentIPlayer, int xCoordinate, int yCoordinate)
		{
			int xcount = 0;
			int ycount = 0;

			for (int i = 0; i < BoardWidth; i++)
			{
				for (int j = 0; j < BoardLength; j++)
				{
					if(Board[i,j] == currentIPlayer)
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
				GameOver(currentIPlayer);
			}
		}

		void DetermineIfMoveCausedDraw()
		{
			bool noEmptyCoordinateExist = true;

			for (int i = 0; i < BoardWidth; i++)
			{
				for (int j = 0; j < BoardLength; j++)
				{
					noEmptyCoordinateExist &= (Board[i, j] != null);
				}
			}

			if(noEmptyCoordinateExist)
			{
				Over = true;
				Draw = true;
			}
		}

		void GameOver(IPlayer currentIPlayer)
		{
			Winner = currentIPlayer;
			Loser = GetOpponent(currentIPlayer);
			Over = true;
		}

		IPlayer GetOpponent(IPlayer currentIPlayer)
		{
			return currentIPlayer == SomePlayer ? SomeOtherPlayer : SomePlayer;
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