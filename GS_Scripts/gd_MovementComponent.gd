extends Node2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

var target:Node2D
var moveSpeed:int
var maxMoveSpeed:int


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

func SetMoveSpeed(var speed:int):
	self.moveSpeed = speed
func GetMoveSpeed():
	return self.moveSpeed
func SetTarget(var target:Node2D):
	self.target = target
func GetTarget():
	return self.target
# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
