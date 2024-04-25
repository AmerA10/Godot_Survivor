using Godot;
using System;

public class ColorRectFader : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    private AnimationPlayer anim;

    public override void _Ready()
    {
        this.Visible = false;
        anim = GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
        if(!anim.IsConnected("animation_finished", this, "OnAnimationEnd"))
        {
            anim.Connect("animation_finished", this, "OnAnimationEnd");

        }
    }

    public void FadeIn()
    {
        this.Visible = true;
        anim.Stop();
        anim.Play("FadeIn");
    }

    public void FadeOut()
    {
        this.Visible = true;
        anim.Stop();
        anim.Play("FadeOut");
    }

    public AnimationPlayer GetAnimPlayer()
    {
        return anim;
    }

    public void OnAnimationEnd(string name)
    {
        if(name == "FadeOut")
        {
            this.Visible = false;
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
