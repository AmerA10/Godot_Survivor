extends Node


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

signal OnStatusUpdated

export(float) var attackSpeed
export(float) var moveSpeed
export(int) var maxHealth
export(float) var maxAttackSpeed
export(float) var maxMoveSpeed

func AddAttackSpeed(var newAttackSpeed:float):
	self.attackSpeed -= newAttackSpeed
	if(self.attackSpeed < self.maxAttackSpeed):
		self.attackSpeed = maxAttackSpeed
	emit_signal("OnStatusUpdated")

func AddMoveSpeed(var newMoveSpeed:float):
	self.moveSpeed += newMoveSpeed
	if(self.moveSpeed > self.maxMoveSpeed):
		self.moveSpeed = self.maxMoveSpeed
	emit_signal("OnStatusUpdated")

func UpdateMaxHealth(var newMaxHealth:int):
	self.maxHealth = newMaxHealth
	emit_signal("OnStatusUpdated")
	
func GetMaxHealth():
	return self.maxHealth
func GetMoveSpeed():
	return self.moveSpeed
func GetAttackSpeed():
	return self.attackSpeed

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
