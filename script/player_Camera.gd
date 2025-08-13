class_name PlayerCamera
extends Camera2D

@export_category("Camera Shake Settings")
@export var camera_shake:bool = true
@export var max_shake_force: float = 10
@export var shake_fade: float = 10

var _current_shake_force: float = 0.0

func trigger_shake(override_force: float = 0) -> void:
	if !camera_shake: return 
	
	if override_force > 0:
		_current_shake_force = override_force
	else:
		_current_shake_force = max_shake_force

func _ready() -> void:
	# Room change event setup
	RoomManager.on_room_change.connect(func(room):
		global_position = room.global_position;
		)

func _process(delta: float) -> void:
	# Camera shake logic
	if _current_shake_force > 0:
		_current_shake_force = lerp(_current_shake_force, 0.0,  1.0 - exp(-shake_fade * delta))
		offset = Vector2(randf_range(-_current_shake_force, _current_shake_force), randf_range(-_current_shake_force, _current_shake_force))
