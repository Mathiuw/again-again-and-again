class_name Zemerlin
extends CharacterBody2D

var target: Node2D = null

@onready var _health: Health = $HealthComponent
@onready var _navigation_agent_2d: NavigationAgent2D = $NavigationAgent2D
@onready var _animated_sprite_2d: AnimatedSprite2D = $AnimatedSprite2D

func _ready() -> void:
	# Die function connect
	_health.on_die.connect(on_die)
	
	target = get_tree().get_first_node_in_group("player")
	if  !target:
		push_error("Couldnt find player")


func on_die() -> void:
	if _animated_sprite_2d.animation == "die": return
	
	# $NavigationAgent2D.queue_free()
	# $AIShootBehaviour.queue_free()
	# $WeaponComponent.queue_free()
	# $CollisionShape2D.queue_free()
	
	set_process(false)
	set_physics_process(false)


# Timer calculate cooldown
func _on_draw_path_timer_timeout() -> void:
	if target && _navigation_agent_2d:
		_navigation_agent_2d.target_position = target.global_position


func damage(damageAmount: int) -> void:
	_health.remove_health(damageAmount)
	
	if !_health.dead:
		var damage_tween: Tween = create_tween().set_trans(Tween.TRANS_LINEAR)
		damage_tween.tween_property($AnimatedSprite2D, "material:shader_parameter/flash_value", 1, 0.125)
		damage_tween.chain().tween_property($AnimatedSprite2D, "material:shader_parameter/flash_value", 0, 0.125)
