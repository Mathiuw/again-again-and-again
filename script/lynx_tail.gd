extends Area2D
class_name LynxTail

@export var idle_time: float = 1.5

@onready var weapon_component: Weapon = $WeaponComponent
@onready var animation_player: AnimationPlayer = $AnimationPlayer

signal on_tail_attack_end

func damage(damageAmount: int) -> void:
	var attack_root: AttackBase = get_parent() as AttackBase
	if attack_root:
		attack_root.boss_health_component.remove_health(damageAmount)


@warning_ignore("unused_parameter")
func _on_body_entered(body: Node2D) -> void:
	pass # Replace with function body.


func tail_shoot() -> void:
	weapon_component.shoot(transform.x, self)


func transition_end() -> void:
	await  get_tree().create_timer(idle_time).timeout
	animation_player.play("tail_retract")
	await animation_player.animation_finished
	on_tail_attack_end.emit()
