using Godot;
using System;

public class HealthComponent : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private int maxHealth = 3;
    private int currentHealth;

    [Signal]
    public delegate void OnDeathEvenetHandler();
    public OnDeathEvenetHandler OnDeathEvenet;

    [Signal]
    public delegate void OnHealthUpdatedEventHandler(int newHealth);
    
    public delegate void HealthDelegate(int newHealth);
    public HealthDelegate OnHealthUpdated;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        currentHealth = maxHealth;
    }
    public void SetUpHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        EmitSignal(nameof(OnHealthUpdatedEventHandler), currentHealth);
        OnHealthUpdated?.Invoke(currentHealth);
        if (currentHealth <= 0) 
        {
            //Fire a IsDead delegate
            EmitSignal(nameof(OnDeathEvenetHandler));
            OnDeathEvenet.Invoke();
        
        }
    }

    public int GetMaxHealth()
    {
        return maxHealth; 
    }


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
