extends KinematicBody2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

var healthComp
var moveComp

var spawner

var desiredVel:Vector2
var steering:Vector2

var target:Node2D

var sprite:Sprite

export(Vector2)var velocity:Vector2
export(Vector2)var acceleration:Vector2
export(Vector2)var repulsiveForce:Vector2

export(int)   var Health:int
export(float) var maxMoveSpeed:float
export(int)   var testMoveSpeed:int
export(float) var maxForce:float
export(int)   var seperationDistance:int


# Called when the node enters the scene tree for the first time.
func _ready():
	healthComp = get_node("HealthComponent")
	moveComp = get_node("MovementComponent")
	if(!healthComp.is_connected("OnDeathEvent",self,"OnDeathHandle")):
		healthComp.connect("OnDeathEvent", self, "OnDeathHandle")
	sprite = get_node("Sprite")

func SetUpEnemy(var spawner, var target:Node2D):
	healthComp = get_node("HealthComponent")
	moveComp = get_node("MovementComponent")
	if(!healthComp.is_connected("OnDeathEvent",self,"OnDeathHandle")):
		healthComp.connect("OnDeathEvent", self, "OnDeathHandle")
	
	moveComp.SetTarget(target)
	moveComp.SetMoveSpeed(testMoveSpeed)
	healthComp.SetUpHealth(Health)
	self.spawner = spawner

func OnDeathHandle():
	print("Dying")
	if(spawner != null):
		spawner.RemoveEnemy(self)
	self.queue_free()
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta):
	target = moveComp.GetTarget()
	if(is_instance_valid(target)):
		desiredVel = (target.position - self.position) + repulsiveForce
		desiredVel = desiredVel.normalized() * maxMoveSpeed
		steering = desiredVel - velocity	
		steering = steering.limit_length(maxForce)
		acceleration += steering
		
		velocity += acceleration
		velocity = velocity.limit_length(maxMoveSpeed)
		var kinColl = self.move_and_collide(velocity * delta)
		
	if(spawner != null && spawner.GetEnemies().size() > 1):
		var numClose:int = 0
		repulsiveForce = Vector2.ZERO
		for enemy in spawner.GetEnemies():
			if(enemy != self):
				var distance = abs((enemy.position - self.position).length())
				if(distance < seperationDistance):
					var repulsVector:Vector2 = (self.position - enemy.position) / (distance * 0.05)
					numClose += 1
					repulsiveForce += repulsVector
		if(numClose > 0):
			repulsiveForce /= numClose
		repulsiveForce *= maxMoveSpeed * 0.75
	if(is_instance_valid(sprite)):
		sprite.flip_h = false
	else:
		sprite.flip_h =  true

func MoveBack():
	if(target != null):
		acceleration = -acceleration
		velocity = (self.position - target.position) * maxMoveSpeed
