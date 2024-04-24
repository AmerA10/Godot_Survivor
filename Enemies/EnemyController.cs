using Godot;
using System;

public class EnemyController : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    private HealthComponent healthComp;
    private MovementComponent moveComp;

    private EnemySpawner spawner;

    private Vector2 desiredVel;
    private Vector2 steering;

    private Node2D target;

    private Sprite sprite;

    [Export] private Vector2 velocity;
    [Export] private Vector2 acceleration;
    [Export] private Vector2 repulsiveForce;

    //This needs to come from somewhere else
    [Export] private int Health;
    [Export] private float maxMoveSpeed;
    [Export] private int testMoveSpeed;
    [Export] private float maxForce;
    [Export] private int seperationDistance;



    public override void _Ready()
    {
        healthComp = GetNode<HealthComponent>("HealthComponent");
        moveComp = GetNode<MovementComponent>("MovementComponent");
        healthComp.OnDeathEvenet += OnDeathHandle;
        sprite = GetNodeOrNull<Sprite>("Sprite");
    }

    public void SetUpEnemy(EnemySpawner spawner ,Node2D target)
    {
      
        healthComp = GetNode<HealthComponent>("HealthComponent");
        moveComp = GetNode<MovementComponent>("MovementComponent");
        healthComp.OnDeathEvenet += OnDeathHandle;

        moveComp.SetTarget(target);
        moveComp.SetMoveSpeed(testMoveSpeed);
        healthComp.SetUpHealth(Health);
        this.spawner = spawner;
    }

    public void OnDeathHandle()
    {
        if(spawner != null)
        {
            spawner.RemoveEnemy(this);

        }
        QueueFree();

    }
    public override void _PhysicsProcess(float delta)
    {
        target = moveComp.GetTarget();
        if (IsInstanceValid(target))
        {
            desiredVel = (target.Position - this.Position) + repulsiveForce;
            desiredVel = desiredVel.Normalized() * maxMoveSpeed;
            steering = desiredVel - velocity;
            steering = steering.LimitLength(maxForce);
            acceleration += steering;
            
            velocity += acceleration;
            velocity = velocity.LimitLength(maxMoveSpeed);
           
            KinematicCollision2D kinColl = this.MoveAndCollide(velocity * delta);


        }

        if(spawner != null && spawner.GetEnemies().Count > 1)
        {
            int numClose = 0;
            repulsiveForce = Vector2.Zero;
            foreach (EnemyController enemy in spawner.GetEnemies())
            {
                if(enemy != this)
                {
                    float distance = Mathf.Abs((enemy.Position - this.Position).Length());
                    if (distance < (seperationDistance))
                    {
                        Vector2 repulsVector = (this.Position - enemy.Position) /  (distance * 0.5f);
                        numClose++;
                        repulsiveForce += repulsVector;
                    }
                }

            }
            if(numClose > 0)
            {
                repulsiveForce /= numClose;
            }
            repulsiveForce *= maxMoveSpeed * 0.75f;

        }
        
    }

    public void MoveBack()
    {
        if(target != null)
        {

            acceleration = -acceleration;
            velocity = (this.Position - target.Position) * maxMoveSpeed;

        }
    }

}
