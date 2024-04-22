using Godot;
using System;

public class Experience : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export]
    private int maxExp = 20;
    [Export]
    private int currentExp;

    [Signal]
    public delegate void OnLevelUpEventHandler();
    public OnLevelUpEventHandler OnLevelUpEvent;

    public override void _Ready()
    {
        currentExp = 0;
    }

    public void AddExperience(int experience)
    {
        currentExp += experience;
        if(currentExp >= maxExp)
        {
            //Fire a signal that we leveled up
            EmitSignal(nameof(OnLevelUpEventHandler));
            OnLevelUpEvent.Invoke();
        }
    }

    public void TestLevelUp()
    {
        AddExperience(100);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
