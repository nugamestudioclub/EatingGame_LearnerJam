using Godot;
using System;

public partial class PlayerMovement : CharacterBody3D
{
	[Export]
	private float speed = 5;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		velocity.X = direction.X * speed;
		velocity.Z = direction.Z * speed;
		velocity.Y = 0;

		Velocity = velocity;
		MoveAndSlide();
	}
}
