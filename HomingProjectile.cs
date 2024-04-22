using Godot;
using System;

public class HomingProjectile : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private EnemyController target = null;
    private int moveSpeed = 300;
    private int damage = 1;

    private Timer timer;
    bool isTimerSet = false;

    private Vector2 velocity;
    private Vector2 acceleration;

    private Vector2 desiredVel;
    private Vector2 steering;
    [Export] private float maxForce = 0.8f;

    // Called when the node enters the scene tree for the first time.

    public void SetupMissile(EnemyController target, int moveSpeed, int damage)
    {
        this.target = target;
        this.moveSpeed = moveSpeed;
        this.damage = damage;
        if(IsInstanceValid(target))
        {

            this.velocity = (this.Position - target.Position).Rotated((float)GD.RandRange(-Mathf.Pi / 2, Mathf.Pi / 2)) * moveSpeed * 3;
        }
        GD.Print("Setting up Projectile");
    }
    public override void _Ready()
    {
        timer = new Timer();
        timer.OneShot = true;
        timer.WaitTime = 10;
        AddChild(timer);
        timer.Start();
        isTimerSet = true;
        GD.Print("Projectile Ready");
        
    }

    public override void _PhysicsProcess(float delta)
    {
        if(IsInstanceValid(target))
        {
          
            desiredVel = (this.target.Position - this.Position).Normalized() * moveSpeed;
            steering = desiredVel - velocity;
            steering = steering.LimitLength(maxForce);
            acceleration += steering;

            velocity += acceleration;
            velocity = velocity.LimitLength(moveSpeed);

            this.Rotation = (float)((velocity.Angle()) + (Math.PI / 2.0));
            maxForce += 0.025f;
        }

        this.Position += velocity * delta;


        if (timer.TimeLeft <= 0 && isTimerSet == true)
        {
            QueueFree();
        }
    }
    public void OnAreaEntered(Node2D body)
    {
        GD.Print("Missile entered body");
        if(body != this.target)
        {
            return;
        }

        HealthComponent enemyHealth = body.GetNodeOrNull<HealthComponent>("./HealthComponent");
        GD.Print("Getting component: " + enemyHealth.Name);
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            QueueFree();
        }

    }
}
