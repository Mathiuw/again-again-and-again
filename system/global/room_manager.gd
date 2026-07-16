extends Node

enum LoadDirection {
	NONE,
	LEFT,
	RIGHT,
	UP,
	DOWN
}

signal on_room_change(room: Room)

static var current_room_loaded : Room
static var previous_room_loaded: Room


func set_current_loaded_room_state(state: Node.ProcessMode) -> void:
	if !current_room_loaded:
		push_warning("There is no valid room loaded")
		return
	current_room_loaded.process_mode = state


func clear_previous_loaded_rooms() -> void:
	if previous_room_loaded:
		previous_room_loaded.queue_free()


func load_room_into_world_from_scene(room_uid: StringName, load_direction: LoadDirection, room_activated: bool = true) -> Room:
	if room_uid.is_empty(): 
		push_warning("room UID not valid")
		return
	
	var room_packed_scene: PackedScene = load(room_uid)
	await get_tree().process_frame
	var room_instance: Room = room_packed_scene.instantiate()
	load_room_into_world_from_node(room_instance, load_direction, room_activated)
	return room_instance


func load_room_into_world_from_node(room_instance: Node, load_direction: LoadDirection, room_activated: bool = true) -> void:
	var world_root: Node2D = get_tree().get_first_node_in_group("world")
	if world_root:
		room_instance.global_position = get_desired_load_position(room_instance, load_direction)
		world_root.add_child(room_instance)
		if current_room_loaded:
			previous_room_loaded = current_room_loaded
		current_room_loaded = room_instance
		if !room_activated:
			set_current_loaded_room_state.call_deferred(Node.PROCESS_MODE_DISABLED)
		on_room_change.emit(room_instance)
	else:
		push_error("World root not found")


func get_desired_load_position(room_to_load: Room, load_direction: LoadDirection) -> Vector2:
	var current_room_size: Vector2 = Vector2(current_room_loaded.size) if current_room_loaded else Vector2.ZERO
	var desired_position: Vector2 = current_room_loaded.global_position if current_room_loaded else Vector2.ZERO
	
	match load_direction:
		LoadDirection.UP:
			desired_position.y -= current_room_size.y/2 + float(room_to_load.size.y)/2 if room_to_load.centered else float(room_to_load.size.y)
		LoadDirection.DOWN:
			desired_position.y += current_room_size.y/2 + float(room_to_load.size.y)/2 if room_to_load.centered else float(room_to_load.size.y)
		LoadDirection.LEFT:
			desired_position.x -= current_room_size.x/2 + float(room_to_load.size.x)/2 if room_to_load.centered else float(room_to_load.size.x) 
		LoadDirection.RIGHT:
			desired_position.x += current_room_size.x/2 + float(room_to_load.size.x)/2 if room_to_load.centered else float(room_to_load.size.x) 
		LoadDirection.NONE:
			pass	
	
	return desired_position
