using Godot;

public partial class FadingPopup : Label
{
	public string text;
	public Vector2 position;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = text;
		Position = position;
		
		var timer = GetNode<Timer>("FadingPopupTimer");
		timer.Timeout += QueueFree;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
