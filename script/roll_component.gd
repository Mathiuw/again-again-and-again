extends Timer
class_name Roll

@export var dash_speed_multiplier: float = 9.0:
	get:
		if is_dashing:
			return dash_speed_multiplier
		else:
			return 1

@export var dash_length: float = 0.075
@export var dash_cooldown: float = 0.75

var can_dash: bool = true
var is_dashing: bool = false

@onready var _cooldown_timer: Timer = $CooldownTimer


func _ready() -> void:
	wait_time = dash_length
	_cooldown_timer.wait_time = dash_cooldown

func start_dash() -> void:
	if !can_dash: 
		return
	is_dashing = true
	start()


func _on_timeout() -> void:
	is_dashing = false
	can_dash = false
	_cooldown_start()

func _cooldown_start() -> void:
	_cooldown_timer.start()


func _on_cooldown_timer_timeout() -> void:
	can_dash = true
