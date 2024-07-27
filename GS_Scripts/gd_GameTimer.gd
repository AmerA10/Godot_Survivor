extends Node


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


var timer:float = 0.0

export(int) var timerInterval:int = 10

signal OnTimerReached

# Called when the node enters the scene tree for the first time.
func _ready():
	timer = 0.0


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta):
	timer += delta
	
	if(timer as int % timerInterval == 0 && timer > timerInterval):
		timerInterval += timer
		emit_signal("OnTimerReached")

func GetTimer():
	return timer

func SetInterva(var interval:int):
	self.timerInterval = interval
