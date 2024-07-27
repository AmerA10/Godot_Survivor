extends Node2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
const GameTimer =preload("res://GS_Scripts/gd_GameTimer.gd")

var player
var playerCombatStats
var enemySpawner

enum gameState {PLAYING,DONE}
var currentState = gameState.PLAYING

var gameTimer
var fader

export(PackedScene) var MenuScene

# Called when the node enters the scene tree for the first time.
func _ready():
	gameTimer = GameTimer.new()
	self.add_child(gameTimer)
	player = get_node("./Player")
	if(is_instance_valid(player)):
		playerCombatStats = player.GetCombatStats()
	enemySpawner = get_node("./EnemySpawner")
	gameTimer.connect("OnTimerReached",self, "OnTimerIntervalReached")
	fader =get_node("/root/ColorRectFader")
	
	player.GetHealthComponent().connect("OnDeathEvent", self, "HandlePlayerDeath")
	
	if(is_instance_valid(fader)):
		fader.FadeOut()

func OnTimerIntervalReached():
	if(is_instance_valid(player)):
		playerCombatStats.AddAttackSpeed(0.25)
		playerCombatStats.AddMoveSpeed(10.0)
	if(is_instance_valid(enemySpawner)):
		enemySpawner.UpLevel()
		
func HandlePlayerDeath():
	
	currentState = gameState.DONE
	
	if(!fader.GetAnimPlayer().is_connected("animation_finished",self, "GoToMenu")):
		fader.GetAnimPlayer().connect("animation_finished",self,"GoToMenu")
		
	fader.FadeIn()
	
	player.GetHealthComponent().disconnect("OnDeathEvent",self, "HandlePlayerDeath")
	
	get_tree().change_scene("res://Scenes/Main.tscn")

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
