extends GridContainer


export(PackedScene) var highlight
export(Resource) var tutorial
export(Array) var buttons

export(PackedScene) var PlayScene

export(Resource) var btnClick
export(Resource) var btnPlay
export(Resource) var btnMove

var audioPlayer:AudioStreamPlayer2D

var currentBtn:TextureButton
var currentBtnIndex
var fader

var IsDisplayingTutorial:bool

# Called when the node enters the scene tree for the first time.
func _ready():
	highlight = get_node("../Highlight")
	fader = get_node("/root/ColorRectFader")
	tutorial = get_node("../TutorialContainer")
	audioPlayer = get_node("../ButtonAudio")
	tutorial.visible = false
	IsDisplayingTutorial = false
	fader.FadeOut();

	for n in self.get_children():
		buttons.append(n)

	currentBtnIndex = 0
	currentBtn = buttons[0]
	highlight.rect_position = currentBtn.get_global_rect().position + ((currentBtn.rect_size / 2) - (highlight.rect_size / 2))
	
func ChangeSelectedButton(var delta):
	audioPlayer.stop()
	audioPlayer.stream = btnMove
	audioPlayer.play()
	
	ClearPresses()
	
	currentBtnIndex = min(max(currentBtnIndex + delta,0), buttons.size() - 1)
	currentBtn = buttons[currentBtnIndex]
	currentBtn.grab_focus()
	
	highlight.rect_position = currentBtn.get_global_rect().position + ((currentBtn.rect_size / 2) - (highlight.rect_size / 2))
	
	
func ClearPresses():
	for n in buttons:
		n.pressed = false
		
func PressCurrentButton():
	if(is_instance_valid(currentBtn)):
		if(IsDisplayingTutorial):
			tutorial.visible = false
			IsDisplayingTutorial = false
			return
		
		currentBtn.pressed = true
		currentBtn.grab_focus()
		match currentBtnIndex:
			0:
				audioPlayer.stop()
				audioPlayer.stream = btnPlay
				audioPlayer.play()
				if(is_instance_valid(fader)):
					
					if(!fader.GetAnimPlayer().is_connected("animation_finished",self, "OnAnimationEnd")):
						fader.GetAnimPlayer().connect("animation_finished", self,"GoToPlay")
					fader.FadeIn()
			1:
				audioPlayer.stop()
				audioPlayer.stream = btnClick
				audioPlayer.play()
				tutorial.visible = true
				IsDisplayingTutorial = true
			2:
				get_tree().quit()

func GoToPlay(var _animName):
	if(fader.GetAnimPlayer().is_connected("animation_finished",self,"OnAnimationEnd")):
		fader.GetAnimPlayer().disconnect("animation_finished",self,"GoToPlay")
	get_tree().change_scene_to(PlayScene)
	
# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
