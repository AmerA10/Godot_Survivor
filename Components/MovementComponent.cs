using Godot;
using System;

public class MovementComponent : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    private Node2D target;
    private int moveSpeed;
    private int maxMoveSpeed;

    public override void _Ready()
    {
        
    }
    public void SetMoveSpeed(int speed)
    {
        this.moveSpeed = speed;
    }

    public int GetMoveSpeed()
    {
        return this.moveSpeed;
    }

    public void SetTarget(Node2D target) 
    { 
        this.target= target;    
    }

    public Node2D GetTarget()
    {
        return this.target;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
