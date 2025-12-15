class_name LoopTimer
extends Timer

@export_group("Loop Timer Settings")
@export var max_wait_time: float = 60
var loop_amount: int = 0

func _ready() -> void:
	load_game()
	
	var player: Player = get_tree().get_first_node_in_group("player")
	if  player:
		player.on_player_die.connect(func():
			end_loop()
			)
	
	RoomManager.on_room_change.connect(func(room: Room, _smooth_transition: bool):
		if  room.pause_timer:
			paused = true
		else:
			paused = false
		)
	
	# Start loop
	start(max_wait_time)
	print("Loop started")


#func _process(_delta: float) -> void:
	#print(time_left)


func save()-> Dictionary :
	var save_dict = {
		"loop_amount": loop_amount
	}
	
	return save_dict


func addTime(time: float) -> void:
	var new_time_left: float = time_left + time
	clampf(new_time_left, 0, max_wait_time)
	start(new_time_left)


func end_loop() -> void:
	loop_amount += 1
	save_game()
	get_tree().reload_current_scene()
	print("Loop ended")


func save_game() -> void :
	var save_file: FileAccess = FileAccess.open("user://savegame.save", FileAccess.WRITE)
	var save_data: Dictionary = save()
	
	var json_string = JSON.stringify(save_data)
	save_file.store_line(json_string)


func load_game() -> void:
	if !FileAccess.file_exists("user://savegame.save"):
		return # No save file found
	
	var save_file: FileAccess = FileAccess.open("user://savegame.save", FileAccess.READ)
	while save_file.get_position() < save_file.get_length():
		var json_string = save_file.get_line()
		
		var json = JSON.new()
		
		var parse_result = json.parse(json_string)
		
		if not parse_result == OK:
			push_error("JSON parse error")
			continue
		
		var node_data = json.data
		
		loop_amount = node_data["loop_amount"]
