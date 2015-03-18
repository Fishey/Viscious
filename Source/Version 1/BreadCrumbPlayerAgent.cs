
using System;
using System.Drawing;
using System.Collections.Generic;

namespace GXPEngine
{
	/**
	 * BreadCrumbPlayerAgent drops BreadCrumbs so that other agents can follow them.
	 */
	public class BreadCrumbPlayerAgent:AbstractAgent
	{
		//breadcrumb wraps a position and shows it in a textfield for debugging purposes
		//basically all we need is a position
		class BreadCrumb:TextField {
			public IntVec2 position;

			public BreadCrumb (IntVec2 pPosition, int pSize):base (pSize, pSize) {
				position = pPosition;
			}
		}

		//list of all breadcrumbs we've dropped, so we can clean them if we want to 
		private List<BreadCrumb> _crumbs = new List<BreadCrumb>();
		//used to check whether we need to drop a breadcrumb
		private IntVec2 _lastPosition = new IntVec2(0,0);

		public BreadCrumbPlayerAgent(int pSize, Color pColor, int pThinkSpeed = 100) : base (pSize, pColor, pThinkSpeed)
		{
		}

		/**
		 * Check movement and if moved, drop a breadcrumb.
		 */
		protected override void think ()
		{
			horVerMovement ();

			if (!_lastPosition.Equals (position)) {
				dropBreadCrumb ();
				_lastPosition = position.Clone();
			}
		}

		void horVerMovement() {
			IntVec2 newPos = position.Clone ();

			if (Input.GetKey(Key.LEFT))
				newPos.x--;
			else if (Input.GetKey(Key.RIGHT))
				newPos.x++;
			else if (Input.GetKey(Key.UP))
				newPos.y--;
			else if (Input.GetKey(Key.DOWN))
				newPos.y++;

			if (_context.IsWalkable (newPos)) {
				position = newPos;
			}
		}

		void dropBreadCrumb() {
			BreadCrumb crumb = new BreadCrumb (position, width);	//create crumb
			crumb.text = ""+_crumbs.Count;							//set visual info
			crumb.x = crumb.position.x * _context.tileSize;			//set position on screen
			crumb.y = crumb.position.y * _context.tileSize;
			parent.AddChild (crumb);								//make sure it is visible
			_crumbs.Add (crumb);									//keep track of it in our list
		}

		public int CrumbCount {
			get { return _crumbs.Count; }
		}

		public IntVec2 CrumbAt (int pIndex) {
			return _crumbs[pIndex].position;
		}

	}
}

