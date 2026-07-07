extends StaticBody2D
class_name BulletBase

var bullet_speed: float = 0
var damage: int = 0
var shooter: Node
var ignore_group: Array[StringName]

func set_values(_bullet_speed: float, _damage: int, _shooter: Node):
	bullet_speed = _bullet_speed
	damage = _damage
	shooter = _shooter

func _physics_process(_delta: float) -> void:
	var collision: KinematicCollision2D = move_and_collide(transform.x * bullet_speed * _delta)
	if collision:
		var body: Node2D = collision.get_collider() as Node2D
		collision_check(body)


func _on_timer_timeout() -> void:
	queue_free()


func collision_check(body: Node2D) -> void:
	if !body: return
	
	# Check if the collider is in the same group as the shooter
	for group in ignore_group:
		if  body.is_in_group(group):
			add_collision_exception_with(body)
			print("collision ignored")
			return
	
	AudioManager.create_audio(SoundEffect.SOUND_EFFECT_TYPE.HIT)
	
	#print_debug(body.owner)
	if body.has_method("damage"):
		body.damage(damage)
	
	queue_free()


func _on_area_2d_area_entered(area: Area2D) -> void:
	collision_check(area)
