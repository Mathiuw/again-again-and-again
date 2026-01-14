extends AttackBase
class_name AttackLynxRanged

enum ShootType {Random, Line, LineTargeted, Targeted } 

@export var shoot_type:ShootType = ShootType.Random
@export var lynx_tail_side_scene: PackedScene
var spawn_markers:Array[Marker2D]


func attack() -> void:
	if spawn_markers.size() == 0:
		push_warning("spawn markers size is 0!")
		return
	
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
	var new_lynx_tail: LynxTail = spawn_tail(spawn_markers[index_to_spawn].global_position)
	
	await new_lynx_tail.on_tail_attack_end
	
	new_lynx_tail.queue_free()
	on_attack_end.emit()


func shoot_targeted() -> void:
	print("Shoot targeted")
	
	var targeted_markers: Array[Marker2D]
	
	for marker in spawn_markers:
		if marker.get_parent().target:
			targeted_markers.push_back(marker)
	
	if targeted_markers.size() == 0:
		print("no weapons targeted, shooting random!")
		shoot_random()
		return
	
	var index_to_shoot: int = randi_range(0,targeted_markers.size()-1)
	var new_lynx_tail: LynxTail = spawn_tail(targeted_markers[index_to_shoot].global_position)
	
	await new_lynx_tail.on_tail_attack_end
	
	new_lynx_tail.queue_free()
	on_attack_end.emit()


func spawn_tail(spawn_global_position: Vector2) -> LynxTail:
	var new_lynx_tail: LynxTail = lynx_tail_side_scene.instantiate()
	get_parent().get_parent().add_child(new_lynx_tail)
	new_lynx_tail.attack_root = self
	new_lynx_tail.global_position = spawn_global_position
	return new_lynx_tail
