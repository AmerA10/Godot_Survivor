extends Label


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

var timer:float

# Called when the node enters the scene tree for the first time.
func _ready():
	timer = 0.0

# Called every frame. 'delta' is the elapsed time since the previous frame.
func UpdateTimerUI(var timer:float):
	self.timer = timer
	var seperator:Array = String(timer).split(".")
	self.text = String(stepify(timer,0.01))
	
