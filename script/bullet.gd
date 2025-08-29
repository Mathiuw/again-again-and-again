class_name Bullet
extends StaticBody2D

@export var bullet_speed: float = 500
@export var damage: int = 1
var shooter: Node

func _physics_process(_delta: float) -> void:
	var collision: KinematicCollision2D = move_and_collide(transform.x * bullet_speed * _delta)
	if collision:
		var body: Node = collision.get_collider() as Node
			
		# Check if the collider is in the same group as the shooter
		#for group in body.get_groups():
			#if shooter.is_in_group(group):
				#return
		
		AudioManager.create_audio(SoundEffect.SOUND_EFFECT_TYPE.HIT)
		
		if body && body.has_method("damage"):
			body.damage(damage)
		
		queue_free()

func _on_timer_timeout() -> void:
	queue_free()
