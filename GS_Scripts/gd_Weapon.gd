extends Node


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
export(int) var weaponDamage
var projectile
export(PackedScene) var weaponPath
export(int) var secondaryFireMeterMax
var secondaryFireMeter:int

export(int) var secondaryFireDamage

export(PackedScene) var secondaryWeaponPath

var missiles:Array

signal OnSecondaryWeaponAmountChange(newVal)

var primaryAudio:AudioStreamPlayer2D
var secondaryAudio:AudioStreamPlayer2D

func FireWeapon(var pos:Vector2, var dir:Vector2):
	var proj = weaponPath.instance()
	
	projectile = proj.get_node(".")
	projectile.set_as_toplevel(true)
	projectile.SetUpProjectile(dir,400,weaponDamage)
	projectile.position = pos
	
	projectile.connect("OnProjectileHit",self,"OnProjectileHit")
	
	self.add_child(projectile)
	primaryAudio.play()
	

func FireSecondaryWeapon(var pos:Vector2, var targets:Array):
	if(secondaryFireMeter < secondaryFireMeterMax):
		return
	
	missiles.clear()
	
	for target in targets:
		var proj = secondaryWeaponPath.instance()
		
		var projectile = proj.get_node(".")
		projectile.set_as_toplevel(true)
		projectile.SetupMissile(target,400,secondaryFireDamage)
		projectile.position = pos
		
		missiles.append(projectile)
		
		self.add_child(projectile)
		secondaryAudio.play()
		
	secondaryFireMeter = 0
	emit_signal("OnSecondaryWeaponAmountChange",secondaryFireMeter)

func OnProjectileHit():
	AddSecondaryMeter(1)

func GetSecondaryWeaponMaxAmount():
	return secondaryFireMeterMax

func AddSecondaryMeter(var amt:int):
	secondaryFireMeter = min(secondaryFireMeter + amt, secondaryFireMeterMax)
	emit_signal("OnSecondaryWeaponAmountChange",secondaryFireMeter)


# Called when the node enters the scene tree for the first time.
func _ready():
	primaryAudio = get_node("PrimaryWeaponAudio")
	secondaryAudio = get_node("SecondaryWeaponAudio")

