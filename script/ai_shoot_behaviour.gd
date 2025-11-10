class_name AIShootBehaviour
extends Node

@export_group("Behaviour Settings")
@export var start_shooting_cooldown: float = 0
@export var shoot_cooldown: float = 0.3
@export var cooldown_variation: float = 0.3
@export var can_shoot: bool = true
@export var weapon_component: Weapon
@export var root_node: Node2D

signal on_ai_shoot

var target: Node2D = null
var current_cooldown: float = 0

func _ready() -> void:
	# begin cooldown custom timer
	current_cooldown = shoot_cooldown
	
	target = get_tree().get_first_node_in_group("player")
	if  !target:
		push_error("error finding player on node tree")
	
	if weapon_component:
		# random shoot cooldown variation
		weapon_component.shootCooldown += randf_range(-cooldown_variation, cooldown_variation)


func _process(delta: float) -> void:
	
	current_cooldown -= delta
	
	if current_cooldown <= 0:
		on_ai_shoot.emit()
		current_cooldown = shoot_cooldown


func _on_on_ai_shoot() -> void:
	if can_shoot && target:
		ai_shoot()


func ai_shoot() -> void:
	if !weapon_component || !root_node:
		return
	
	weapon_component.shoot(target.global_position - root_node.global_position, root_node)
