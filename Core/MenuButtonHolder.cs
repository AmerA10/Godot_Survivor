using Godot;
using System;
using System.Collections.Generic;

public class MenuButtonHolder : GridContainer
{
    [Export]
    private TextureRect highlight;

    [Export]
    private List<TextureButton> buttons;

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


        currentBtnIndex = Mathf.Min(Mathf.Max(currentBtnIndex + delta,0), buttons.Count - 1);

        currentBtn = buttons[currentBtnIndex];

        highlight.RectPosition = currentBtn.GetGlobalRect().Position + ((currentBtn.RectSize / 2) - (highlight.RectSize / 2));

    }

    public void PressCurrentButton()
    {

    }
    


}
