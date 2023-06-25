using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public PackedScene EnemyScene { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void NextRound()
	{
		SpawnEnemies(GetNextRoundCount(DespawnEnemies()));
	}

	// Despawns all enemies and returns the number of enemies despawned
	private int DespawnEnemies()
	{
		int count = 0;
		foreach (Node node in GetTree().GetNodesInGroup("prey"))
		{
			node.QueueFree();
			count++;
		}
		
		return count;
	}

	// Gets the number of enemies to spawn next round
	private int GetNextRoundCount(int count)
	{
		return (int) (count * 1.5f);
	}

	// Spawns the given number of enemies
	private void SpawnEnemies(int count)
	{
		for (int i = 0; i < count; i++)
		{
			PreyComponent prey = EnemyScene.Instantiate<PreyComponent>();
			Vector3 spawnPosition = new Vector3();
			prey.Position = spawnPosition;
			AddChild(prey);
		}
	}

}
