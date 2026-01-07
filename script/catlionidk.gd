class_name Catliondk
extends CharacterBody2D

@export_group("AI Dasher Settings")
@export var move_speed: float = 100;
@export var dash_target_distance: float = 125.0

var target: Node2D = null

@onready var _health: Health = $HealthComponent
@onready var roll_component: Roll = $RollComponent
@onready var navigation_agent_2d: NavigationAgent2D = $NavigationAgent2D

func _ready() -> void:
	# Die function connect
	_health.on_die.connect(func(): queue_free())
	
	target = get_tree().get_first_node_in_group("player")
	if  !target:
		push_error("Couldnt find player")


func _physics_process(_delta: float) -> void:
	if !target:
		return
	
	# Do not query when the map has never synchronized and is empty.
	if NavigationServer2D.map_get_iteration_id(navigation_agent_2d.get_navigation_map()) == 0:
		return
	
#	if navigation_agent_2d.is_navigation_finished():
#		return
	
	if  target.global_position.distance_to(global_position) < dash_target_distance:
		#print("can dash")
		roll_component.start_dash()
		return
	
	var move_direction: Vector2 = navigation_agent_2d.get_next_path_position()
	var new_velocity: Vector2 = global_position.direction_to(move_direction) * move_speed
	
	if  navigation_agent_2d.avoidance_enabled:
		navigation_agent_2d.velocity = new_velocity
	else:
		_on_navigation_agent_2d_velocity_computed(new_velocity)


# Timer calculate cooldown
func _on_draw_path_timer_timeout() -> void:
	if target:
		navigation_agent_2d.target_position = target.global_position


func _on_navigation_agent_2d_velocity_computed(safe_velocity: Vector2) -> void:
	velocity = safe_velocity
	move_and_slide()


func damage(damageAmount: int):
	_health.remove_health(damageAmount)
