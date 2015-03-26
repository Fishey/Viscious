using System;

namespace GXPEngine
{
	public class Connection : LineSegment
	{
		public new Node firstNode {
			get;
			set;
		}
		public new Node secondNode {
			get;
			set;
		}

		public Connection (Node pFirstNode, Node pSecondNode) : base(pFirstNode.Position,pSecondNode.Position)
		{
			firstNode = pFirstNode;
			secondNode = pSecondNode;
		}
	}
}

