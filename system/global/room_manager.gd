extends Node

enum LoadDirection {
	NONE,
	LEFT,
	RIGHT,
	UP,
	DOWN
}


func set_current_loaded_room_state(state: Node.ProcessMode) -> void:
	if !Globals.current_room_loaded:
		push_warning("There is no valid room loaded")
		return
	Globals.current_room_loaded.process_mode = state


func clear_previous_loaded_rooms() -> void:
	if Globals.previous_room_loaded:
		Globals.previous_room_loaded.queue_free()


func is_enemy_cleared_from_current_room(enemy_name: String) -> bool:
	var data: Variant = JSON.parse_string(Globals.room_saves)
	if data == null : return false

	if data is Dictionary:
		if data.has(Globals.current_room_loaded.name):
			var enemies_cleared: Array = data[Globals.current_room_loaded.name].enemies_cleared
			return enemies_cleared.has(enemy_name)
	
	return false


func add_enemy_cleared_to_current_room(enemy_name: String) -> void:
	var data: Variant = JSON.parse_string(Globals.room_saves)
	if data == null : return

	if data is not Dictionary: 
		data = {}

	if data.has(Globals.current_room_loaded.name):
		var new_enemies_cleared: Array = data[Globals.current_room_loaded.name].enemies_cleared
		new_enemies_cleared.push_front(enemy_name)
		data[Globals.current_room_loaded.name].enemies_cleared = new_enemies_cleared
	else:
		data[Globals.current_room_loaded.name] = { "enemies_cleared" : [enemy_name] }
	
	Globals.room_saves = JSON.stringify(data, "\t")


func load_room_into_world_from_scene(room_uid: StringName, load_direction: LoadDirection, room_activated: bool = true) -> Room:
	if room_uid.is_empty(): 
		push_warning("room UID not valid")
		return
	
	var room_packed_scene: PackedScene = load(room_uid)
	await get_tree().process_frame
	var room_instance: Room = room_packed_scene.instantiate()

	load_room_into_world_from_node(room_instance, load_direction, room_activated)
	return room_instance


func load_room_into_world_from_node(room_instance: Node2D, load_direction: LoadDirection, room_activated: bool = true) -> void:
	var world_root: Node2D = get_tree().get_first_node_in_group("world")
	
	if world_root:
		room_instance.global_position = get_desired_load_position(room_instance as Room, load_direction)
		world_root.add_child(room_instance)

		if !room_activated:
			set_current_loaded_room_state.call_deferred(Node.PROCESS_MODE_DISABLED)

		if Globals.current_room_loaded:
			Globals.previous_room_loaded = Globals.current_room_loaded
		Globals.current_room_loaded = room_instance

		SignalBus.on_room_change.emit(room_instance)
	else:
		push_error("World root not found")


func get_desired_load_position(room_to_load: Room, load_direction: LoadDirection) -> Vector2:
	var current_room_size: Vector2 = Vector2(Globals.current_room_loaded.size) if Globals.current_room_loaded else Vector2.ZERO
	var desired_position: Vector2 = Globals.current_room_loaded.global_position if Globals.current_room_loaded else Vector2.ZERO
	
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
