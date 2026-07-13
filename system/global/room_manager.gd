extends Node

enum LoadDirection {
	NONE,
	LEFT,
	RIGHT,
	UP,
	DOWN
}


signal on_room_change_started(room: Room, smooth_camera: bool);
signal on_room_change_ended(room: Room)

var current_room_loaded : Room

func _ready() -> void:
	on_room_change_started.connect(func(_room: Room, _smooth_camera: bool):
		pass
	)

	on_room_change_ended.connect(func(room: Room):
		if current_room_loaded:
			current_room_loaded.queue_free()
		current_room_loaded = room
	)


func set_current_loaded_room_state(state: Node.ProcessMode):
	if !current_room_loaded:
		push_warning("There is no valid room loaded")
		return

	current_room_loaded.process_mode = state


func load_room_into_world_from_scene(room: PackedScene, load_direction: LoadDirection, room_activated: bool = true) -> void:
	if !room: 
		push_warning("room not found")
		return
	
	var new_room: Room = room.instantiate()
	if !new_room:
		push_warning("scene that is not a room tried to be loaded as one")
		return

	if !room_activated:
		new_room.process_mode = Node.PROCESS_MODE_DISABLED

	load_room_into_world_from_node(new_room, load_direction)


func load_room_into_world_from_node(room_to_load: Node, load_direction: LoadDirection) -> void:
	var world_root: Node2D = get_tree().get_first_node_in_group("world")
	if world_root:
		room_to_load.global_position = get_desired_load_position(room_to_load, load_direction)
		world_root.add_child(room_to_load)
		on_room_change_started.emit(room_to_load, true)
	else:
		push_error("World root not found")


func get_desired_load_position(room_to_load: Room, load_direction: LoadDirection) -> Vector2:
	var desired_position: Vector2 = current_room_loaded.global_position if current_room_loaded else Vector2.ZERO
	match load_direction:
		LoadDirection.UP:
			desired_position.y -= float(room_to_load.size.y)/2 if room_to_load.centered else float(room_to_load.size.y)
		LoadDirection.DOWN:
			desired_position.y += float(room_to_load.size.y)/2 if room_to_load.centered else float(room_to_load.size.y)
		LoadDirection.LEFT:
			desired_position.x -= float(room_to_load.size.x)/2 if room_to_load.centered else float(room_to_load.size.x) 
		LoadDirection.RIGHT:
			desired_position.x += float(room_to_load.size.x)/2 if room_to_load.centered else float(room_to_load.size.x) 
	
	return desired_position
