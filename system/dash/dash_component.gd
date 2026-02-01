extends AbilityBase
class_name DashComponent

signal on_dash_start

@export var dash_speed_multiplier: float = 9.0:
	get:
		if is_dashing:
			return dash_speed_multiplier
		else:
			return 1

@export var dash_length: float = 0.075
@export var dash_cooldown: float = 0.75
@export var infinite_dash: bool = false
var _current_dash_length: float = 0.0
var can_dash: bool = true
var is_dashing: bool = false


func _process(delta: float) -> void:
	if _current_dash_length > 0:
		_current_dash_length -= delta
		_current_dash_length = maxf(_current_dash_length, 0)
		if _current_dash_length <= 0:
			stop_dash()


func start_dash() -> void:
	if !enabled: return
	if !can_dash: return
	is_dashing = true
	if infinite_dash: return
	on_dash_start.emit()
	_current_dash_length = dash_length
	print("started dash")


func stop_dash() -> void:
	is_dashing = false
	can_dash = false
	print("started dash")
	await get_tree().create_timer(dash_cooldown).timeout
	can_dash = true
	print("can dash again")
