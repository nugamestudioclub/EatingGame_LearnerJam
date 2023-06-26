using Godot;
using System;

public partial class PlayerMovement : CharacterBody3D
{
	[Export]
	public int Hunger { private set; get;}
	[Export]
	public int MinHunger { private set; get;}
	private int overStuffed = 0;
	[Export]
	private float BaseSpeed = 5;
	[Export]
	private float SpeedPerOverstuff { set; get; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		float speed = BaseSpeed + overStuffed * SpeedPerOverstuff;
		Vector3 velocity = Velocity;

		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		velocity.X = direction.X * speed;
		velocity.Z = direction.Z * speed;
		velocity.Y = 0;

		Velocity = velocity;
		MoveAndSlide();
		// Despawn any prey if collision
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			KinematicCollision3D collision = GetSlideCollision(i);
			if (collision.GetCollider().IsClass("Node"))
			{
				Node collider = (Node)collision.GetCollider();
				if (collider.IsInGroup("prey"))
				{
					Hunger += 1;
					if (Hunger > MinHunger)
					{
						overStuffed += 1;
					}
					collider.QueueFree();
					GD.Print(overStuffed);
				}
			}
		}
	}
}
