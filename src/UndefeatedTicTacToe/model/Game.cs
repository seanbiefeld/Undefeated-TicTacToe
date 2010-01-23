namespace UndefeatedTicTacToe.model
{
	public class Game
	{
		protected Player Opponent { get; set; }
		protected Bot Bot { get; set; }
		public virtual Player[,] Board { get; protected set; }
		public virtual bool Over{ get; set; }
		protected Player NextPlayer { get; set; }

		public Game(Bot bot, Player opponent, Player firstPlayer)
		{
			Board = new Player[3, 3];
			Opponent = opponent;
			Bot = bot;
			NextPlayer = firstPlayer;

			NextPlayer.MakeMove(this);
		}

		public virtual void PlayMove(int xCoordinate, int yCoordinate, Player player)
		{
			Board[xCoordinate, yCoordinate] = player;
		}
	}
}