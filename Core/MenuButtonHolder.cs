using Godot;
using System;
using System.Collections.Generic;

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

    public override void _Ready()
    {

        highlight = GetNodeOrNull<TextureRect>("../Highlight");

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

    public void PressCurrentButton()
    {
        if(IsInstanceValid(currentBtn))
        {
            currentBtn.Pressed= true;
            currentBtn.GrabFocus();
            switch (currentBtnIndex)
            {
                case 0:
                    GetTree().ChangeSceneTo(PlayScene);
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
    


}
