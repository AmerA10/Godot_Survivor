using Godot;
using System;

public class GameState : Node2D
{

    private PlayerController player;

    private enum gameState { PLAYING, DONE };

    private gameState currentState = gameState.PLAYING;

    public override void _Ready()
    {
        player = GetNodeOrNull<PlayerController>("./Player");
        player.GetHealthComponent().OnDeathEvenet += HandlePlayerDeath;
        GD.Print("Got player: " + player);
    }

    public void HandlePlayerDeath()
    {
        currentState = gameState.DONE;
    }

}
