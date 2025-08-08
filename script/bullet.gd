class_name Bullet
extends StaticBody2D

@export var bullet_speed: float = 500

func _physics_process(delta: float) -> void:
	var collision: KinematicCollision2D = move_and_collide(transform.x * bullet_speed * delta)
	if collision:
		queue_free()

func _on_timer_timeout() -> void:
	queue_free()
