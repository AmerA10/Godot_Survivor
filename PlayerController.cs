using Godot;
using System;

public class PlayerController : KinematicBody2D
{


    private PlayerCombatStats combatStats;
    private HealthComponent health;
    private HitBoxComponent hitBox;
    private Experience experience;
    private Sprite sprite;
    private Timer timer;
    private EnemySpawner spawner;
    private AudioStreamPlayer2D audioPlayer;

    public delegate void PlayerReady();
    public PlayerReady OnPlayerReady;

    private Timer HitTimer;

    //TODO: Figure out how to set up the weapon
    private Weapon weapon;

    private CollisionShape2D coll;

    private Vector2 moveDir;
    private Vector2 facingDir;

    private int moveSpeed;

    private bool canPushBack = true;

    public override void _Ready()
    {
        combatStats = GetNode<PlayerCombatStats>("PlayerCombatStats");
        health = GetNode<HealthComponent>("HealthComponent");
        hitBox = GetNode<HitBoxComponent>("HitBoxComponent");
        experience = GetNode<Experience>("Experience");
        sprite = GetNode<Sprite>("Sprite");
        timer = GetNode<Timer>("Timer");
        weapon = GetNode<Weapon>("Weapon");

        audioPlayer = GetNode<AudioStreamPlayer2D>("AudioPlayer");

        HitTimer = GetNode<Timer>("HitTimer");

        coll = hitBox.GetNode<CollisionShape2D>("CollisionShape2D");

        moveDir = new Vector2(0, 0);
        facingDir = new Vector2(1, 0);

        moveSpeed = combatStats.GetMoveSpeed();
        health.SetUpHealth(combatStats.GetMaxHealth());

        timer.Connect("timeout", this, "OnTimerReached");

        timer.WaitTime = (combatStats.GetAttackSpeed());
        timer.Start();
        experience.OnLevelUpEvent += combatStats.OnLevelUp;

        GD.Print("Player Controller Ready");
        OnPlayerReady.Invoke();
    }

    public HealthComponent GetHealthComponent ()
    {
        return health;
    }

    public Weapon GetWeapon()
    {
        return weapon; 
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {


        if (Input.IsActionJustPressed("game_action1")) 
        {
            experience.TestLevelUp();
        }

        if (Input.IsActionJustPressed("game_action2")) 
        {
            weapon.FireSecondary(this.Position, spawner.GetEnemies());
        
        }

        //TODO: Move to physics process
        HandleMoveDirInput();

        moveDir = moveDir.Normalized() * delta;
        this.MoveAndCollide(Vector2.Zero);

        this.Position += moveDir * moveSpeed;
    
    }

    public void HandleMoveDirInput()
    {

        if (Input.IsActionPressed("game_right"))
        {
            moveDir.x += 1;
            facingDir.x = 1;
            sprite.FlipH = false;
        }
        else if (Input.IsActionPressed("game_left"))
        {
            moveDir.x -= 1;
            facingDir.x = -1;
            sprite.FlipH = true;
        }
        else
        {
            moveDir.x = 0;
        }
        if (Input.IsActionPressed("game_up"))
        {
            moveDir.y -= 1;
            facingDir.y = -1;
        }
        else if (Input.IsActionPressed("game_down"))
        {
            moveDir.y += 1;
            facingDir.y = 1;
        }
        else
        {
            moveDir.y = 0;
        }
        if (facingDir.y != 0 && (!Input.IsActionPressed("game_left") && !Input.IsActionPressed("game_right")))
        {
            facingDir.x = 0;
        }
        if (facingDir.x != 0 && (!Input.IsActionPressed("game_up") && !Input.IsActionPressed("game_down")))
        {
            facingDir.y = 0;
        }
        facingDir = facingDir.Normalized();
        
    }
    public void SetEnemySpawner(EnemySpawner enemySpawner)
    {
        this.spawner = enemySpawner;
    }

    public void OnBodyEnteredPlayer(Node2D body)
    {

        EnemyController enemy = body as EnemyController;
        
        if (enemy != null)
        {
            if(canPushBack)
            {
                spawner.MoveEnemiesBack();
                canPushBack = false;
            }
            //Try to move the player or enemy away

            coll.Disabled = true;

            HitTimer.Start();

            health.TakeDamage(1);
        }
    }
    public void OnHitTimerTimeOut()
    {
        coll.Disabled = false;
        canPushBack = true;
    }

    public void OnTimerReached()
    {
       Vector2 targetVec = facingDir.Normalized();
       weapon.FireWeapon(this.Position, targetVec);
    }
}

