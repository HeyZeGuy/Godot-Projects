using Godot;
using System;

public partial class Killzone : Area2D
{
	private Timer timer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_body_entered(Node2D body)
	{
		Engine.TimeScale = 0.5;
		timer.Start();
	}

	private void _on_timer_timeout()
	{
		GetTree().ReloadCurrentScene();
		Engine.TimeScale = 1;
	}
}
