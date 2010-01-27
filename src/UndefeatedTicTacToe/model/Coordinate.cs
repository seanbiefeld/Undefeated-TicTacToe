namespace UndefeatedTicTacToe.model
{
	public class Coordinate
	{
		public int XValue { get; protected set; }
		public int YValue { get; protected set; }

		public Coordinate(int xValue, int yValue)
		{
			XValue = xValue;
			YValue = yValue;
		}
	}
}
