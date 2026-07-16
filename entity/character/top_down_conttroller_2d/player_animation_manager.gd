extends AnimatedSprite2D

@export var character_body_2d: CharacterBody2D

func _process(_delta: float) -> void:
	if character_body_2d:
		#if int(character_body_2d.get_position_delta().length()) > 0:
		if character_body_2d.get_position_delta().length() > 0.05:
			_set_player_animation(character_body_2d.velocity)
		else: 
			_set_player_idle() 


func _set_player_animation(desiredDirection: Vector2) -> void:
	if desiredDirection.y == 0 && desiredDirection.x > 0:
		play("walk_right")
	elif desiredDirection.y == 0 && desiredDirection.x < 0:
		play("walk_left")
	elif desiredDirection.y > 0:
		play("walk_front")
	elif desiredDirection.y < 0:
		play("walk_back")


func _set_player_idle() -> void:
	match animation:
		"walk_back":
			play("idle_back")
		"walk_front":
			play("idle_front")
		"walk_left":
			play("idle_left")
		"walk_right":
			play("idle_right")
