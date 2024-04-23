using Godot;
using System;

public class GameUI : CanvasLayer
{

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

        PlayerController playerController = this.Owner.GetNodeOrNull<PlayerController>("Player");

        healthUI.SetupHealthUI(playerController.GetHealthComponent().GetMaxHealth());
        playerController.GetHealthComponent().OnHealthUpdated += healthUI.UpdateHealth;

        secondaryWeaponUI.SetUpProgress(playerController.GetWeapon().GetSecondaryWeaponMaxAmount());
        playerController.GetWeapon().OnSecondaryAmountUpdate += secondaryWeaponUI.UpdateAmount;

    
    }

}
