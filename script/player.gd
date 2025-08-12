class_name Player
extends CharacterBody2D

const SPEED: float = 150.0

@onready var _animated_sprite_2d: AnimatedSprite2D = $AnimatedSprite2D
@onready var _health: Health = %Health
@onready var _weapon: Weapon = $Weapon

func _ready() -> void:
	# Die function
	_health.on_die.connect(func():
		set_physics_process(false)
		set_process(false)
		_animated_sprite_2d.play("die")
		await get_tree().create_timer(3).timeout
		get_tree().reload_current_scene()
		)

func _process(_delta: float) -> void:
	# set player animations
	if velocity.length() > 0:
		_set_player_animation(velocity.normalized())
	else:
		_set_player_idle()
	
	# shoot input
	var shoot_vector: Vector2 = Input.get_vector("shoot left", "shoot right", "shoot up", "shoot down")
	if shoot_vector : _weapon.shoot(shoot_vector)

func _physics_process(_delta: float) -> void:
	# Move logic
	var desired_direction: Vector2 = Input.get_vector("move left", "move right", "move up", "move down");
	velocity = desired_direction * SPEED;
	move_and_slide();

func _set_player_animation(desiredDirection: Vector2) -> void:
	#TODO implement player shoot animation
	if desiredDirection.y == 1:
		_animated_sprite_2d.play("walk_front")
	elif desiredDirection.y == -1:
		_animated_sprite_2d.play("walk_back")
	elif desiredDirection.x == 1:
		_animated_sprite_2d.play("walk_right")
	elif desiredDirection.x == -1:
		_animated_sprite_2d.play("walk_left")

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
