using Godot;
using System;

public partial class HUD : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_start_button_pressed(){
		GetNode<Button>("StartButton").Hide();
		EmitSignal(SignalName.StartGame);
	}

	public void UpdateScore(int score){
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}

	async public void ShowGameOver(){
		ShowMessage("Game Over.");

		await ToSignal(GetTree().CreateTimer(2.0), SceneTreeTimer.SignalName.Timeout);
		ShowMessage("Dodge the Creeps!");
		GetNode<Button>("StartButton").Show();
	}

	public void ShowMessage(string text){
		var message = GetNode<Label>("Message");
		message.Text = text;
		message.Show();
	}
}
