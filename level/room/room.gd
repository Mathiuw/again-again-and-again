@tool
class_name Room
extends Node2D

@export_group("Room Settings")
@export var id: StringName = "000"
@export var initial_room: bool = false
@export var pause_timer: bool = false
@export var music_override: AudioStream
@export var transition_positions: Dictionary[String, Marker2D]
var navigation_region_2D: NavigationRegion2D
var enemies_root: Node2D

@export_group("Add Transition")
@export var transition_left: bool = true
@export var transition_right: bool = true
@export var transition_up: bool = true
@export var transition_down: bool = true
@export_tool_button("Spawn Transition Markers", "Marker2D") var spawn_marker_action = spawn_transition_markers

signal on_no_enemies_left

#region Room Logic
func _ready() -> void:
	if Engine.is_editor_hint(): return
	
	enemies_root = self
	
	for child in get_children():
		if child is NavigationAgent2D:
			navigation_region_2D = child
			enemies_root = child
	
	# y sort setup
	y_sort_enabled = true
	if navigation_region_2D:
		navigation_region_2D.y_sort_enabled = true
	if enemies_root:
		enemies_root.y_sort_enabled = true
	
	RoomManager.on_room_change.connect(on_room_change)
	# check initial room at the end of the frame
	check_initial_room.call_deferred()
	
	if get_enemy_count(false) == 0:
		return
	
	# on enemy die function connect
	for node in enemies_root.get_children(true):
		if node.is_in_group("enemy"):
			for child in node.get_children():
				if child is Health && !child.dead:
					child.on_die.connect(on_enemy_die)


func check_initial_room() -> void:
		if  initial_room: 
			RoomManager.on_room_change.emit(self, false)


func open_room_doors(open_effects: bool = true) -> void:
	var door_count: int = 0
	
	for node in get_children():
		if node is Door:
			node.set_door_open_state(true)
			door_count += 1
	
	if open_effects && door_count > 0:
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


func on_enemy_die() -> void:
	get_enemy_count()
	# update navigation region if have
	if navigation_region_2D:
		await get_tree().process_frame
		if !navigation_region_2D.is_baking():
			navigation_region_2D.bake_navigation_polygon(true)


func on_room_change(room: Room, _smooth_transition: bool) -> void:
	if room != self:
		set_room_state.call_deferred(false)
	else:
		set_room_state.call_deferred(true)
		if music_override:
			AudioManager.set_music(music_override)
		
		if navigation_region_2D:
			if !navigation_region_2D.is_baking():
				navigation_region_2D.bake_navigation_polygon()


func set_room_state(state: bool) -> void:
	for node: Node in get_children():
		if  state:
			node.process_mode = Node.PROCESS_MODE_INHERIT 
		else:
			node.process_mode = Node.PROCESS_MODE_DISABLED
#endregion


#region Spawn Transition Marker Inspector buttom
func spawn_transition_markers() -> void:
	if transition_left:
		var marker_1: Marker2D = Marker2D.new()
		marker_1.name = "TransitionLeft"
		
		transition_positions.get_or_add("left", marker_1)
		
		add_child(marker_1, true)
		marker_1.owner = get_tree().edited_scene_root
	
	if transition_right:
		var marker_2: Marker2D = Marker2D.new()
		marker_2.name = "TransitionRight"
		
		transition_positions.get_or_add("right", marker_2)
		
		add_child(marker_2, true)
		marker_2.owner = get_tree().edited_scene_root
	
	if transition_up:
		var marker_3: Marker2D = Marker2D.new()
		marker_3.name = "TransitionUp"
		
		transition_positions.get_or_add("up", marker_3)
		
		add_child(marker_3, true)
		marker_3.owner = get_tree().edited_scene_root
	
	if transition_down:
		var marker_4: Marker2D = Marker2D.new()
		marker_4.name = "TransitionDown"
		
		transition_positions.get_or_add("down", marker_4)
		
		add_child(marker_4, true)
		marker_4.owner = get_tree().edited_scene_root
	
	notify_property_list_changed()
#endregion
