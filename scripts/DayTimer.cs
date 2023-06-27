using Godot;
using System;

public partial class DayTimer : Control
{
	private Label label;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		label = GetNode<Label>("Label");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void Update(int curTime) {
		label.Text = $"Daylight Remaining: {curTime}";
	}
}
