class_name tuga
extends StaticBody2D

@onready var _health: Health = $HealthComponent
@onready var animation_player: AnimationPlayer = $AnimationPlayer


func _ready() -> void:
	# Die function connect
	_health.on_die.connect(on_die)


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
	_health.remove_health(damageAmount)


func _on_ai_shoot_behaviour_on_ai_shoot() -> void:
	animation_player.play("shoot")


func play_idle_anim():
	animation_player.play("idle")
