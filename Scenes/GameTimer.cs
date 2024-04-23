using Godot;
using System;


public class GameTimer : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    private float timer = 0.0f;

    private int timerInterval = 30;

    public delegate void OnTimerIntervalReachedEvent();
    public OnTimerIntervalReachedEvent OnTimerReached;

    public override void _Ready()
    {
        timer = 0.0f;
    }

    public override void _PhysicsProcess(float delta)
    {
        timer += delta;

        if (((int)timer % timerInterval) == 0 ) 
        {
            //Interval Reached
            OnTimerReached?.Invoke();
        
        }

    }

    public float GetTimer()
    {
        return timer;
    }

    public void SetInterval(int interval)
    {
        this.timerInterval = interval;
    }
}
