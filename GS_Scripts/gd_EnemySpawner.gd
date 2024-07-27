extends Node2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"

export(PackedScene) var EnemyScene1
export(PackedScene) var EnemyScene2
export(PackedScene) var EnemyScene3
export(PackedScene) var EnemyScene4

var level:int

export(Resource) var spawnLoc1
export(Resource) var spawnLoc2
export(Resource) var spawnLoc3

export(int) var spawnTime:int = 5

export(int) var maxEnemies:int = 4
export(int) var maxSpawnableEnemies:int = 10

var SpawnPercentage = [[100,200,0,], [60,100,200,200], [30,60,90,100], [10,30,70,100]]

var playerTarget

var enemies:Array

# Called when the node enters the scene tree for the first time.
func _ready():
	playerTarget = self.owner.get_node("Player")
	playerTarget.SetEnemySpawner(self)
	
	var timer:Timer = get_node("Timer")
	timer.wait_time = spawnTime
	spawnLoc1 = get_node("./Loc1")
	spawnLoc2 = get_node("./Loc2")
	spawnLoc3 = get_node("./Loc3")
	
	timer.start()
	randomize()

func SpawnEnemy():
	var spawnPos:Vector2
	
	
	if(randf() <= 0.5):
		spawnPos = Vector2(rand_range(spawnLoc1.global_position.x, spawnLoc2.global_position.x), spawnLoc1.global_position.y)
	else:
		spawnPos = Vector2(spawnLoc1.global_position.x,rand_range(spawnLoc1.global_position.y, spawnLoc3.global_position.y))
	
	var otherSpawnSide:int
	if(randf() <= 0.5):
		otherSpawnSide = 1
	else:
		otherSpawnSide = -1
	
	spawnPos.x *= otherSpawnSide
	
	if(randf() <= 0.5):
		otherSpawnSide = 1
	else:
		otherSpawnSide = -1
	spawnPos.y *= otherSpawnSide

	var chance:int = abs(randi() % 101)
	
	var enemy
	
	if(chance <= SpawnPercentage[level][0] && chance < SpawnPercentage[level][1]):
		enemy = EnemyScene1.instance()
	elif(chance <= SpawnPercentage[level][1] && chance < SpawnPercentage[level][2]):
		enemy = EnemyScene2.instance()
	elif(chance <= SpawnPercentage[level][2] && chance < SpawnPercentage[level][3]):
		enemy = EnemyScene3.instance()
	elif(chance <= SpawnPercentage[level][3]):
		enemy = EnemyScene4.instance()
	else:
		enemy = null
	
	if(enemy != null):
		enemy.SetUpEnemy(self,playerTarget)
		enemy.position = spawnPos
		enemies.append(enemy)
		add_child(enemy)
	
func GetClosestEnemyTo(var position:Vector2):
	if(enemies.size() <= 0):
		return null
	var closest = enemies[0]
	for enemy in enemies:
		if(abs((enemy.position - position).length_squared()) < abs((closest.position - position).length_squared())):
			closest = enemy
	return closest			

func RemoveEnemy(var enemy):
	enemies.remove(enemies.find(enemy,0))
func OnTimerTimeout():
	if(enemies.size() < maxEnemies):
		SpawnEnemy()
func GetEnemies():
	return enemies
func MoveEnemiesBack():
	for enemy in enemies:
		enemy.MoveBack()

func UpLevel():
	self.level += 1
	self.maxEnemies += 1		

	if(level >= 3):
		level = 3
	if(maxEnemies > maxSpawnableEnemies):
		self.maxEnemies = maxSpawnableEnemies


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
