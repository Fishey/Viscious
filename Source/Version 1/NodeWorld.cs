using System;
using System.Collections.Generic;


namespace GXPEngine
{
	public class NodeWorld : GameObject
	{
		MouseHandler mh;
		private List<Node> _nodeList;
		private List<Connection> _connections;

		Node _selectednode = null;
		LineSegment _selectionline = null;
		TextField _stats = TextField.CreateTextField("STR: 000 - FLX: 000 - RGH: 000 - Error: Node X cannot connect to node Y");

		//stats
		int strength = 0;
		int flexibility = 0;
		int roughness = 0;
		string error = "";

		public NodeWorld ()
		{
			_stats.text = "STR: "+ strength + " - FLX: " + flexibility + " - RGH: " + roughness + error;
			mh = new MouseHandler (this);
			mh.OnMouseDown += DoThisOnMouseDown;
			mh.OnMouseUp += DoThisOnMouseUp;
			mh.OnMouseMove += DoThisOnMouseMove;
			this.AddChild (_stats);
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
			if (node1 == node2) {
				Console.WriteLine ("Cannot connect to same node");
				return;
			}
			if (node1 == null || node2 == null) {
				Console.WriteLine ("could not find 2 valid nodes");
				return;
			}
			if (!node1.CheckConnections () && !node2.CheckConnections ()) {

				Connection connection = new Connection (node1, node2);

				_connections.Add (connection);

				node1.AddConnection (connection);
				node2.AddConnection (connection);

				AddChild (connection);
				error = "";
			} else {
				error = " - Node " + node1.name + " cannot connect to node " + node2.name;

			}
			CountStats ();

		}
			

		public void CountStats(){
			foreach (Connection connection in _connections) {
				//check how much stats each connection gives here
				if (connection.firstNode.name == "1" && connection.secondNode.name == "2") {
					strength++;
				} else if (connection.firstNode.name == "1" && connection.secondNode.name == "3") {
					strength--;
				} else if (connection.firstNode.name == "1" && connection.secondNode.name == "4") {
					flexibility++;
				} else if (connection.firstNode.name == "2" && connection.secondNode.name == "3") {
					flexibility--;
				} else if (connection.firstNode.name == "2" && connection.secondNode.name == "4") {
					roughness++;
				} else if (connection.firstNode.name == "3" && connection.secondNode.name == "4") {
					roughness--;
				}
				_stats.text = "STR: "+ strength + " - FLX: " + flexibility + " - RGH: " + roughness + error;
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
			

		void DoThisOnMouseDown(object sender, MouseEventType e){
			foreach (Node node in _nodeList) {
				if (Math.Abs (Input.mouseX - node.Position.x) < node.Radius && Math.Abs (Input.mouseY - node.Position.y) < node.Radius) {
					_selectednode = node;
					_selectionline = new LineSegment (_selectednode.Position, _selectednode.Position);
					AddChild(_selectionline);
					break;
				}
			}
		}
		void DoThisOnMouseUp(object sender, MouseEventType e){
			if (_selectednode != null) {
				foreach (Node node in _nodeList) {
					if (Math.Abs (Input.mouseX - node.Position.x) < node.Radius && Math.Abs (Input.mouseY - node.Position.y) < node.Radius) {
						AddConnection (_selectednode, node);
						break;
					}
				}
			}
			_selectednode = null;
			if (_selectionline != null) {
				RemoveChild (_selectionline);
				_selectionline = null;
			}
		}
		void DoThisOnMouseMove(object sender, MouseEventType e){
			if (_selectednode != null) {
				_selectionline.end = new Vec2 (Input.mouseX, Input.mouseY);
			}
		}

	}
}
