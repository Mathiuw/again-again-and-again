extends Area2D

@onready var weapon_component: Weapon = $WeaponComponent

func _ready() -> void:
	weapon_component.shoot(transform.x, self)

func damage(damageAmount: int) -> void:
	var attack_root: AttackBase = get_parent() as AttackBase
	if attack_root:
		attack_root.boss_health_component.remove_health(damageAmount)


@warning_ignore("unused_parameter")
func _on_body_entered(body: Node2D) -> void:
	pass # Replace with function body.
