using Godot;
using System;

public partial class TransitionRect : ColorRect
{
	private AnimationPlayer animPlayer;
	private Label dayLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		dayLabel = GetNode<Label>("Label");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Fade(int day)
	{
		dayLabel.Text = String.Format("Day {0}", day);
		animPlayer.Play("FadeOut");
	}
}
