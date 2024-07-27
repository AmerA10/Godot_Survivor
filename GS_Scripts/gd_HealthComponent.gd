extends Node2D

var maxHealth:int
var currentHealth:int

signal OnDeathEvent

signal OnHealthUpdated(new_value)

# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	currentHealth = maxHealth

func SetUpHealth(var newMaxHealth:int):
	self.maxHealth = newMaxHealth
	self.currentHealth = newMaxHealth

func TakeDamage(var damage:int):
	print("Taking damage", damage, "Health: ", currentHealth)
	if(currentHealth < 1):
		return
	currentHealth -= damage
	emit_signal("OnHealthUpdated",currentHealth)
	if(currentHealth <= 0):
		print("DYING")
		emit_signal("OnDeathEvent")

func GetMaxHealth():
	return maxHealth
func GetCurrentHealth():
	return currentHealth

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
