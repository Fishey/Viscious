using System;
using System.Drawing;

namespace GXPEngine
{
	/**
	 * Base class for AbstractAgent
	 */
	public abstract class AbstractAgent:Canvas
	{
		//the world we move in
		protected NodeWorld _context = null;

		//our tile position
		public Vec2 position = new Vec2(0,0);

		//our update speed, once every 100 ms
		private int _thinkSpeed = 100;
		private int _lastThinkTime = 0;

		public AbstractAgent (int pSize, Color pColor, int pThinkSpeed = 100):base (pSize,pSize)
		{
			graphics.Clear (pColor);
			_thinkSpeed = pThinkSpeed;
			_lastThinkTime = 0;
		}

		virtual public void SetContext (NodeWorld pContext) {
			_context = pContext;
		}

		//update calls think every now and then, and then updates our x/y based on our tile position
		virtual protected void Update() {
			if (Time.time - _lastThinkTime < _thinkSpeed)
				return;
			_lastThinkTime = Time.time;

			think ();
			x = position.x;
			y = position.y;
		}

		abstract protected void think();
	}
}

