class_name Zemerlin
extends CharacterBody2D

@export_group("AI Chase Settings")
@export var move_speed: float = 100;
@export var acceleration: float = 12.5
@export var friction: float = 4.5

var target: Node2D = null

@onready var _health: Health = $HealthComponent
@onready var _navigation_agent_2d: NavigationAgent2D = $NavigationAgent2D
@onready var _animation_player: AnimationPlayer = $AnimationPlayer

func _ready() -> void:
	# Damage function connect
	_health.on_health_changed.connect(func(_current_hits: int):
		_animation_player.play("damage_flash")
	)
	# Die function connect
	_health.on_die.connect(func(): queue_free())
	
	target = get_tree().get_first_node_in_group("player")
	if  !target:
		push_error("Couldnt find player")


func _physics_process(_delta: float) -> void:
	# Do not query when the map has never synchronized and is empty.
	if NavigationServer2D.map_get_iteration_id(_navigation_agent_2d.get_navigation_map()) == 0:
		return
	if _navigation_agent_2d.is_navigation_finished():
		return
	
	if target:
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


func damage(damageAmount: int):
	_health.remove_health(damageAmount)
