using System;

namespace UndefeatedTicTacToe.model
{
	public class Coordinate : IEquatable<Coordinate>
	{
		public int XValue { get; protected set; }
		public int YValue { get; protected set; }

		public Coordinate(int xValue, int yValue)
		{
			XValue = xValue;
			YValue = yValue;
		}

		public bool Equals(Coordinate other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.XValue == XValue && other.YValue == YValue;
		}
	}
}
