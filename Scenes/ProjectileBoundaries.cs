using Godot;
using System;

public class ProjectileBoundaries : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
    public void OnAreaEntered(RID areaId, Area2D otherArea, int areaShapeIndex, int localShapeIndex)
    {

        Projectile proj = otherArea.GetOwnerOrNull<Projectile>();

        
        if(IsInstanceValid(proj))
        {
            proj.QueueFree();
            return;
        }

    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
