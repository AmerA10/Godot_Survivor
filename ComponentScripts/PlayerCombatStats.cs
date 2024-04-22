using Godot;
using System;

public class PlayerCombatStats : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.


    [Signal]
    public delegate void OnStatsUpdated();

    [Export]
    private int attackSpeed;
    [Export]
    private int moveSpeed;
    [Export]
    private int maxHealth;
    public override void _Ready()
    {
        
    }

    public void OnLevelUp()
    {
        GD.Print("Combat stats should change now");
    }

    public int GetMoveSpeed() { return moveSpeed; }
    public int GetAttackSpeed() { return attackSpeed; }

    public int GetMaxHealth() { return maxHealth; }

    public void UpdateAttackSpeed(int newAttackSpeed)
    {
        this.attackSpeed= newAttackSpeed;
        EmitSignal(nameof(OnStatsUpdated));
    }
    public void UpdateMoveSpeed(int newMoveSpeed)
    {
        this.moveSpeed= newMoveSpeed;
        EmitSignal(nameof(OnStatsUpdated));

    }
    public void UpdateMaxHealth(int newMaxHealth) 
    {
        this.maxHealth = newMaxHealth;
        EmitSignal(nameof(OnStatsUpdated));

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
