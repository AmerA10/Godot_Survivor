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

    private int level = 1;

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

    private int[,] SpawnPercentage = { { 100, 0, 0, 0 }, { 50, 40, 0, 0 }, { 30, 30, 30, 10 }, { 10, 20, 40, 30 } };


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
    }

    public void SpawnEnemy()
    {
        //Spawn position
        Vector2 spawnPos;

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


        int chance = (int)GD.Randi() % 101;

        EnemyController enemy;

        if (chance >= SpawnPercentage[level,0] && chance < SpawnPercentage[level,1])
        {
            enemy = EnemyScene1.Instance<EnemyController>();

        }
        else if(chance >= SpawnPercentage[level, 1] && chance < SpawnPercentage[level, 2])
        {
            enemy = EnemyScene2.Instance<EnemyController>();
        }
        else if(chance >= SpawnPercentage[level, 2] && chance < SpawnPercentage[level, 3])
        {
            enemy = EnemyScene3.Instance<EnemyController>();
        }
        else
        {
            enemy = EnemyScene4.Instance<EnemyController>();
        }

        enemy.SetUpEnemy(this, playerTarget);


        enemy.Position = spawnPos;


        enemies.Add(enemy);

        AddChild(enemy);
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

        if(level >= 4)
        {
            level = 4;
        }
        if(maxEnemies > maxSpawnableEnemies)
        {
            this.maxEnemies= maxSpawnableEnemies;
        }
    }
}
