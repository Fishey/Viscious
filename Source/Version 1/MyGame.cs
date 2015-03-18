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

		_nodeWorld.AddNode (new Node (1,30, Color.Blue, new Vec2 (200, 100))); // 0
		_nodeWorld.AddNode (new Node (4,30, Color.Blue, new Vec2 (400, 50))); // 1
		_nodeWorld.AddNode (new Node (4,30, Color.Blue, new Vec2 (700, 300))); // 2

		_nodeWorld.AddConnection (_nodeWorld.GetNodeAt (0), _nodeWorld.GetNodeAt (1));
		_nodeWorld.AddConnection (_nodeWorld.GetNodeAt (0), _nodeWorld.GetNodeAt (2));
	

	}

	void Update () {

	}

}

