class_name Bullet
extends StaticBody2D

@export var bullet_speed: float = 500
@export var damage: int = 1

func _physics_process(delta: float) -> void:
	var collision: KinematicCollision2D = move_and_collide(transform.x * bullet_speed * delta)
	if collision:
		AudioManager.create_audio(SoundEffect.SOUND_EFFECT_TYPE.HIT)
		var character: CharacterBody2D = collision.get_collider() as CharacterBody2D
		if character:
			var health: Health = character.get_node("%Health")
			if  health:
				health.remove_health(1)
		queue_free()

func _on_timer_timeout() -> void:
	queue_free()
