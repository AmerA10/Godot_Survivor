using Godot;
using System;
using System.Collections.Generic;

public class Weapon : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export]private int weaponDamage;
    private Projectile projectile;

    [Export]
    private PackedScene weaponPath;

    [Export]
    private int secondaryFireMeterMax;
    private int secondaryFireMeter = 0;

    [Export] private int secondaryFireDamage;

    [Export] PackedScene secondaryWeaponPath;

    List<HomingProjectile> missiles = new List<HomingProjectile>();

    public delegate void OnSecondaryWeaponAmountChangeEvent(int newAmount);
    public OnSecondaryWeaponAmountChangeEvent OnSecondaryAmountUpdate;

    public override void _Ready()
    {

    }

    public void FireWeapon(Vector2 pos, Vector2 dir)
    {
        var proj = ResourceLoader.Load<PackedScene>(weaponPath.ResourcePath).Instance();
      //  GD.Print("Firing projectile");
        //GD.Print(proj.ToString());
        
        projectile = proj.GetNode<Projectile>(".");
        projectile.SetAsToplevel(true);
        projectile.SetUpProjectile(dir, 400, weaponDamage);
        projectile.Position = pos;
        projectile.OnProjecatileHit += OnProjectileHit;
        this.AddChild(projectile);
    }


    public void FireSecondary(Vector2 pos, List<EnemyController> targets)
    {
        if(secondaryFireMeter < secondaryFireMeterMax)
        {
            return;
        }

        missiles.Clear();

        foreach (EnemyController target in targets)
        {
            var proj = ResourceLoader.Load<PackedScene>(secondaryWeaponPath.ResourcePath).Instance();

            HomingProjectile projectile = proj.GetNode<HomingProjectile>(".");
            projectile.SetAsToplevel(true);
            projectile.SetupMissile(target, 400, secondaryFireDamage);
            projectile.Position = pos;

            missiles.Add(projectile);

            this.AddChild(projectile);
            GD.Print("Creating Missile");
        }

        secondaryFireMeter = 0;
        OnSecondaryAmountUpdate?.Invoke(secondaryFireMeter);
    }

    public void OnProjectileHit()
    {
        AddSecondaryMeter(1);
    }
    public int GetSecondaryWeaponMaxAmount()
    {
        return secondaryFireMeterMax;
    }

    public void AddSecondaryMeter(int amt)
    {
        secondaryFireMeter = Mathf.Min(secondaryFireMeter + amt, secondaryFireMeterMax);
        OnSecondaryAmountUpdate?.Invoke(secondaryFireMeter);
    }

}
