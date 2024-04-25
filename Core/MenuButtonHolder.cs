using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

public class MenuButtonHolder : GridContainer
{
    [Export]
    private TextureRect highlight;

    [Export]
    private List<TextureButton> buttons;

    [Export]
    private PackedScene PlayScene;

    private TextureButton currentBtn;

    private int currentBtnIndex;


    [Export]
    private ColorRectFader fader;

    public override void _Ready()
    {

        highlight = GetNodeOrNull<TextureRect>("../Highlight");
        fader = GetNodeOrNull<AnimationPlayer>("../FaderAnimation");

        GD.Print(highlight.Name);

        foreach(TextureButton texBtn in this.GetChildren())
        {
            buttons.Add(texBtn);
        }

        currentBtnIndex = 0;
        currentBtn = buttons[0];
        highlight.RectPosition = currentBtn.GetGlobalRect().Position + ((currentBtn.RectSize / 2) - (highlight.RectSize / 2)); 

    }

    public void ChangeSelectedButton(int delta)
    {
        ClearPresses();

        currentBtnIndex = Mathf.Min(Mathf.Max(currentBtnIndex + delta,0), buttons.Count - 1);
        currentBtn = buttons[currentBtnIndex];
        currentBtn.GrabFocus();

        highlight.RectPosition = currentBtn.GetGlobalRect().Position + ((currentBtn.RectSize / 2) - (highlight.RectSize / 2));
        
    }

    public void ClearPresses()
    {
        GD.Print("ClearPresses");
        foreach(TextureButton btn in buttons)
        {
            btn.Pressed = false;
        }
    }

    public async void PressCurrentButton()
    {
        if(IsInstanceValid(currentBtn))
        {
            currentBtn.Pressed= true;
            currentBtn.GrabFocus();
            switch (currentBtnIndex)
            {
                case 0:
                    fader.GetAnimPlayer().Connect("animation_finished", this, "GoToPlay");
                    fader.FadeIn();
                    
                    break;
                case 1:

                    break;
                case 2:
                    GetTree().Quit();
                    break;
                default:
                    break;
            }
        }
    }

    public void GoToPlay(string animName)
    {
        GetTree().ChangeSceneTo(PlayScene);
    }
    


}
