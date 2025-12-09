class_name LynxSegment
extends CharacterBody2D

@export_group("Lynx Segment Settings")
@export var lynx_head: LynxHead;
@export var draw_path_timer: Timer
@export var override_target: Node2D
@export var aditional_speed: float = 0

@onready var _navigation_agent_2d: NavigationAgent2D = $NavigationAgent2D

func _ready() -> void:
	if  !lynx_head:
		push_error("Couldnt find lynx head")
	
	if !draw_path_timer:
		push_error("cant find draw path timer")
	else:
		draw_path_timer.timeout.connect(func():
			_on_draw_path_timer_timeout()
			)
	


func _physics_process(_delta: float) -> void:
	if !lynx_head:
		return
	
	# Do not query when the map has never synchronized and is empty.
	if NavigationServer2D.map_get_iteration_id(_navigation_agent_2d.get_navigation_map()) == 0:
		return
	
	if _navigation_agent_2d.is_navigation_finished():
		return
	
	var move_direction: Vector2 = _navigation_agent_2d.get_next_path_position()
	var new_velocity: Vector2 = global_position.direction_to(move_direction) * (lynx_head.move_speed + aditional_speed)
	
	if  _navigation_agent_2d.avoidance_enabled:
		_navigation_agent_2d.velocity = new_velocity
	else:
		_on_navigation_agent_2d_velocity_computed(new_velocity)


# Timer calculate cooldown
func _on_draw_path_timer_timeout() -> void:
	if override_target:
		_navigation_agent_2d.target_position = override_target.global_position
	elif lynx_head:
		_navigation_agent_2d.target_position = lynx_head.global_position


func _on_navigation_agent_2d_velocity_computed(safe_velocity: Vector2) -> void:
	velocity = safe_velocity
	move_and_slide()


#func damage(damageAmount: int):
#	_health.remove_health(damageAmount)
