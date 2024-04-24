using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    [Export]
    public PackedScene EnemyScene1 { get; set; }
    [Export]
    public PackedScene EnemyScene2 { get; set; }
    [Export]
    public PackedScene EnemyScene3 { get; set; }
    [Export]
    public PackedScene EnemyScene4 { get; set; }

    private int level = 0;

    [Export]
    public Node2D spawnLoc1 { get; set; }
    [Export]
    public Node2D spawnLoc2 { get; set; }
    [Export]
    public Node2D spawnLoc3 { get; set; }


    [Export]
    private int spawnTime = 5;

    [Export] private int maxEnemies = 4;
    [Export] private int maxSpawnableEnemies = 10;

    private int[,] SpawnPercentage = { { 100, 200, 0, 0 }, { 60, 100, 200, 200 }, { 30, 60, 90, 100 }, { 10, 30, 70, 100 } };


    private PlayerController playerTarget;

    //Need a way to pick from a group of random enemies instead of just one
    //Need to remove the enemy from the list once they are intialized
    //Probably pass this instance to the enemies
    List<EnemyController> enemies;

    public override void _Ready()
    {
        playerTarget = this.Owner.GetNode<PlayerController>("Player");
        enemies = new List<EnemyController>();
        playerTarget.SetEnemySpawner(this);

        Timer timer = GetNode<Timer>("Timer");
        timer.WaitTime = spawnTime;
        spawnLoc1 = GetNode<Node2D>("./Loc1");
        spawnLoc2 = GetNode<Node2D>("./Loc2");
        spawnLoc3 = GetNode<Node2D>("./Loc3");
        GD.Print(spawnLoc1.Name + " : " + spawnLoc2.Name + " : " + spawnLoc3.Name);

        timer.Start();
        GD.Randomize();
    }

    public void SpawnEnemy()
    {
        //Spawn position
        Vector2 spawnPos;
        GD.Print("SPAWN FOR LEVEL: " + level);
        

        if (GD.Randf() <= 0.5f)
        {
            spawnPos = new Vector2((float)GD.RandRange(spawnLoc1.GlobalPosition.x, spawnLoc2.GlobalPosition.x), spawnLoc1.GlobalPosition.y);
        }
        else
        {
            spawnPos = new Vector2(spawnLoc1.GlobalPosition.x, (float)GD.RandRange(spawnLoc1.GlobalPosition.y, spawnLoc3.GlobalPosition.y));
        }

        int otherSideSpawn = GD.Randf() <= 0.5f ? 1 : -1;

        spawnPos.x *= otherSideSpawn;
        otherSideSpawn = GD.Randf() <= 0.5f ? 1 : -1;
        spawnPos.y *= otherSideSpawn;


        int chance = Mathf.Abs((int)GD.Randi() % 101);

        EnemyController enemy;

        if (chance <= SpawnPercentage[level,0] && chance < SpawnPercentage[level,1])
        {
            enemy = EnemyScene1.Instance<EnemyController>();
            GD.Print("Spawning enemy 1: " + +chance);

        }
        else if(chance >= SpawnPercentage[level, 1] && chance < SpawnPercentage[level, 2])
        {
            enemy = EnemyScene2.Instance<EnemyController>();
            GD.Print("Spawning enemy 2: " + +chance);
        }
        else if(chance >= SpawnPercentage[level, 2] && chance < SpawnPercentage[level, 3])
        {
            enemy = EnemyScene3.Instance<EnemyController>();
            GD.Print("Spawning enemy 3: " + +chance);
        }
        else if(chance >= SpawnPercentage[level, 3])
        {
            enemy = EnemyScene4.Instance<EnemyController>();
            GD.Print("Spawning enemy 4: " + chance);
        }
        else
        {
            enemy = null;
        }

        if (enemy != null)
        {
            enemy.SetUpEnemy(this, playerTarget);


            enemy.Position = spawnPos;


            enemies.Add(enemy);

            AddChild(enemy);
        }

    }


    public EnemyController GetClosestEnemyTo(Vector2 position)
    {
        if(enemies.Count <= 0)
        {
            return null;
        }
        EnemyController closest = enemies[0];
        foreach(EnemyController enemy in enemies)
        {
            if(Mathf.Abs((enemy.Position - position).LengthSquared()) < Mathf.Abs((closest.Position - position).LengthSquared()))
            {
                closest = enemy;
            }
        }

        return closest;
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        enemies.Remove(enemy);
    }

    public void OnTimerTimeout()
    {
        if(enemies.Count < maxEnemies)
        {
            SpawnEnemy();
        }
    }

    public List<EnemyController> GetEnemies()
    {
        return enemies;
    }

    public void MoveEnemiesBack()
    {
        foreach(EnemyController enemy in enemies)
        {
            enemy.MoveBack();
        }
    }

    public void UpLevel()
    {
        this.level += 1;
        this.maxEnemies++;

        if(level >= 3)
        {
            level = 3;
        }
        if(maxEnemies > maxSpawnableEnemies)
        {
            this.maxEnemies= maxSpawnableEnemies;
        }
    }
}
