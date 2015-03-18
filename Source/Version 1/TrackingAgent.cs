
using System;
using System.Drawing;

namespace GXPEngine
{
	//walking around...
	public class TrackingAgent:AbstractAgent
	{
		//for random wandering
		private IntVec2[] _directions = new IntVec2[] {
			new IntVec2	(0,1),new IntVec2	(0,-1),new IntVec2	(1,0),new IntVec2	(-1,0)
		};
		private int _currentDirection = 0;
		private Random _random = new Random();
		private int _steps = 0;

		//for breadcrumbs...
		private BreadCrumbPlayerAgent _target;

		public TrackingAgent (int pSize, Color pColor, BreadCrumbPlayerAgent pTarget, int pThinkSpeed) : base (pSize, pColor, pThinkSpeed)
		{
			_target = pTarget;
		}

		protected override void think ()
		{
			IntVec2 breadCrumb = sniffForCrumb ();

			if (breadCrumb != null && _context.IsWalkable (breadCrumb)) {
				graphics.Clear (Color.Red);
				position = breadCrumb.Clone ();
			} else {
				graphics.Clear (Color.PowderBlue);
				moveRandomly ();
			}
		}

		IntVec2 sniffForCrumb() {
			//return the position of the freshest nearest crumb if we detect it
			return null;
		}

		void moveRandomly() {
			IntVec2 newPos = position.Clone ().Add (_directions [_currentDirection]);

			if (_steps > 5 || newPos.Equals (position) || !_context.IsWalkable (newPos)) {
				_currentDirection = _random.Next (0, 4);
				_steps = 0;
			} else {
				position = newPos;
				_steps++;
			}
		}

	}
}

