using Godot;
using System;

public class MainMenu : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    [Export] MenuButtonHolder menuBtns;

    public override void _Ready()
    {
        menuBtns = GetNodeOrNull<MenuButtonHolder>("./MainMenu/MenuButtonHolder");
        GD.Print(menuBtns.Name);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

        if (Input.IsActionJustPressed("game_up"))
        {
            menuBtns.ChangeSelectedButton(-1);
        }
        else if (Input.IsActionJustPressed("game_down"))
        {
            menuBtns.ChangeSelectedButton(1);
        }
        if (Input.IsActionJustPressed("game_action1"))
        {
            menuBtns.PressCurrentButton();
        }
    }
}
