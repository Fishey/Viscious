using System;
using System.Drawing;
using System.Collections.Generic;

namespace GXPEngine
{
	public class WanderingAgent : AbstractAgent
	{
		private Node _target;
		private Node _lastTarget;
		private NodeWorld _world;
		private Vec2 _velocity;
		private int _speed;
		private List<Node> pathList;

		public WanderingAgent (NodeWorld world = null, int pRadius = 15, Color? pColor = null, int speed = 1) : base(15, Color.Red, 0)
		{
			SetContext (_world);
			_world = world;
			_target = _world.GetNodeAt(Utils.Random (0, _world.NodeCount));
			this.position = _target.Position.Clone();
			_speed = speed;
			this.x = position.x;
			this.y = position.y;
			this.SetOrigin (width / 2, height / 2);
		}

		protected override void think()
		{
			if (Input.GetMouseButtonDown(0) && _target == _lastTarget)
				SetMouseTarget ();
			if (Math.Abs(this.position.x - _target.Position.x) < 5 && Math.Abs(this.position.y - _target.Position.y) < 5) {
				_velocity = Vec2.zero;
				_lastTarget = _target;

			}
			step ();

		}

		private void SetMouseTarget()
		{
			Console.WriteLine ("Clicked on " + Input.mouseX + " " + Input.mouseY);
			foreach (Node node in _world.NodeList) {
				if (Math.Abs (Input.mouseX - node.Position.x) < node.Radius && Math.Abs (Input.mouseY - node.Position.y) < node.Radius) {
						_target = node;
						Console.WriteLine ("Changed targets to {0}", _target.Position);
					}

				_velocity = _target.Position.Clone ().Substract (this.position).Normalize ().Scale (_speed);
					this.rotation = this._velocity.GetAngleDegrees ();
				//padright
			}
			if (_target != _lastTarget)
				_lastTarget.GetPath (new List<Node> (), _target);

		}

		private void step()
		{
			this.position.x += _velocity.x;
			this.position.y += _velocity.y;
		}

	}
}

