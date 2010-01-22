namespace UndefeatedTicTacToe.model
{
	public class Game
	{
		public virtual Player Opponent { get; set; }
		public virtual Bot Bot { get; set; }
		public virtual Board Board { get; set; }
	}
}