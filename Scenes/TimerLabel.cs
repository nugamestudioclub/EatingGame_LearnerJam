using Godot;
using System;

public partial class TimerLabel : Label
{
	[Export]
	private double maxTime = 60f;
	private double time;

	[Export]
	private Main main;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Reset();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		time -= delta;
		if (time <= 0)
		{
			main.NextRound();
			Reset();
		}
		Text = $"Daylight Remaining: {(int)time}";
	}

	private void Reset()
	{
		time = maxTime;
	}
}
