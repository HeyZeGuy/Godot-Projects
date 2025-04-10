using System.Security.Cryptography.X509Certificates;
using Godot;

public partial class Player : Area2D
{
	[Signal]
	public delegate void HitEventHandler(); // Custom "Hit" signal to emit later.
	
	[Export]
	public int Speed { get; set; } = 400; // How fast the player moves (px/sec).
	public Vector2 ScreenSize; // Games window size.

	// On entry.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size; // (ScreenSize.X, ScreenSize.Y)
		Hide();
	}

	// Every frame.
	public override void _Process(double delta)
	{
		PlayerMovement(delta);

	}

	// Calcuating player movement.
	private void PlayerMovement(double delta){
		var velocity = CalcVelocity();
		
		WalkAnimation(velocity);
		ChangePosition(velocity, delta);
	}

	// Returns the velocity based on user input.
	private Vector2 CalcVelocity(){
		var velocity = Vector2.Zero; // Setting velocity to (0, 0)

		if (Input.IsActionPressed("move_right")){
			velocity.X += 1;
		}
		if (Input.IsActionPressed("move_left")){
			velocity.X -= 1;
		}
		if (Input.IsActionPressed("move_down")){
			velocity.Y += 1;
		}
		if (Input.IsActionPressed("move_up")){
			velocity.Y -= 1;
		}

		if (velocity.Length() > 0){
			velocity = velocity.Normalized() * Speed;
		}

		return velocity;
	}

	// Changes position based on velocity.
	private void ChangePosition(Vector2 velocity, double delta){
		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
	}

	// Runs animation based on velocity.
	private void WalkAnimation(Vector2 velocity){
		var MoveAnim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		if (velocity.X != 0){
			MoveAnim.Animation = "walk";
			MoveAnim.FlipV = false;
			MoveAnim.FlipH = velocity.X < 0;
		} else if (velocity.Y != 0){
			MoveAnim.Animation = "up";
			MoveAnim.FlipV = velocity.Y > 0;
		}
		
		if (velocity.Length() > 0){
			MoveAnim.Play();
		} else {
			MoveAnim.Stop();
		}
	}

	// Reseting the board.
	public void Start(Vector2 position){
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}

	// Detecting collision.
	public void OnBodyEntered(Node2D body){
		Hide();
		EmitSignal(SignalName.Hit); // Emitting "Hit" signal.
		// Disabling collision at the end of the frame to avoid multii hits.
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}
}
