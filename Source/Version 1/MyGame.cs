using System;
using GXPEngine;
using System.Drawing;
using GXPEngine.OpenGL;

public class MyGame : Game
{	
	static void Main() {
		new MyGame().Start();
	}

	NodeWorld _nodeWorld;

	public MyGame () : base(1280, 720, false, true)
	{
		GL.ClearColor (1, 1, 1, 1);
		_nodeWorld = new NodeWorld ();
		AddChild (_nodeWorld);

		_nodeWorld.AddNode (new Node (1,30, Color.Blue, new Vec2 (200, 100))); // 1
		_nodeWorld.AddNode (new Node (3,30, Color.Blue, new Vec2 (400, 50))); // 3
		_nodeWorld.AddNode (new Node (4,30, Color.Blue, new Vec2 (700, 300))); // 4

		_nodeWorld.AddNode (new Node (2,30, Color.Blue, new Vec2 (400, 180))); // 2
		_nodeWorld.AddNode (new Node (4,30, Color.Blue, new Vec2 (500, 250))); // 3
		_nodeWorld.AddNode (new Node (3,30, Color.Blue, new Vec2 (500, 400))); // 4

		_nodeWorld.AddNode (new Node (2,30, Color.Blue, new Vec2 (300, 100))); // 2
		_nodeWorld.AddNode (new Node (3,30, Color.Blue, new Vec2 (700, 450))); // 3
		_nodeWorld.AddNode (new Node (4,30, Color.Blue, new Vec2 (900, 350))); // 4

	}

	void Update () {

	}

}

