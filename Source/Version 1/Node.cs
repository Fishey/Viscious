using System;
using System.Collections.Generic;
using System.Drawing;

namespace GXPEngine
{
	public class Node : Canvas
	{
		private List<Node> _connections;
		private Vec2 _position;
		private Color _color;
		private int _radius;

		public Node (int pRadius, Color? pColor = null, Vec2 pPosition = null):base (pRadius*2, pRadius*2)
		{
			_connections = new List<Node> ();
			_radius = pRadius;
			SetOrigin (pRadius, pRadius);
			_position = pPosition ?? Vec2.zero;
			_color = pColor ?? Color.Blue;
			draw ();
			step ();
		}

		private void draw() {
			graphics.Clear (Color.Empty);
			graphics.FillEllipse (
				new SolidBrush (_color),
				0, 0, 2 * _radius, 2 * _radius
			);
		}

		public List<Node> GetPath(List<Node> checkedNodes, Node target)
		{
			checkedNodes.Add (this);

			if (this == target) {
				Console.WriteLine ("Found it!");
				foreach (Node node in checkedNodes) {
					Console.WriteLine (node.Position);
				}
				return checkedNodes;
			} else {
				foreach (Node node in _connections) {
					if (checkedNodes.Contains(node))
					{
						Console.WriteLine ("OK! It was not {0}. We'll check {1}!", this.Position, node.Position);
						return node.GetPath(checkedNodes, target);
					}
				}
				return null;
			}
		}

		private void step()
		{
			this.x = Position.x;
			this.y = Position.y;
		}

		public Vec2 Position
		{
			get { return this._position; }
			set { this._position = value; }
		}

		public void AddConnection(Node node)
		{
			_connections.Add (node);
		}

		public int ConnectionCount
		{
			get { return this._connections.Count; }
		}

		public Node GetConnectionAt(int index)
		{
			try{
			return _connections [index];
			}
			catch (ArgumentOutOfRangeException ex) {
				Console.WriteLine ("Node was not found at that index ({0})", index);
				return null;
			}
		}

		public int Radius
		{
			get { return this._radius; }
		}

	}
}

