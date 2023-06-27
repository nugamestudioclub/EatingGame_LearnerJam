using Godot;
using System;

public partial class PlayerMovement : CharacterBody3D
{
	[Signal]
	public delegate void EatPreyEventHandler(int curHunger, int minHunger);

	[Export]
	public int Hunger { set; get;}
	[Export]
	public int MinHunger { set; get;}
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
					EmitSignal(SignalName.EatPrey, Hunger, MinHunger);
					collider.QueueFree();
				}
			}
		}
	}
}
