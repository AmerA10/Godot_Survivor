using Godot;
using System;

public class GameUI : CanvasLayer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.


    private HealthUI healthUI;
    private SecondaryWeaponUI secondaryWeaponUI;

    public override void _Ready()
    {
        PlayerController playerController = this.Owner.GetNodeOrNull<PlayerController>("Player");

        playerController.OnPlayerReady += SetUpUI;
    }

    public void SetUpUI()
    {
        healthUI = GetNode<HealthUI>("./BaseControl/HealthGrid");
        secondaryWeaponUI = GetNode<SecondaryWeaponUI>("./BaseControl/SecondaryWeaponUI");

        GD.Print(healthUI.Name);

        PlayerController playerController = this.Owner.GetNodeOrNull<PlayerController>("Player");

        healthUI.SetupHealthUI(playerController.GetHealthComponent().GetMaxHealth());
        playerController.GetHealthComponent().OnHealthUpdated += healthUI.UpdateHealth;

        secondaryWeaponUI.SetUpProgress(playerController.GetWeapon().GetSecondaryWeaponMaxAmount());
        playerController.GetWeapon().OnSecondaryAmountUpdate += secondaryWeaponUI.UpdateAmount;

    
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
