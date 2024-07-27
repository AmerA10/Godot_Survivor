extends Node2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

signal OnProjectileHit

var moveDir:Vector2 = Vector2(1,1)
var moveSpeed:int = 300
var damage:int = 1

func SetUpProjectile(var moveDir:Vector2, var moveSpeed:int, var damage:int):
	self.moveDir = moveDir
	self.moveSpeed = moveSpeed
	self.damage = damage


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta):
	self.position += moveDir * moveSpeed * delta
	self.rotate(delta* PI * 2)

func OnAreaEntered(var body:Node2D):
	var enemyHealth = body.get_node("./HealthComponent")
	if(enemyHealth != null && !body.is_queued_for_deletion()):
		emit_signal("OnProjectileHit")
		enemyHealth.TakeDamage(damage)
		queue_free()
