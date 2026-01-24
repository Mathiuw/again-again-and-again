extends Control

var _loop_timer: LoopTimer

const DAMAGE_PARTICLE_SCENE: PackedScene = preload("uid://d4djleirskoec")

@onready var progress_bar: ProgressBar = $TimerBar/ProgressBar
@onready var damage_bar: ProgressBar = $TimerBar/ProgressBar/DamageBar

@onready var _repeat_amount: Label = $RepeatAmount
@onready var _time_bar_animation_player: AnimationPlayer = $TimerBar/ProgressBar/TimeBarAnimationPlayer

var timeout: bool = false

func _ready() -> void:
	_loop_timer = get_tree().get_first_node_in_group("loop_timer")
	if  _loop_timer:
		_loop_timer.timeout.connect(on_loop_timer_timeout)
		
		progress_bar.max_value = _loop_timer.max_wait_time
		progress_bar.value = _loop_timer.wait_time
		
		damage_bar.max_value = _loop_timer.wait_time
		damage_bar.value = _loop_timer.wait_time
		
		_repeat_amount.text = str(_loop_timer.loop_amount) 
		
		var player: Player = get_tree().get_first_node_in_group("player") as Player
		if player:
			player.on_player_damage.connect(func(damage_amount: int):
				_spawn_damage_particle(damage_amount)
				)
	else:
		push_error("HUD: cant find loop timer")
		return
	
	RoomManager.on_room_change.connect(on_room_change)



func _process(_delta: float) -> void:
	if !timeout:
		_set_progress_bar(_loop_timer.time_left)
	
	# damage bar value lerp
	damage_bar.value = lerp(damage_bar.value, progress_bar.value, 1.0 - exp(-5 * get_process_delta_time()))


func on_room_change(room:Room, _smooth_trasition) -> void:
	if  room.pause_timer:
		_time_bar_animation_player.play("timer_paused")
	else:
		_time_bar_animation_player.play("RESET")


func on_loop_timer_timeout() -> void:
	timeout = true
	_set_progress_bar(0)


func _set_progress_bar(value: float) -> void:
	if _loop_timer:
		progress_bar.value = value


func _spawn_damage_particle(damage_amount: int):
	var damage_particle = DAMAGE_PARTICLE_SCENE.instantiate()
	damage_particle.damage_amount = damage_amount
	add_child(damage_particle)
	
	var spawn_position: Vector2 = progress_bar.global_position
	spawn_position.x = progress_bar.global_position.x + (progress_bar.size.x * (progress_bar.value/ progress_bar.max_value))
	damage_particle.global_position = spawn_position
