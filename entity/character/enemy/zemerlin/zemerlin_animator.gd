extends Node

@export var character_body_2d: CharacterBody2D
@export var animated_sprite_2d: AnimatedSprite2D
@export var health: Health

func _ready() -> void:
	health.on_die.connect(_on_die)

func _process(_delta: float) -> void:
	set_animations()


func set_animations() -> void:
	if health.dead:
		return
	
	if character_body_2d.velocity.length() > 0:
		_set_walk_animation(character_body_2d.velocity.normalized())
	else:
		_set_idle_animation()


func _set_walk_animation(direction: Vector2) -> void:
	var margin: float = 0.4
	
	if direction.y <= margin && direction.y >= -margin && direction.x > 0:
		animated_sprite_2d.play("walk_right")
	elif direction.y <= margin && direction.y >= -margin && direction.x < 0:
		animated_sprite_2d.play("walk_left")
	elif direction.y > margin:
		animated_sprite_2d.play("walk_front")
	elif direction.y < margin:
		animated_sprite_2d.play("walk_back")


func _set_idle_animation() -> void:
	match animated_sprite_2d.animation:
		"walk_back":
			animated_sprite_2d.play("idle_back")
		"walk_front":
			animated_sprite_2d.play("idle_front")
		"walk_left":
			animated_sprite_2d.play("idle_left")
		"walk_right":
			animated_sprite_2d.play("idle_right")


func _on_die() -> void:
	animated_sprite_2d.play("die")
	AudioManager.create_audio(SoundEffect.SOUND_EFFECT_TYPE.DIE)
	
	await animated_sprite_2d.animation_finished
	
	character_body_2d.queue_free()
