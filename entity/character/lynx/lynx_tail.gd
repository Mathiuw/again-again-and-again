extends Area2D
class_name LynxTail

@export var idle_time: float = 1.5

@onready var weapon_component: Weapon = $WeaponComponent
@onready var animation_player: AnimationPlayer = $AnimationPlayer
var attack_root: AttackBase 

signal on_tail_attack_end


func _ready() -> void:
	if attack_root:
		attack_root.boss_health_component.on_die.connect(on_die)


func damage(damageAmount: int) -> void:
	if attack_root:
		attack_root.boss_health_component.remove_health(damageAmount)
		if !attack_root.boss_health_component.dead:
			var damage_tween = create_tween().set_trans(Tween.TRANS_LINEAR)
			damage_tween.tween_property($Sprite2D, "material:shader_parameter/flash_value", 1, 0.125)
			damage_tween.chain().tween_property($Sprite2D, "material:shader_parameter/flash_value", 0, 0.125)


func on_die() -> void:
	queue_free()


@warning_ignore("unused_parameter")
func _on_body_entered(body: Node2D) -> void:
	pass # Replace with function body.


func tail_shoot() -> void:
	weapon_component.shoot(transform.x)


func transition_end() -> void:
	await  get_tree().create_timer(idle_time).timeout
	animation_player.play("tail_retract")
	await animation_player.animation_finished
	on_tail_attack_end.emit()
