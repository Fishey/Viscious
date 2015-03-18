using System;

namespace GXPEngine
{
	public class IntVec2 
	{
		public int x = 0;
		public int y = 0;
		public static IntVec2 zero { get { return new IntVec2 (0, 0); } }


		public IntVec2 (int pX = 0, int pY = 0)
		{
			x = pX;
			y = pY;
		}

		public override string ToString ()
		{
			return String.Format ("({0}, {1})", x, y);
		}

		public static bool operator == (IntVec2 a, IntVec2 b)
		{
			if (System.Object.ReferenceEquals (a, b))
				return true;
			if (((object)a == null || ((object)b == null)))
			{
				return false;
			}

			return a.x == b.x && a.y == b.y;
		}

		public static bool operator != (IntVec2 a, IntVec2 b)
		{
			return !(a == b);
		}

		public Vec2 ToVec2()
		{
			return new Vec2 (this.x, this.y);
		}

		public IntVec2 Add (IntVec2 other) {
			x += other.x;
			y += other.y;
			return this;
		}

		public IntVec2 Sub (IntVec2 other) {
			x -= other.x;
			y -= other.y;
			return this;
		}

		public IntVec2 Clone() {
			return new IntVec2 (x, y);
		}

		public bool Equals (IntVec2 other)
		{
			return other != null && x == other.x && y == other.y;
		}

	}
}

