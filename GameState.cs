using Godot;
using System;

public class GameState : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

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
