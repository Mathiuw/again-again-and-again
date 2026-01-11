extends AttackBase

enum ShootType {Random, Line, LineTargeted, Targeted } 

@export var shoot_type:ShootType = ShootType.Random
@export var lynx_tail_side_scene: PackedScene
@export var spawn_markers:Array[TargetMarker2D]
var target: Node2D

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	target = get_tree().get_first_node_in_group("player")
	if  !target:
		push_error("Couldnt find player")


func attack() -> void:
	match shoot_type:
		ShootType.Random:
			shoot_random()
		ShootType.Line:
			push_warning("Not implemented")
		ShootType.LineTargeted:
			push_warning("Not implemented")
		ShootType.Targeted:
			shoot_targeted()


func shoot_random() -> void:
	print("Shoot random")
	
	var index_to_spawn: int = randi_range(0,spawn_markers.size()-1)
	#weapons[index_to_shoot].shoot(weapons[index_to_shoot].transform.x, self)
	var new_lynx_tail: LynxTail = lynx_tail_side_scene.instantiate()
	new_lynx_tail.global_position = spawn_markers[index_to_spawn].global_position
	add_child(new_lynx_tail)
	
	await new_lynx_tail.on_tail_attack_end
	
	new_lynx_tail.queue_free()
	on_attack_end.emit()
	


func shoot_targeted() -> void:
	print("Shoot targeted")
	
	var targeted_markers: Array[TargetMarker2D]
	
	for marker in spawn_markers:
		if marker.target_area.target:
			targeted_markers.push_back(marker)
	
	if targeted_markers.size() == 0:
		print("no weapons targeted, shooting random!")
		shoot_random()
		return
	
	var index_to_shoot: int = randi_range(0,targeted_markers.size()-1)
	var new_lynx_tail: LynxTail = lynx_tail_side_scene.instantiate()
	new_lynx_tail.global_position = targeted_markers[index_to_shoot].global_position
	add_child(new_lynx_tail)
	
	await new_lynx_tail.on_tail_attack_end
	
	new_lynx_tail.queue_free()
	on_attack_end.emit()
