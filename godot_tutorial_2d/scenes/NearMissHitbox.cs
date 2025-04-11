using Godot;
using System;

public partial class NearMissHitbox : Area2D
{
	[Signal]
	public delegate void NearMissEventHandler();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void _on_area_exited(Area2D area){
		EmitSignal(SignalName.NearMiss);
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}
}
