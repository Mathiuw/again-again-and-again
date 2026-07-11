extends Node

@warning_ignore("unused_signal")
signal on_room_change(room: Node2D, smooth_transition: bool);

func load_room_into_world(room: PackedScene, position: Vector2 = Vector2.ZERO) -> void:
	if !room: 
		push_warning("room not found")
		return
	
	var new_room: Room = room.instantiate()
	if !new_room:
		push_warning("scene that is not a room tried to be loaded as one")
		return
	
	var world_root: Node = get_tree().get_first_node_in_group("world")
	if world_root:
		new_room.global_position = position
		world_root.add_child(new_room)
