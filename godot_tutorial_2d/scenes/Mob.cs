using Godot;
using System;

public partial class Mob : RigidBody2D
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayRandomAnim();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnVisibleOnScreenNotifier2DScreenExited(){
		QueueFree(); // Unloading the node.
	}

	private void PlayRandomAnim(){
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
		animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);
	}
}
