class_name LynxHead
extends CharacterBody2D

@export_group("Lynx Head Settings")
@export var move_speed: float = 200;

var target: Node2D = null

@onready var _navigation_agent_2d: NavigationAgent2D = $NavigationAgent2D

func _ready() -> void:

	
	target = get_tree().get_first_node_in_group("player")
	if  !target:
		push_error("Couldnt find player")


func _physics_process(_delta: float) -> void:
	if !target:
		return
	
	# Do not query when the map has never synchronized and is empty.
	if NavigationServer2D.map_get_iteration_id(_navigation_agent_2d.get_navigation_map()) == 0:
		return
	
	if _navigation_agent_2d.is_navigation_finished():
		return
	
	var move_direction: Vector2 = _navigation_agent_2d.get_next_path_position()
	var new_velocity: Vector2 = global_position.direction_to(move_direction) * move_speed
	
	if  _navigation_agent_2d.avoidance_enabled:
		_navigation_agent_2d.velocity = new_velocity
	else:
		_on_navigation_agent_2d_velocity_computed(new_velocity)

# Timer calculate cooldown
func _on_draw_path_timer_timeout() -> void:
	if target:
		_navigation_agent_2d.target_position = target.global_position


func _on_navigation_agent_2d_velocity_computed(safe_velocity: Vector2) -> void:
	velocity = safe_velocity
	move_and_slide()
