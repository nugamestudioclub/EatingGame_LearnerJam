using Godot;
using System;

public partial class PlayerMovement : Node3D
{
    [Export]
    private float speed = 5;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Vector3 position = Position;
        if (Input.IsActionPressed("ui_right"))
        {
            position.X += speed * (float)delta;
        }
        if (Input.IsActionPressed("ui_left"))
        {
            position.X -= speed * (float)delta;
        }
        if (Input.IsActionPressed("ui_up"))
        {
            position.Z -= speed * (float)delta;
        }
        if (Input.IsActionPressed("ui_down"))
        {
            position.Z += speed * (float)delta;
        }
        Position = position;
    }
}
