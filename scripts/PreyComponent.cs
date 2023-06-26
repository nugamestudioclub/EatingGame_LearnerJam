using Godot;
using System;

public partial class PreyComponent : CharacterBody3D
{
	[Export]
	public float DistancePerMove { get; set; }
	[Export]
	public float Speed = 3f;
	// Bounds represent global position
	[Export]
	public Rect2 Bounds { get; set; }
	
	private Vector3 destination;

	private CollisionShape3D preyCollider;

	private RandomNumberGenerator rn = new RandomNumberGenerator();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		preyCollider = GetNode<CollisionShape3D>("PreyCollider");
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		//We know where we want to go.
		//Get our current position.
		Vector3 myPosition = Position;

		if (myPosition.DistanceTo(destination) < 0.5f)
		{
			//We are at our desintation!
			//Pick a new destination!
			PickNewPosition();
		}
		// Move to destination
		Velocity = myPosition.DirectionTo(destination) * Speed;
		MoveAndSlide();
		//MoveAndCollide(Velocity * (float) delta);
	}

	public void PickNewPosition()
	{
		Vector3 posOffset = new Vector3(rn.RandfRange(-1, 1), 0, rn.RandfRange(-1, 1)).Normalized() * DistancePerMove;
		Vector3 globalPos = GlobalPosition + posOffset;
		Vector2 worldPos = new Vector2(globalPos.X, globalPos.Z);
		if (!Bounds.HasPoint(worldPos))
		{
			PickNewPosition();
		}
		else
		{
			destination = Position + posOffset;
			GD.Print(destination);
		}
	}
}
