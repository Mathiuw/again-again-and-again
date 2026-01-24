extends Camera2D
class_name GameCamera

@export var camera_shake: bool = true
@export var max_shake_force: float = 10
@export var shake_fade: float = 10

var _current_shake_force: float = 0.0
var current_room: Room


func _ready() -> void:
	SignalBus.on_camera_shake.connect(trigger_shake)
	RoomManager.on_room_change.connect(on_room_change)

func _process(delta: float) -> void:
	# Camera shake logic
	if _current_shake_force > 0:
		_current_shake_force = lerp(_current_shake_force, 0.0,  1.0 - exp(-shake_fade * delta))
		offset = Vector2(randf_range(-_current_shake_force, _current_shake_force), randf_range(-_current_shake_force, _current_shake_force))


func trigger_shake(override_force: float = 0) -> void:
	if !camera_shake: return 
	
	if override_force > 0:
		_current_shake_force = override_force
	else:
		_current_shake_force = max_shake_force


func on_room_change(room: Room, smooth_transition: bool) -> void:
	position_smoothing_enabled = smooth_transition
	global_position = room.global_position;
