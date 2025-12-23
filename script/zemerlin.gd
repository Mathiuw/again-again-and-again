class_name Zemerlin
extends CharacterBody2D

@export_group("AI Chase Settings")
@export var move_speed: float = 100;
@export var acceleration: float = 12.5
@export var friction: float = 4.5

var target: Node2D = null

@onready var _health: Health = $HealthComponent
@onready var _navigation_agent_2d: NavigationAgent2D = $NavigationAgent2D
@onready var _animated_sprite_2d: AnimatedSprite2D = $AnimatedSprite2D

func _ready() -> void:
	# Die function connect
	_health.on_die.connect(func(): queue_free())
	
	target = get_tree().get_first_node_in_group("player")
	if  !target:
		push_error("Couldnt find player")


func _process(_delta: float) -> void:
	print(velocity.normalized())
	set_animations()


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


func _set_zemerlin_animation(desiredDirection: Vector2) -> void:
	var margin: float = 0.4
	
	if desiredDirection.y <= margin && desiredDirection.y >= -margin && desiredDirection.x > 0:
		_animated_sprite_2d.play("walk_right")
	elif desiredDirection.y <= margin && desiredDirection.y >= -margin && desiredDirection.x < 0:
		_animated_sprite_2d.play("walk_left")
	elif desiredDirection.y > margin:
		_animated_sprite_2d.play("walk_front")
	elif desiredDirection.y < margin:
		_animated_sprite_2d.play("walk_back")


func _set_zemerlin_idle() -> void:
	match _animated_sprite_2d.animation:
		"walk_back":
			_animated_sprite_2d.play("idle_back")
		"walk_front":
			_animated_sprite_2d.play("idle_front")
		"walk_left":
			_animated_sprite_2d.play("idle_left")
		"walk_right":
			_animated_sprite_2d.play("idle_right")

# Timer calculate cooldown
func _on_draw_path_timer_timeout() -> void:
	if target:
		_navigation_agent_2d.target_position = target.global_position


func set_animations() -> void:
	if velocity.length() > 0:
		_set_zemerlin_animation(velocity.normalized())
	else:
		_set_zemerlin_idle()


func _on_navigation_agent_2d_velocity_computed(safe_velocity: Vector2) -> void:
	velocity = safe_velocity
	move_and_slide()


func damage(damageAmount: int):
	_health.remove_health(damageAmount)
