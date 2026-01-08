extends RayCast2D
class_name TargetRaycastCheck2D

signal on_target_in_sight(target: Object)
var target_on_sight: bool = false
var target: Node2D


func _ready() -> void:
	target = get_tree().get_first_node_in_group("player")
	if  !target:
		push_error("Couldnt find player")


func _on_timer_timeout() -> void:
	await get_tree().physics_frame
	target_position = target.global_position - global_position
	force_raycast_update()
	if get_collider() == target:
		on_target_in_sight.emit(get_collider())
		print("Player on sight")
