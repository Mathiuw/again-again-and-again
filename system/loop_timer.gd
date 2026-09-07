extends Timer
class_name LoopTimer

@export_group("Loop Timer Settings")
@export var max_wait_time: float = 60
var loop_amount: int = 0

func _ready() -> void:
	load_game()
	
	var player: TopDownController2d = get_tree().get_first_node_in_group("player")
	if  player:
		player.on_player_die.connect(on_player_die)
	
	SignalBus.on_room_change.connect(on_room_change_started)

	# Start loop
	start(max_wait_time)
	print("Loop started")


#func _process(_delta: float) -> void:
	#print(time_left)


func on_player_die() -> void:
	end_loop()


func on_room_change_started(room: Room) -> void:
	if  room.pause_timer:
		paused = true
	else:
		paused = false


func addTime(time: float) -> void:
	var new_time_left: float = time_left + time
	new_time_left = clampf(new_time_left, 0, max_wait_time)
	start(new_time_left)


func remove_time(time: float) -> float:
	var new_time_left: float = time_left - time
	new_time_left = clampf(new_time_left, 0, max_wait_time)
	print(new_time_left)
	
	#if new_time_left > time_left: return new_time_left
	
	if new_time_left == 0:
		timeout.emit()
		return new_time_left

	start(new_time_left)
	return new_time_left


func end_loop() -> void:
	loop_amount += 1
	save_game()
	Globals.reset_enemies_cleared()
	get_tree().reload_current_scene()
	print("Loop ended")


func save()-> Dictionary :
	var save_dict: Dictionary = {
		"loop_amount": loop_amount
	}
	
	return save_dict


func save_game() -> void :
	var save_file: FileAccess = FileAccess.open("user://savegame.save", FileAccess.WRITE)
	var save_data: Dictionary = save()
	
	var json_string: String = JSON.stringify(save_data)
	save_file.store_line(json_string)


func load_game() -> void:
	if !FileAccess.file_exists("user://savegame.save"):
		return # No save file found
	
	var save_file: FileAccess = FileAccess.open("user://savegame.save", FileAccess.READ)
	while save_file.get_position() < save_file.get_length():
		var json_string: String = save_file.get_line()
		var json: JSON = JSON.new()
		var parse_result: Error = json.parse(json_string)
		
		if parse_result != OK:
			push_error("JSON parse error")
			continue
		
		var node_data: Variant = json.data
		
		loop_amount = node_data["loop_amount"]
