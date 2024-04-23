using Godot;
using System;

public class Projectile : Node2D
{

   
    public delegate void OnProjectileHitEventHandler();
    public OnProjectileHitEventHandler OnProjecatileHit;

    private Vector2 moveDir = new Vector2(1, 1);
    private int moveSpeed = 300;
    private int damage = 1;
    public override void _Ready()
    {

    }
    

    public void SetUpProjectile(Vector2 moveDir, int moveSpeed, int damage)
    {

        this.moveDir = moveDir;
        this.moveSpeed = moveSpeed;
        this.damage = damage;
    }


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
