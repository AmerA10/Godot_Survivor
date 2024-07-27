extends KinematicBody2D


signal OnPlayerReady

var timer:Timer
var hitTimer:Timer
var coll:CollisionShape2D

var moveDir:Vector2
var facingDir:Vector2

var moveSpeed:float
var canPushBack:bool

var weapon
var spawner

var health
var combatStats
var sprite:Sprite

# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	combatStats = get_node("PlayerCombatStats")
	health = get_node("HealthComponent")
	coll = get_node("HitBoxComponent/CollisionShape2D")
	sprite = get_node("Sprite")
	timer = get_node("Timer")
	hitTimer = get_node("HitTimer")
	
	weapon = get_node("Weapon")
	
	moveDir = Vector2(0,0)
	moveSpeed = combatStats.GetMoveSpeed()
	facingDir = Vector2(1,0)
	health.SetUpHealth(combatStats.GetMaxHealth())
	
	timer.connect("timeout",self, "OnTimerReached")
	timer.wait_time = combatStats.GetAttackSpeed()
	timer.start()
	
	emit_signal("OnPlayerReady")


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	
	if(Input.is_action_just_pressed("game_action2")):
		weapon.FireSecondaryWeapon(self.position, spawner.GetEnemies())
	
	HandleMoveDirInput()
	
	moveDir = moveDir.normalized() * delta
	
	self.move_and_collide(Vector2.ZERO)
	
	self.position += moveDir * moveSpeed

func HandleMoveDirInput():
	if(Input.is_action_pressed("game_right")):
		moveDir.x += 1
		facingDir.x = 1
		sprite.flip_h = false
	elif(Input.is_action_pressed("game_left")):
		moveDir.x -= 1
		facingDir.x = -1
		sprite.flip_h = true
	else:
		moveDir.x = 0
	if(Input.is_action_pressed("game_up")):
		moveDir.y -= 1
		facingDir.y = -1
	elif(Input.is_action_pressed("game_down")):
		moveDir.y += 1
		facingDir.y = 1
	else:
		moveDir.y = 0
		
	if(facingDir.y != 0 && (!Input.is_action_pressed("game_left") && !Input.is_action_pressed("game_right"))):
		facingDir.x = 0
		
	if(facingDir.x != 0 && (!Input.is_action_pressed("game_up") && !Input.is_action_pressed("game_down"))):
		facingDir.y = 0
		
	facingDir = facingDir.normalized()

func SetEnemySpawner(var enemySpawner):
	self.spawner = enemySpawner

func OnBodyEnteredPlayer(var body:Node2D):
	var enemy = body
	if(enemy != null):
		coll.set_deferred("Disable",true)
		
		if(canPushBack):
			spawner.MoveEnemiesBack()
			canPushBack = false
		hitTimer.start()
		
		health.TakeDamage(1)

func OnHitTimerTimeOut():
	coll.set_deferred("Disabled", false)
	canPushBack = true

func OnTimerReached():
	var targetVec:Vector2 = facingDir.normalized()
	weapon.FireWeapon(self.position, targetVec)

func UpdateStats():
	self.moveSpeed = combatStats.GetMoveSpeed()
	self.timer.WaitTime = combatStats.GetAttackSpeed()
	
func GetHealthComponent():
	return self.health
func GetWeapon():
	return self.weapon
func GetCombatStats():
	return self.combatStats
