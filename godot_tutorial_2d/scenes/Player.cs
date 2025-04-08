using Godot;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 401; // How fast the player moves (px/sec).
	public Vector2 ScreenSize; // Games window size.
}
