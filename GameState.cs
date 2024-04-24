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
    }
        
    
    public void OnTimerIntervalReached()
    {
        playerCombatStats.AddAttackSpeed(0.1f);
        playerCombatStats.AddMoveSpeed(10.0f);
    }

    public void HandlePlayerDeath()
    {
        currentState = gameState.DONE;
    }

}
