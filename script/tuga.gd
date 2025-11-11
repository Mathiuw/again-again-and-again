class_name tuga
extends CharacterBody2D

@onready var _health: Health = $HealthComponent
@onready var animation_player: AnimationPlayer = $AnimationPlayer


func _ready() -> void:
	# Die function connect
	_health.on_die.connect(func(): queue_free())


func damage(damageAmount: int):
	_health.remove_health(damageAmount)


func _on_ai_shoot_behaviour_on_ai_shoot() -> void:
	animation_player.play("shoot")


func play_idle_anim():
	animation_player.play("idle")
