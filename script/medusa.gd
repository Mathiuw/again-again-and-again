class_name medusa
extends StaticBody2D

@onready var health_component: Health = $HealthComponent
@onready var animation_player: AnimationPlayer = $AnimationPlayer


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	health_component.on_die.connect(on_die)


func on_die() -> void:
	if animation_player.current_animation == "die": return
	
	var ai_shoot_behaviour: AIShootBehaviour = $AIShootBehaviour
	ai_shoot_behaviour.queue_free()
	
	set_process(false)
	set_physics_process(false)

	animation_player.play("die")
	
	await animation_player.animation_finished
	
	queue_free()


func damage(damageAmount: int):
	health_component.remove_health(damageAmount)
	
	if !health_component.dead:
		var damage_tween = create_tween().set_trans(Tween.TRANS_LINEAR)
		damage_tween.tween_property($MedusaSprite2D, "material:shader_parameter/flash_value", 1, 0.125)
		damage_tween.chain().tween_property($MedusaSprite2D, "material:shader_parameter/flash_value", 0, 0.125)


func _on_ai_shoot_behaviour_on_ai_shoot() -> void:
	animation_player.play("shoot")


func play_idle_animation() -> void:
	animation_player.play("idle")
