class_name AIShootBehaviour
extends Node

signal on_ai_shoot

@export_group("Behaviour Settings")
@export var start_shooting_cooldown: float = 0
@export var start_shooting_cooldown_variation: float = 0
@export var shoot_cooldown: float = 0.3
@export var cooldown_variation: float = 0.3
@export var can_shoot: bool = true
@export var shoot_weapon_component: bool = true
# nodes
@export var weapon_component: Weapon
@export var target_raycast_check_2d: TargetRaycastCheck2D
@export var root_node: Node2D

var target: Node2D = null
var current_cooldown: float = 0
var final_shoot_cooldown_variation: float

func _ready() -> void:
	# cooldown variation
	final_shoot_cooldown_variation = randf_range(-cooldown_variation, cooldown_variation)
	
	# set start shoot cooldown
	var final_start_shooting_cooldown: float = start_shooting_cooldown + randf_range(-start_shooting_cooldown_variation, start_shooting_cooldown_variation) 
	
	# begin cooldown custom timer
	current_cooldown = shoot_cooldown + final_shoot_cooldown_variation + final_start_shooting_cooldown
	
	if weapon_component:
		weapon_component.on_finished_shooting.connect(func():
			_on_on_ai_shoot_end()
		)
	
	target = get_tree().get_first_node_in_group("player")
	if  !target:
		push_error("error finding player on node tree")

func _process(delta: float) -> void:
	if !target: return
	
	current_cooldown -= delta
	
	if current_cooldown <= 0 && can_shoot:
		if target_raycast_check_2d:
			if !target_raycast_check_2d.target_on_sight:
				#print("Target not on sight")
				return
		ai_shoot()


func _on_on_ai_shoot_end() -> void:
	can_shoot = true


func weapon_component_shoot() -> void:
	weapon_component.shoot(target.global_position - root_node.global_position, root_node)


func ai_shoot() -> void:
	if !weapon_component || !root_node:
		push_error("cant find weapon conponent or root node")
		return
	
	on_ai_shoot.emit()
	
	if shoot_weapon_component:
		weapon_component_shoot()
	
	current_cooldown = shoot_cooldown + final_shoot_cooldown_variation
