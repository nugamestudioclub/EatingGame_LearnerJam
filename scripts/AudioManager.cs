using Godot;
using System;

public partial class AudioManager : Node3D
{
	[Export]
	private AudioStreamPlayer[] players;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		//load in each player
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlaySong(GameState gameState)
	{
		switch(gameState)
		{
			case GameState.Happy:
                players[0].Play();
                players[1].Stop();
                players[2].Stop();
				break;
            case GameState.Medium:
                players[0].Stop();
                players[1].Play();
                players[2].Stop();
                break;
            case GameState.Sad:
                players[0].Stop();
                players[1].Stop();
                players[2].Play();
                break;
        }
	}
}
