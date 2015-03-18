using System;

namespace GXPEngine
{
	public class Connection : LineSegment
	{
		new Node start {
			get;
			set;
		}
		new Node end {
			get;
			set;
		}

		public Connection (Node pstart, Node pend) : base(pstart.Position,pend.Position)
		{
			start = pstart;
			end = pend;
		}
	}
}

