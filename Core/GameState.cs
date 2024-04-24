using Godot;
using System;

public class GameState : Node2D
{

    private PlayerController player;
    private PlayerCombatStats playerCombatStats;
    private EnemySpawner enemySpawner;

    private enum gameState { PLAYING, DONE };

    private gameState currentState = gameState.PLAYING;

    private GameTimer gameTimer;

    public override void _Ready()
    {
        gameTimer = new GameTimer();
        this.AddChild(gameTimer);
        player = GetNodeOrNull<PlayerController>("./Player");
        player.GetHealthComponent().OnDeathEvenet += HandlePlayerDeath;
        playerCombatStats = player.GetPlayerCombatStats();
        enemySpawner = GetNodeOrNull<EnemySpawner>("./EnemySpawner");
        gameTimer.OnTimerReached += OnTimerIntervalReached;
    }
        
    
    public void OnTimerIntervalReached()
    {
        if (IsInstanceValid(player))
        {
        playerCombatStats.AddAttackSpeed(0.25f);
        playerCombatStats.AddMoveSpeed(10.0f);

        }
        if(IsInstanceValid(enemySpawner))
        {
        enemySpawner.UpLevel();

        }
    }

    public void HandlePlayerDeath()
    {
        currentState = gameState.DONE;
    }

}
