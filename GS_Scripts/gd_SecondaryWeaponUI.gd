extends TextureProgress


var overTexture:Texture




# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	self.value = 0
	self.tint_over = Color(tint_over.a, tint_over.g,tint_over.b,0.0)
	self.step = 1.0

func SetUpProgress(var maxAmount:int):
	self.max_value = maxAmount
func ResetProgress():
	self.value = 0

func UpdateAmount(var amount:int):
	self.value = amount
	if(value == max_value):
		tint_over = Color(tint_over.a, tint_over.g,tint_over.b,0.8)
	else:
		tint_over = Color(tint_over.a, tint_over.g,tint_over.b,0.0)


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
