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

    private ColorRectFader fader;

    [Export]
    private PackedScene MenuScene;

    public override void _Ready()
    {
        gameTimer = new GameTimer();
        this.AddChild(gameTimer);
        player = GetNodeOrNull<PlayerController>("./Player");
        player.GetHealthComponent().OnDeathEvenet += HandlePlayerDeath;
        playerCombatStats = player.GetPlayerCombatStats();
        enemySpawner = GetNodeOrNull<EnemySpawner>("./EnemySpawner");
        gameTimer.OnTimerReached += OnTimerIntervalReached;
        fader = GetNodeOrNull<ColorRectFader>("/root/ColorRectFader");

        if(IsInstanceValid(fader))
        {
            fader.FadeOut();

        }
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
        GD.Print("Handling player death");
        currentState = gameState.DONE;
        if (!fader.GetAnimPlayer().IsConnected("animation_finished", this, "GoToMenu"))
        {
            fader.GetAnimPlayer().Connect("animation_finished", this, "GoToMenu");

        }
        fader.FadeIn();
        player.GetHealthComponent().OnDeathEvenet -= HandlePlayerDeath;
    }
    public void GoToMenu(string name)
    {
        GD.Print("Changing scenes");
        if (fader.GetAnimPlayer().IsConnected("animation_finished", this, "GoToMenu"))
        {
            fader.GetAnimPlayer().Disconnect("animation_finished", this, "GoToMenu");

        }
        GetTree().ChangeScene("res://Scenes/Main.tscn");
    }

}
