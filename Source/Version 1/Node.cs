using System;
using System.Collections.Generic;
using System.Drawing;

namespace GXPEngine
{
	public class Node : Canvas
	{
		private List<Connection> _connections;
		private Vec2 _position;
		private Color _color;
		private int _radius;
		private int reqConnections;

		public Node (int preqConnections, int pRadius, Color? pColor = null, Vec2 pPosition = null):base (pRadius*2, pRadius*2)
		{
			reqConnections = preqConnections;
			name = reqConnections.ToString ();
			_connections = new List<Connection> ();
			_radius = pRadius;
			SetOrigin (pRadius, pRadius);
			_position = pPosition ?? Vec2.zero;
			_color = pColor ?? Color.Blue;
			draw ();
			step ();
		}

		public bool CheckConnections(){
			if (_connections.Count == reqConnections) {
				this._color = Color.Red;
				draw ();
				return true;
			} else {
				return false;
			}
		}

		private void draw() {
			graphics.Clear (Color.Empty);
			graphics.FillEllipse (
				new SolidBrush (_color),
				0, 0, 2 * _radius, 2 * _radius
			);
			graphics.DrawString (name, new Font ("Arial", 10), new SolidBrush (Color.White), Radius/2,Radius/2);
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

		public void AddConnection(Connection connection)
		{
			connection.x += _connections.Count * 5;
			connection.y -= _connections.Count * 5;
			_connections.Add (connection);
			CheckConnections ();
		}

		public int ConnectionCount
		{
			get { return this._connections.Count; }
		}


		public int Radius
		{
			get { return this._radius; }
		}

	}
}

