extends Node

@export var move_speed: float = 100
@export var navigation_agent_2d: NavigationAgent2D
@export var character_body_2d: CharacterBody2D


func _ready() -> void:
	if !character_body_2d:
		push_warning("character_body_2d is null")
		return
	
	if !navigation_agent_2d:
		push_error("navigation_agent_2d is null")
		return
	
	navigation_agent_2d.velocity_computed.connect(on_velocity_computed)


func _process(_delta: float) -> void:
	if !navigation_agent_2d: return
	if NavigationServer2D.map_get_iteration_id(navigation_agent_2d.get_navigation_map()) == 0: return
	if navigation_agent_2d.is_navigation_finished(): return
	
	var move_direction: Vector2 = navigation_agent_2d.get_next_path_position()
	var new_velocity: Vector2 = character_body_2d.global_position.direction_to(move_direction) * move_speed
	
	if navigation_agent_2d.avoidance_enabled:
		navigation_agent_2d.velocity = new_velocity
	else:
		on_velocity_computed(new_velocity)


func on_velocity_computed(safe_velocity: Vector2):
	character_body_2d.velocity = safe_velocity
	character_body_2d.move_and_slide()
