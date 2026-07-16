extends Camera2D
class_name TopDownCamera2D

static var smooth_transition: bool = true

@export var move_time: float = 0.3
@export var camera_shake: bool = true
@export var max_shake_force: float = 10
@export var shake_fade: float = 10

var _current_shake_force: float = 0.0

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


func on_room_change(room: Room) -> void:
	var tween: Tween = get_tree().create_tween()
	tween.tween_property($'.', "position", room.global_position, move_time).set_trans(Tween.TRANS_SINE)
	await tween.finished
	RoomManager.clear_previous_loaded_rooms()
	RoomManager.set_current_loaded_room_state.call_deferred(Node.PROCESS_MODE_INHERIT)
