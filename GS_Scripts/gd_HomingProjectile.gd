extends Node2D


var target
var moveSpeed:int = 300
var damage:int = 1

var timer:Timer
var isTimerSet:bool = false

var velocity:Vector2
var acceleration:Vector2

var desiredVel:Vector2
var steering:Vector2

export(float) var maxForce = 0.8

func SetupMissile(var target, var moveSpeed:int, var damage:int):
	self.target = target
	self.moveSpeed = moveSpeed
	self.damage = damage
	
	if(is_instance_valid(target)):
		self.velocity = (self.position - target.position).rotated(rand_range(-PI / 2, PI / 2)) * self.moveSpeed * 3


# Called when the node enters the scene tree for the first time.
func _ready():
	self.timer = Timer.new()
	self.timer.one_shot = true
	self.timer.wait_time = 10
	self.add_child(timer)
	self.timer.start()
	self.isTimerSet = true


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta):
	if(is_instance_valid(target)):
		desiredVel = (self.target.position - self.position).normalized() * moveSpeed
		steering = desiredVel - velocity
		steering = steering.limit_length(maxForce)
		acceleration += steering
		
		velocity += acceleration
		velocity = velocity.limit_length(moveSpeed)
		self.rotation = velocity.angle() + (PI / 2)
		maxForce += 0.025
	
	self.position += velocity * delta
	
	if(timer.time_left <= 0 && isTimerSet == true):
		queue_free()

func OnAreaEntered(var body:Node2D):
	if(body != target):
		return
	
	var enemyHealth = body.get_node("./HealthComponent")
	if(enemyHealth != null):
		enemyHealth.TakeDamage(damage)
		queue_free()
		

