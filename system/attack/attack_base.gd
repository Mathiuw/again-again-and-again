extends Node2D
class_name AttackBase

@warning_ignore("unused_signal")
signal on_attack_end

@onready var boss_health_component: Health = %HealthComponent

func attack() -> void:
	pass
