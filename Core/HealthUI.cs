using Godot;
using Godot.Collections;
using System;

public class HealthUI : GridContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Export] PackedScene HealthDot;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.Columns = 1;
    }

    public void SetupHealthUI(int maxHealth)
    {
        Columns = maxHealth;
        for(int i = 0; i < maxHealth; i++)
        {
            var dot = ResourceLoader.Load<PackedScene>(HealthDot.ResourcePath).Instance();
            this.AddChild(dot);
        }
    }

    public void UpdateHealth(int newHealth)
    {
        if (newHealth < 0) return;


        for(int i = 0; i < GetChildren().Count; i++)
        {
            var child = GetChild(i);
            Control con = child as Control;
            
            if (i > newHealth - 1)
            {
                con.Visible = false;
            }
            else
            {
                //child.GetNode<Control>("./HealthNode").Visible = true;
                con.Visible = true;

            }
        }
    }
}
