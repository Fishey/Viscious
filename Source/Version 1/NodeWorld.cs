using System;
using System.Collections.Generic;

namespace GXPEngine
{
	public class NodeWorld : GameObject
	{
		private List<Node> _nodeList;
		private List<AbstractAgent> _agentList;

		public NodeWorld ()
		{
			_nodeList = new List<Node> ();
			_agentList = new List<AbstractAgent> ();
		}

		public void AddNode(Node node)
		{
			_nodeList.Add (node);
			AddChild (node);
		}

		public void AddConnection(Node node1, Node node2)
		{
			node1.AddConnection (node2);
			node2.AddConnection (node1);
			LineSegment connection = new LineSegment (node1.Position.Clone(), node2.Position.Clone());
			AddChild (connection);
		}

		public void AddAgent(AbstractAgent agent)
		{
			_agentList.Add (agent);
			AddChild (agent);
		}

		public int NodeCount
		{
			get { return this._nodeList.Count; }
		}

		public Node GetNodeAt(int index)
		{
			try{
			return _nodeList [index];
			}
			catch (ArgumentOutOfRangeException ex) {
					Console.WriteLine ("Node was not found at that index ({0})", index);
					return null;
			}
		}

		public List<Node> NodeList
		{
			get { return this._nodeList; }
		}
	}
}
