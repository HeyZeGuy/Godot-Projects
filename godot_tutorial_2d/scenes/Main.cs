using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }

	private int _score;
	private enum PLAYER_STATE { DEAD, ALIVE };
	private PLAYER_STATE _player_state;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewGame();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void GameOver(){
		_player_state = PLAYER_STATE.DEAD;

		GetNode<Timer>("MobTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
	}

	public void NewGame(){
		_score = 0;
		_player_state = PLAYER_STATE.ALIVE;

		var Player = GetNode<Player>("Player");	
		var StartPosition = GetNode<Marker2D>("StartPosition");	
		Player.Start(StartPosition.Position);
	}

	// Start countdown.
	private void _on_start_timer_timeout(){
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}
	// Updating score.
	private void _on_score_timer_timeout(){
		_score++;
		GD.Print(_score);
	}
	// Spawning mob.
	private void _on_mob_timer_timeout(){
		SpawnMob();
	}

	// Mob is spawned randomly on a Path2D/PathFollow2D node.
	private void SpawnMob(){
		// Creating a Mob instance.
		Mob mob = MobScene.Instantiate<Mob>();

		// Choose a random location on Path2D - between 0-1.
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		// Set the mob's direction perpendicular to the path direction.
		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mob.Rotation = direction;

		// Position.
		mob.Position = mobSpawnLocation.Position;

		// Velocity.
		Vector2 velocity = new Vector2(200, 0);
		mob.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(mob);

		mob.NearMiss += _on_near_miss; // Adding connection to NearMiss mob signal.
	}

	private void _on_near_miss(){
		if (_player_state == PLAYER_STATE.ALIVE){
			GD.Print("Near miss!");
			_score += 10;
		}
	}
}
