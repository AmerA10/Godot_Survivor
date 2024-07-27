extends Area2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func OnAreaEntered(var areaId:RID, var otherArea:Area2D, var areaShapeIndex:int, var localShapeIndex:int):
	
	var proj = otherArea.get_owner()
	
	if(is_instance_valid(proj)):
		proj.queue_free()
		return


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass
