class_name Room
extends Node2D

@export_group("Room Settings")
@export var initial_room: bool = false
@export var pause_timer: bool = false
@export var music_override: AudioStream
@export var navigation_region_2D: NavigationRegion2D
@export var enemies_root: Node = self

signal on_no_enemies_left

func _ready() -> void:
	if enemies_root == null:
		enemies_root = self
	
	RoomManager.on_room_change.connect(func(room: Room, _smooth_transition: bool):
		if room != self:
			set_room_state(false)
		else:
			set_room_state(true)
			if music_override:
				AudioManager.current_music = music_override
			
			if navigation_region_2D:
				navigation_region_2D.bake_navigation_polygon(true)
		)
	
	#call_deferred("check_initial_room")
	
	# check initial room at the end of the frame
	check_initial_room.call_deferred()
	
	if get_enemy_count(false) == 0:
		return
	
	for node in enemies_root.get_children(true):
		if node.is_in_group("enemy"):
			for child in node.get_children():
				if child is Health && !child.dead:
					child.on_die.connect(func():
						get_enemy_count()
						
						# update navigation region if have
						if navigation_region_2D:
							await get_tree().process_frame
							navigation_region_2D.bake_navigation_polygon(true)
						)


func check_initial_room() -> void:
		if  initial_room: 
			RoomManager.on_room_change.emit(self, false)


func open_room_doors(open_effects: bool = true) -> void:
	for node in get_children():
		if node is Door:
			node.set_door_open_state(true)
	
	if open_effects:
		AudioManager.create_audio(SoundEffect.SOUND_EFFECT_TYPE.ROOM_OPEN)
		SignalBus.on_camera_shake.emit(6)


func get_enemy_count(open_effects: bool = true) -> int:
	var count: int = 0
	for node in enemies_root.get_children():
		if node.is_in_group("enemy"):
			for child in node.get_children():
				if child is Health && !child.dead:
					count =+ 1
	
	if count == 0:
		open_room_doors(open_effects)
		on_no_enemies_left.emit()
	
	return count

func set_room_state(state: bool) -> void:
	for node: Node in get_children():
		if  state:
			node.process_mode = Node.PROCESS_MODE_INHERIT 
		else:
			node.process_mode = Node.PROCESS_MODE_WHEN_PAUSED 
