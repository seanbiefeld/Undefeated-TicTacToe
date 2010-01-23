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
			NextPlayer = currentPlayer == SomePlayer ? SomeOtherPlayer : SomePlayer;
		}

		void DetermineIfMoveCausedWin(int xCoordinate, int yCoordinate, Player currentPlayer)
		{
			//horizontal check y remains constant

			//vertical check x remains constant
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