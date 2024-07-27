extends CanvasLayer


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

export(Resource) var menuBtns




# Called when the node enters the scene tree for the first time.
func _ready():
	menuBtns = get_node("./MainMenu/MenuButtonHolder")


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if(Input.is_action_just_pressed("game_up")):
		menuBtns.ChangeSelectedButton(-1)
	elif(Input.is_action_just_pressed("game_down")):
		menuBtns.ChangeSelectedButton(1)
	if(Input.is_action_just_pressed("game_action1")):
		menuBtns.PressCurrentButton()
