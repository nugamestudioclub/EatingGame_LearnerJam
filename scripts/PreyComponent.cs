using Godot;
using System;

public partial class PreyComponent : Node3D
{
	private Vector3 destination;
	private float speed = 1f;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}
	
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//We know where we want to go.
		//Get our current position.
		Vector3 myPosition = Position;
		
		if(myPosition.distance_to(destination)<0.5f){
			//We are at our desintation!
			//Pick a new destination!
		}
		
		//figure out the vector towards our desintation.
		Vector3 movementVector = (myPosition-destination).normalized();
		//Multiply our normal (1 length) vector with our speed to get our "next step in a frame" and
		//add that with our current position
		myPosition+=movementVector*speed;
		//set current position with temporary variable myPosiion.
		Position = myPosition;
	}
}
