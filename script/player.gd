class_name Player
extends CharacterBody2D

const SPEED: float = 150.0

@onready var _animated_sprite_2d: AnimatedSprite2D = $AnimatedSprite2D
@onready var _weapon: Weapon = $Weapon
@onready var _interact_component: InteractComponent = $InteractComponent
@onready var _roll_component: Roll = $RollComponent
var _loop_timer: Timer

signal on_player_die
signal on_player_damage(damageAmount: int)

func _ready() -> void:
	# Hide and confine mouse
	#Input.mouse_mode = Input.MOUSE_MODE_CONFINED_HIDDEN
	
	# On loop end function
	_loop_timer = get_tree().get_first_node_in_group("loop_timer")
	if _loop_timer:
		_loop_timer.timeout.connect(func():
			set_move_state(false)
			_animated_sprite_2d.play("die")
			await get_tree().create_timer(3).timeout
			on_player_die.emit()
			)
	else:
		push_warning("No loop timer found")
	
	SignalBus.on_dialog_enter.connect(func(_dialogue_steps: Array[DialogueBase]):
		set_move_state(false)
		)
	
	SignalBus.on_dialog_end.connect(func():
		await get_tree().create_timer(0.1).timeout
		set_move_state(true)
		)

func _process(_delta: float) -> void:
	# set player animations
	if velocity.length() > 0:
		_set_player_animation(velocity.normalized())
	else:
		_set_player_idle()
	
	# shoot input
	var shoot_vector: Vector2 = Input.get_vector("shoot left", "shoot right", "shoot up", "shoot down")
	if shoot_vector :
		_weapon.shoot(shoot_vector, self)
	
	# interact/roll input
	if Input.is_action_just_pressed("interact"):
		if !_interact_component.try_to_interact():
			_roll_component.start_dash()

func _physics_process(_delta: float) -> void:
	# Move logic
	var multiplier: float = _roll_component.dash_speed_multiplier
	var desired_direction: Vector2 = Input.get_vector("move left", "move right", "move up", "move down");
	velocity = desired_direction * (SPEED * multiplier)
	move_and_slide();

func set_move_state(state: bool) -> void:
	set_physics_process(state)
	set_process(state)
	_set_player_idle()

func _set_player_animation(desiredDirection: Vector2) -> void:
	#TODO implement player shoot animation
	if desiredDirection.y == 0 && desiredDirection.x > 0:
		_animated_sprite_2d.play("walk_right")
	elif desiredDirection.y == 0 && desiredDirection.x < 0:
		_animated_sprite_2d.play("walk_left")
	elif desiredDirection.y > 0:
		_animated_sprite_2d.play("walk_front")
	elif desiredDirection.y < 0:
		_animated_sprite_2d.play("walk_back")

func _set_player_idle() -> void:
	match _animated_sprite_2d.animation:
		"walk_back":
			_animated_sprite_2d.play("idle_back")
		"walk_front":
			_animated_sprite_2d.play("idle_front")
		"walk_left":
			_animated_sprite_2d.play("idle_left")
		"walk_right":
			_animated_sprite_2d.play("idle_right")

func damage(damageAmount: int) -> void:
	SignalBus.on_camera_shake.emit(3)
	
	if _loop_timer && _loop_timer.time_left > 0:
		var new_loop_timer_time: float = _loop_timer.time_left - damageAmount
		
		if new_loop_timer_time > _loop_timer.time_left: return
		
		new_loop_timer_time = clampf(new_loop_timer_time, 0.1, new_loop_timer_time)
		_loop_timer.start(new_loop_timer_time)
		
		on_player_damage.emit(damageAmount)
