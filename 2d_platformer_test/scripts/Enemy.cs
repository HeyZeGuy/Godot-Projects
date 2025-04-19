using Godot;
using System;

public partial class Enemy : Node2D
{
	[Export]
	public float SPEED = 60f; 

	private RayCast2D RayCastRight; 
	private RayCast2D RayCastLeft;
	private AnimatedSprite2D Anim;

	private int Direction = 1; 

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RayCastRight = GetNode<RayCast2D>("RayCastRight"); 
		RayCastLeft = GetNode<RayCast2D>("RayCastLeft"); 
		Anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D"); 
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		EnemyWalk(delta);
	}

	private void EnemyWalk(double delta)
	{
		if (RayCastRight.IsColliding()){
			Direction = -1;
			Anim.FlipH = true;
		} else if (RayCastLeft.IsColliding()) {
			Direction = 1;
			Anim.FlipH = false;
		}
		
		Vector2 NewPos = Position;
		NewPos.X += SPEED * Direction * (float)delta;

		Position = NewPos;
	}
}
