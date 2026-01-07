extends PathFollow2D
class_name LynxBody

@export var damage_amount: float = 15
@export var knockback_force: float = 230
@export var knockback_duration: float = 0.15
@onready var animated_sprite_2d: AnimatedSprite2D = $AnimatedSprite2D
var health_component: Health
var vertical_offset: float

func _ready() -> void:
	vertical_offset = animated_sprite_2d.position.y
	
	if rotation_degrees != 90:
		animated_sprite_2d.play("running_horizontal")
		animated_sprite_2d.rotation_degrees = 0
		animated_sprite_2d.position.y = vertical_offset
	else:
		animated_sprite_2d.play("running_vertical")
		animated_sprite_2d.rotation_degrees = -90
		animated_sprite_2d.position.y = 0

func _on_area_2d_body_entered(body: Node2D) -> void:
	if  body is Player:
		var knockback_direction: Vector2 = (body.global_position - global_position).normalized()
		body.apply_knockback(knockback_direction, knockback_force, knockback_duration)
		body.damage(damage_amount)
	else:
		print("No player detected")


func damage(damage_amount)-> void:
	if health_component:
		health_component.remove_health(damage_amount)
