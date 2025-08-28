extends Control

@onready var progress_bar: ProgressBar = $TimerBar/ProgressBar

@onready var _loop_timer: Timer
@onready var _player_health: Health
@onready var _repeat_amount: Label = $RepeatAmount
@onready var _time_bar_animation_player: AnimationPlayer = $TimerBar/ProgressBar/TimeBarAnimationPlayer

func _ready() -> void:
	_loop_timer = get_tree().get_first_node_in_group("loop_timer")
	if  _loop_timer:
		progress_bar.max_value = _loop_timer.wait_time
		_repeat_amount.text = str(_loop_timer.loop_amount) 
	else:
		push_error("HUD: cant find loop timer")
		return
		
		RoomManager.on_room_change.connect(func(room:Room, _smooth_trasition):
			if  room.pause_timer:
				_time_bar_animation_player.play("timer_paused")
			else:
				_time_bar_animation_player.play("RESET")
			)

func _process(_delta: float) -> void:
	if _loop_timer:
		progress_bar.value = _loop_timer.time_left
