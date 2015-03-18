using System;
using System.Collections.Generic;

namespace GXPEngine
{
	public class NodeWorld : GameObject
	{
		private List<Node> _nodeList;
		private List<Connection> _connections;

		public NodeWorld ()
		{
			_nodeList = new List<Node> ();
			_connections = new List<Connection> ();
		}

		public void AddNode(Node node)
		{
			_nodeList.Add (node);
			AddChild (node);
		}

		public void AddConnection(Node node1, Node node2)
		{
			if (!node1.CheckConnections () && !node2.CheckConnections ()) {

				Connection connection = new Connection (node1, node2);

				_connections.Add (connection);

				node1.AddConnection (connection);
				node2.AddConnection (connection);

				AddChild (connection);
			} else {
				Console.WriteLine ("no connections left on the nodes");
			}
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
