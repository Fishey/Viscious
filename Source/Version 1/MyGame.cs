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

		_nodeWorld.AddNode (new Node (30, Color.Blue, new Vec2 (100, 100))); // 0
		_nodeWorld.AddNode (new Node (30, Color.Blue, new Vec2 (400, 50))); // 1
		_nodeWorld.AddNode (new Node (30, Color.Blue, new Vec2 (700, 300))); // 2
		_nodeWorld.AddNode (new Node (30, Color.Blue, new Vec2 (300, 300))); // 3
		_nodeWorld.AddNode (new Node (30, Color.Blue, new Vec2 (100, 500))); // 4
		_nodeWorld.AddNode (new Node (30, Color.Blue, new Vec2 (900, 300))); // 5

		_nodeWorld.AddConnection (_nodeWorld.GetNodeAt (0), _nodeWorld.GetNodeAt (1));
		_nodeWorld.AddConnection (_nodeWorld.GetNodeAt (0), _nodeWorld.GetNodeAt (2));
		_nodeWorld.AddConnection (_nodeWorld.GetNodeAt (0), _nodeWorld.GetNodeAt (3));
		_nodeWorld.AddConnection (_nodeWorld.GetNodeAt (0), _nodeWorld.GetNodeAt (4));
		_nodeWorld.AddConnection (_nodeWorld.GetNodeAt (3), _nodeWorld.GetNodeAt (4));
		_nodeWorld.AddConnection (_nodeWorld.GetNodeAt (2), _nodeWorld.GetNodeAt (3));
		_nodeWorld.AddConnection (_nodeWorld.GetNodeAt (5), _nodeWorld.GetNodeAt (2));

		_nodeWorld.AddAgent (new WanderingAgent (_nodeWorld, 15, Color.Red, 5));
	}

	void Update () {

	}

}

