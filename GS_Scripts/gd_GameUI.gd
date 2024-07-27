extends CanvasLayer


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var  healthUI
var secondaryWeaponUI
var GameTimerUI
var GameTimer


# Called when the node enters the scene tree for the first time.
func _ready():
	var player = self.owner.get_node("Player")
	player.connect("OnPlayerReady", self, "SetUpUI")
func SetUpUI():
	healthUI = get_node("./BaseControl/HealthGrid")
	secondaryWeaponUI = get_node("./BaseControl/SecondaryWeaponUI")
	GameTimerUI = get_node("./BaseControl/GameTimerUI")
	
	var player = self.owner.get_node("Player")
	
	healthUI.SetupHealthUI(player.GetHealthComponent().GetMaxHealth())
	player.GetHealthComponent().connect("OnHealthUpdated", healthUI, "UpdateHealth")
	
	secondaryWeaponUI.SetUpProgress(player.GetWeapon().GetSecondaryWeaponMaxAmount())
	player.GetWeapon().connect("OnSecondaryWeaponAmountChange", secondaryWeaponUI, "UpdateAmount")
	
	GameTimer = self.owner.get_node("GameTimer")
	

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta):
	if(is_instance_valid(GameTimer)):
		GameTimerUI.UpdateTimerUI(GameTimer.GetTimer())
