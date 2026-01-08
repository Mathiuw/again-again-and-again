extends Timer
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
var can_dash: bool = true
var is_dashing: bool = false


func _ready() -> void:
	wait_time = dash_length


func start_dash() -> void:
	if !can_dash: 
		return
	is_dashing = true
	if infinite_dash:
		return
	start()
	on_dash_start.emit()


func stop_dash() -> void:
	is_dashing = false
	can_dash = false
	await get_tree().create_timer(dash_cooldown).timeout
	can_dash = true

 
func _on_timeout() -> void:
	stop_dash()
