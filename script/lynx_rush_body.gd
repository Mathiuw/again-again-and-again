extends PathFollow2D

@export var damage_amount: float = 15
@export var knockback_force: float = 230
@export var knockback_duration: float = 0.15

func _on_area_2d_body_entered(body: Node2D) -> void:
	if  body is Player:
		var knockback_direction: Vector2 = (body.global_position - global_position).normalized()
		body.apply_knockback(knockback_direction, knockback_force, knockback_duration)
		body.damage(damage_amount)
	else:
		print("No player detected")
