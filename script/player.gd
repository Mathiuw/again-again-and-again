extends CharacterBody2D

const SPEED: float = 150.0
var buly = Input.get_axis("shoot up", "shoot down");
var bulx = Input.get_axis("shoot left", "shoot right");
var desiredBullet = Vector2 (bulx, buly)
@onready var animated_sprite_2d: AnimatedSprite2D = $AnimatedSprite2D

func _process(_delta: float) -> void:
	if velocity.length() > 0:
		_set_player_animation(velocity.normalized())
	else:
		_set_player_idle()

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
		animated_sprite_2d.play("shoot_left")
	elif desiredDirection.y == 1:
		animated_sprite_2d.play("walk_front")


	elif desiredDirection.y == -1:
		animated_sprite_2d.play("walk_back")
	elif desiredDirection.x == 1:
		animated_sprite_2d.play("walk_right")
	elif desiredDirection.x == -1:
		animated_sprite_2d.play("walk_left")

func _set_player_idle() -> void:
	match animated_sprite_2d.animation:
		"walk_back":
			animated_sprite_2d.play("idle_back")
		"walk_front":
			animated_sprite_2d.play("idle_front")
		"walk_left":
			animated_sprite_2d.play("idle_left")
		"walk_right":
			animated_sprite_2d.play("idle_right")
