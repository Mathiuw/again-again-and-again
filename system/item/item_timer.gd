class_name ItemTimer
extends ItemBase

@export_group("Item Timer Settings")
@export var time_value_base: float = 10

func on_item_pickup(scene_tree: SceneTree) -> void:
	var loop_timer:LoopTimer = scene_tree.get_first_node_in_group("loop_timer")
	if loop_timer:
		add_time_to_loop(loop_timer)

func add_time_to_loop(timer: LoopTimer) -> void:
	timer.addTime(time_value_base)
	pass

#TODO: calculate the time to give based on the current time value
func calculate_time_to_give() -> float:
	return 0 
