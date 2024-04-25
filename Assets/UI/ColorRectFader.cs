using Godot;
using System;

public class ColorRectFader : ColorRect
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    private AnimationPlayer anim;

    public override void _Ready()
    {
        anim = GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
    }

    public void FadeIn()
    {
        anim.Stop();
        anim.Play("FadeIn");
    }

    public void FadeOut()
    {
        anim.Stop();
        anim.Play("FadeOut");
    }

    public AnimationPlayer GetAnimPlayer()
    {
        return anim;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
