using Godot;
using System;

public partial class Main : Node
{
	private PlayerMovement player;
	private TransitionRect transitionRect;
	private HungerBar hungerBar;
	private AudioManager manager;
	private Timer timer;
	private DayTimer dayTimer;

	[Export]
	public PackedScene[] EnemyScenes { get; set; }

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
		player = GetNode<PlayerMovement>("Player");
		transitionRect = GetNode<TransitionRect>("TransitionRect");
		hungerBar = GetNode<HungerBar>("HungerBar");
		manager = GetNode<AudioManager>("AudioManager");
		timer = GetNode<Timer>("Timer");
		dayTimer = GetNode<DayTimer>("DayTimer");
		player.EatPrey += hungerBar.Update;
		timer.Timeout += NextRound;
		StartRound(InitialEnemies);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		dayTimer.Update((int) timer.TimeLeft);
	}

	public void NextRound()
	{
		round++;
		int nextRoundEnemyCount = GetNextRoundCount(DespawnEnemies());
		StartRound(nextRoundEnemyCount);
	}

	private void StartRound(int preyCount)
	{
		player.Hunger = 0;
		hungerBar.Update(player.Hunger, player.MinHunger);
		transitionRect.Fade(round);
		gameState = UpdateGameState(preyCount);
		// TODO Update music and sprites
		switch (gameState)
		{
			default:
				break;
		}
		SpawnEnemies(preyCount);
		timer.Start(60);

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
			int randomIndex = rn.RandiRange(0, EnemyScenes.Length - 1);
			PreyComponent prey = EnemyScenes[randomIndex].Instantiate<PreyComponent>();
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
		GameState newState;

		if (preyCount < InitialEnemies * .25f)
		{
			manager.PlaySong(2);
			newState=  GameState.SAD;
		} 
		else if (preyCount < InitialEnemies * .5f)
		{
			manager.PlaySong(1);
			newState = GameState.MEDIUM;
		} 
		else
		{
			manager.PlaySong(0);
			newState = GameState.HAPPY;
		}
		GD.Print($"Game State: {newState}");
		return newState;
		

	}

	public enum GameState
	{
		HAPPY, MEDIUM, SAD
	}

}
