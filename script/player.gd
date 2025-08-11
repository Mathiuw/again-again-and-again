class_name Player
extends CharacterBody2D

const SPEED: float = 150.0
var buly = Input.get_axis("shoot up", "shoot down");
var bulx = Input.get_axis("shoot left", "shoot right");
var desiredBullet = Vector2 (bulx, buly)

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
	var shootVector: Vector2 = Vector2(Input.get_axis("shoot left", "shoot right"), Input.get_axis("shoot up","shoot down"))
	if shootVector : _weapon.shoot(shootVector)

func _physics_process(_delta: float) -> void:
	var dirY = Input.get_axis("move up", "move down");
	var dirX = Input.get_axis("move left","move right");
	var desiredDirection = Vector2( dirX, dirY );
	
	if desiredDirection:
		velocity = desiredDirection.normalized() * SPEED;
	else:
		velocity = Vector2.ZERO;
	
	move_and_slide();

func _set_player_animation(desiredDirection: Vector2) -> void:
	if desiredDirection.y == 1 and desiredBullet.y == 1:
		_animated_sprite_2d.play("shoot_left")
	elif desiredDirection.y == 1:
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
