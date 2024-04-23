using Godot;
using System;

public class GameState : Node2D
{

    private PlayerController player;

    private enum gameState { PLAYING, DONE };

    private gameState currentState = gameState.PLAYING;

    private GameTimer gameTimer;

    public override void _Ready()
    {
        gameTimer = new GameTimer();
        this.AddChild(gameTimer);
        player = GetNodeOrNull<PlayerController>("./Player");
        player.GetHealthComponent().OnDeathEvenet += HandlePlayerDeath;
        GD.Print("Got player: " + player);
    }
        
    
    public void OnTimerIntervalReached()
    {

    }

    public void HandlePlayerDeath()
    {
        currentState = gameState.DONE;
    }

}
