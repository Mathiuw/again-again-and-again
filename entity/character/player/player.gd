class_name Player
extends CharacterBody2D

signal on_player_die
signal on_player_damage(damageAmount: int)

const SPEED: float = 115.0

@onready var _animated_sprite_2d: AnimatedSprite2D = $AnimatedSprite2D
@onready var _weapon: Weapon = $Weapon
@onready var _interact_component: InteractComponent = $InteractComponent
@onready var _roll_component: DashComponent = $DashComponent
var _loop_timer: LoopTimer


# knockback variables
var knockback: Vector2 = Vector2.ZERO
var knockback_timer: float = 0.0

# damage variables
@export var invencible_frames_amount: int = 15
var can_take_damage: bool = true


func _ready() -> void:
	# Hide and confine mouse
	#Input.mouse_mode = Input.MOUSE_MODE_CONFINED_HIDDEN
	
	_loop_timer = get_tree().get_first_node_in_group("loop_timer")
	if _loop_timer:
		_loop_timer.timeout.connect(on_loop_timer_timeout)
	else:
		push_warning("No loop timer found")
	
	SignalBus.on_dialog_enter.connect(on_dialogue_enter)
	SignalBus.on_dialog_end.connect(on_dialogue_exit)


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


func _physics_process(delta: float) -> void:
	if knockback_timer > 0:
		# knockback logic
		velocity = knockback
		knockback_timer -= delta
		if knockback_timer <= 0:
			knockback = Vector2.ZERO
			knockback_timer = 0
	else:
		# Move logic
		var multiplier: float = _roll_component.dash_speed_multiplier
		var desired_direction: Vector2 = Input.get_vector("move left", "move right", "move up", "move down");
		velocity = desired_direction * (SPEED * multiplier)
	# apply movement
	move_and_slide()


func set_move_state(state: bool) -> void:
	set_physics_process(state)
	set_process(state)
	_set_player_idle()


func on_dialogue_enter(_dialogue_steps: Array[DialogueBase]) -> void:
	set_move_state(false)

func on_dialogue_exit() -> void:
	await get_tree().create_timer(0.1).timeout
	set_move_state(true)

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
	if !can_take_damage: return
	
	# apply camera shake
	SignalBus.on_camera_shake.emit(3)
	
	AudioManager.create_audio(SoundEffect.SOUND_EFFECT_TYPE.HURT)
	
	# apply damage to loop timer if has one
	if _loop_timer:
		var result_damage: float = _loop_timer.remove_time(damageAmount)
		on_player_damage.emit(damageAmount)
		
		if result_damage == 0:
			return
		
		# activate invencible frames
		can_take_damage = false
		print("Invicibility frames start")
		# invencible tween
		var invencible_tween = create_tween().set_trans(Tween.TRANS_SINE)
		invencible_tween.tween_property($AnimatedSprite2D, "modulate:a", 0.1, invencible_frames_amount/4.0)
		invencible_tween.chain().tween_property($AnimatedSprite2D, "modulate:a", 1.0, invencible_frames_amount/4.0)
		invencible_tween.chain().chain().tween_property($AnimatedSprite2D, "modulate:a", 0.1, invencible_frames_amount/4.0)
		invencible_tween.chain().chain().chain().tween_property($AnimatedSprite2D, "modulate:a", 1.0, invencible_frames_amount/4.0).finished.connect(
			on_invencible_frames_over)


func on_invencible_frames_over() -> void:
	can_take_damage = true
	print("Invicibility frames over")


# On loop end function
func on_loop_timer_timeout() -> void:
	const UI_FADE = preload("uid://mwaxn6ft2wpi")
	
	can_take_damage = false
	set_move_state(false)
	_animated_sprite_2d.play("die")
	await get_tree().create_timer(2).timeout
	
	var new_fade: UIFade = UI_FADE.instantiate()
	new_fade.fade_settings.destroy_on_end = false
	add_child(new_fade)
	await new_fade.on_fade_end
	
	on_player_die.emit()


func apply_knockback(direction: Vector2, force: float, knockback_duration: float) -> void:
	knockback = direction * force
	knockback_timer = knockback_duration
