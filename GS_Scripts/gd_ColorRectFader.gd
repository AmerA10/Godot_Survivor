extends CanvasLayer


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

var anim: AnimationPlayer


# Called when the node enters the scene tree for the first time.
func _ready():
	self.visible = false
	anim = get_node("AnimationPlayer")
	if(!anim.is_connected("animation_finished", self, "OnAnimationEnd")):
		anim.connect("animation_finished", self,"OnAnimationEnd")

func FadeIn():
	self.visible =  true
	anim.stop()
	anim.play("FadeIn")

func FadeOut():
	self.visible = true
	anim.stop()
	anim.play("FadeOut")
	
func GetAnimPlayer():
	return anim

func OnAnimationEnd(var name):
	if(name == "FadeOut"):
		self.visible = false

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
