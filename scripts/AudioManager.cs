using Godot;
using System;

public partial class AudioManager : Node3D
{
	private AudioStreamPlayer[] players = new AudioStreamPlayer[3];
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		//load in each player
		players[0] = GetNode<AudioStreamPlayer>("Happy Song Player");
        players[1] = GetNode<AudioStreamPlayer>("Medium Song Player");
        players[2] = GetNode<AudioStreamPlayer>("Sad Song Player");
    }


	public void PlaySong(int trackNum)
	{
        for (int i = 0; i < players.Length; ++i)
            players[i].Stop();
        players[trackNum].Play();
	}
}
