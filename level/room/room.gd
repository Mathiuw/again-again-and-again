@tool
class_name Room
extends Node2D

@export_group("Room Settings")
@export var pause_timer: bool = false
@export var music_override: AudioStream
@export var on_open_trigger_effetcts: bool = true
@export var transition_positions: Dictionary[String, Marker2D]
@export var size: Vector2i = Vector2i(640, 360):
	set(value):
		size = value
		queue_redraw()

@export var centered: bool = true:
	set(value):
		centered = value
		queue_redraw()

var navigation_region_2D: NavigationRegion2D

@export_group("Editor")
@export_subgroup("Add Transition")
@export var transition_left: bool = true
@export var transition_right: bool = true
@export var transition_up: bool = true
@export var transition_down: bool = true
@export_tool_button("Spawn Transition Markers", "Marker2D") var spawn_marker_action = spawn_transition_markers

func _draw() -> void:
	if Engine.is_editor_hint():
		if size == Vector2i.ZERO || size < Vector2i.ZERO: return
		if centered:
			draw_rect(Rect2(global_position - Vector2(size)/2, size), Color.YELLOW, false, 0.5, false)
		else:
			draw_rect(Rect2i(global_position, size).abs(), Color.YELLOW, false, 0.5, true)


#region Room Logic
func _ready() -> void:
	if Engine.is_editor_hint(): return
	
	for child in get_children():
		if child is NavigationRegion2D:
			navigation_region_2D = child

	# y sort setup
	y_sort_enabled = true
	if navigation_region_2D:
		navigation_region_2D.y_sort_enabled = true
	
	RoomManager.on_room_change.connect(on_room_change)
	SignalBus.on_enemy_die.connect(on_enemy_die)


func on_enemy_die() -> void:
	if on_open_trigger_effetcts && Globals.enemies_spawned.size() == 0:
		AudioManager.create_audio(SoundEffect.SOUND_EFFECT_TYPE.ROOM_OPEN)
		SignalBus.on_camera_shake.emit(6)
	# update navigation region if have
	if navigation_region_2D:
		await get_tree().process_frame
		if !navigation_region_2D.is_baking():
			navigation_region_2D.bake_navigation_polygon(true)


func on_room_change(_room: Room) -> void:
	# change music if music override != null
	if music_override:
		AudioManager.set_music(music_override)
	# bake the room navigation mesh (failsafe measure)
	if navigation_region_2D:
		if !navigation_region_2D.is_baking():
			navigation_region_2D.bake_navigation_polygon()
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
