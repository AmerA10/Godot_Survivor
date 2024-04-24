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
    private float attackSpeed;
    [Export]
    private float moveSpeed;
    [Export]
    private int maxHealth;

    [Export]
    private float maxAttackSpeed;

    [Export]
    private float maxMoveSpeed;
    public override void _Ready()
    {
        
    }

    public void OnLevelUp()
    {
        GD.Print("Combat stats should change now");
    }

    public float GetMoveSpeed() { return moveSpeed; }
    public float GetAttackSpeed() { return attackSpeed; }

    public int GetMaxHealth() { return maxHealth; }

    public void AddAttackSpeed(float newAttackSpeed)
    {
        this.attackSpeed -= newAttackSpeed;
        if(this.attackSpeed < maxAttackSpeed)
        {
            this.attackSpeed = maxAttackSpeed;
        }
        EmitSignal(nameof(OnStatsUpdated));
    }
    public void AddMoveSpeed(float newMoveSpeed)
    {
        
        this.moveSpeed += newMoveSpeed;
        if(this.moveSpeed > maxMoveSpeed)
        {
            this.moveSpeed = maxMoveSpeed;
        }
        EmitSignal(nameof(OnStatsUpdated));

    }
    public void UpdateMaxHealth(int newMaxHealth) 
    {
        this.maxHealth = newMaxHealth;
        EmitSignal(nameof(OnStatsUpdated));

    }

}
