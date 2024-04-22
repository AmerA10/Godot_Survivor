using Godot;
using System;

public class Projectile : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

   
    public delegate void OnProjectileHitEventHandler();
    public OnProjectileHitEventHandler OnProjecatileHit;

    // Called when the node enters the scene tree for the first time.
    private Vector2 moveDir = new Vector2(1, 1);
    private int moveSpeed = 300;
    private int damage = 1;
    public override void _Ready()
    {
        //GD.Print("projectile ready");
    }
    
    //Don't think I need this
    public override void _Notification(int what)
    {
        
        //GD.Print("Projectile Notif");
        //GD.Print(what);
    }

    public void SetUpProjectile(Vector2 moveDir, int moveSpeed, int damage)
    {
       // GD.Print("Setting up projectile");
        //GD.Print(moveDir.ToString());
        //GD.Print(moveSpeed.ToString());
        this.moveDir = moveDir;
        this.moveSpeed = moveSpeed;
        this.damage = damage;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //TODO:Move this to physics process
    public override void _PhysicsProcess(float delta)
    {
        this.Position += moveDir * moveSpeed * delta;
        this.Rotate(delta * Mathf.Pi * 2);
    }

    public void OnAreaEntered(Node2D body)
    {
        HealthComponent enemyHealth = body.GetNodeOrNull<HealthComponent>("./HealthComponent");
        if(enemyHealth != null && !body.IsQueuedForDeletion()) 
        {
            OnProjecatileHit.Invoke();
            enemyHealth.TakeDamage(damage);
            QueueFree();
        }

    }
}
