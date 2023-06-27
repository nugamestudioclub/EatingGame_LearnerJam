using Godot;
using System;

public partial class HungerBar : Control
{

	private Label textLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		textLabel = GetNode<Label>("Label");
		Update(0, 0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Update(int curHunger, int minHunger)
	{
		textLabel.Text = $"Hunger: {curHunger}/{minHunger}";
		GD.Print(minHunger);
	}
}
