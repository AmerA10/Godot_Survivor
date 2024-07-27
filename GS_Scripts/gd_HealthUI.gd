extends GridContainer


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
export(PackedScene) var HealthDot


# Called when the node enters the scene tree for the first time.
func _ready():
	self.columns = 1

func SetupHealthUI(var maxHealth:int):
	self.columns = maxHealth
	for i in maxHealth:
		var dot = HealthDot.instance()
		self.add_child(dot)

func UpdateHealth(var newHealth:int):
	if(newHealth < 0): 
		return
	
	for i in self.get_child_count():
		var child = get_child(i)
		var con:Control = child
		
		if(i > newHealth - 1):
			con.visible = false
		else:
			con.visible = true 

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
