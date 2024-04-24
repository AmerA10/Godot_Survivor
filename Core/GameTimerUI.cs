using Godot;
using System;

public class GameTimerUI : Label
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    private float timer;
    
    public override void _Ready()
    {
        timer = 0.0f;
    }

    public void UpdateTimerUI(float timer)
    {
        this.timer = timer;
        string[] seperator= timer.ToString().Split('.');
        
        this.Text = timer.ToString("0.##");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
