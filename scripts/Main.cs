using Godot;
using System;

public partial class Main : Node
{
	private TransitionRect transitionRect;

	[Export]
	public PackedScene EnemyScene { get; set; }

	[Export]
	public int InitialEnemies { get; set; }
	[Export]
	private Rect2 spawnBounds { get; set; }
	[Export]
	private Vector2 spawnBoundSize { get; set; }

	private GameState gameState = GameState.HAPPY;

	private RandomNumberGenerator rn = new RandomNumberGenerator();

	private int round = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		transitionRect = GetNode<TransitionRect>("TransitionRect");
		StartRound(InitialEnemies);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_accept"))
		{
			NextRound();
		}
	}

	public void NextRound()
	{
		round++;
		int nextRoundEnemyCount = GetNextRoundCount(DespawnEnemies());
		StartRound(nextRoundEnemyCount);
	}

	private void StartRound(int preyCount)
	{
		transitionRect.Fade(round);
		gameState = UpdateGameState(preyCount);
		// TODO Update music and sprites
		switch (gameState)
		{
			default:
				break;
		}
		SpawnEnemies(preyCount);
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
			Vector2 spawnPosition = new Vector2(
				rn.RandfRange(spawnBounds.Position.X, spawnBounds.End.X - spawnBoundSize.X),
				rn.RandfRange(spawnBounds.Position.Y, spawnBounds.End.Y - spawnBoundSize.Y));
			Rect2 bounds = new Rect2(spawnPosition, spawnBoundSize);
			prey.Position = new Vector3(bounds.GetCenter().X, 0, bounds.GetCenter().Y);
			prey.Bounds = bounds;
			AddChild(prey);
		}
	}

	// Gets the game state based off the current number of prey
	private GameState UpdateGameState(int preyCount)
	{
		if (preyCount < InitialEnemies * .25f)
		{
			return GameState.SAD;
		} 
		else if (preyCount < InitialEnemies * .5f)
		{
			return GameState.MEDIUM;
		} 
		else
		{
			return GameState.HAPPY;
		}
	}

	enum GameState
	{
		HAPPY, MEDIUM, SAD
	}

}
