class_name Zemerlin
extends CharacterBody2D

@export_group("AI Settings")
@export var move_speed: float = 100;
@export var acceleration: float = 12.5
@export var friction: float = 4.5
@export var shoot_variation: float = 0.1

var target: Node2D = null

@onready var _health: Health = $Health
@onready var _weapon: Weapon = $Weapon
@onready var _navigation_agent_2d: NavigationAgent2D = $NavigationAgent2D
@onready var _animation_player: AnimationPlayer = $AnimationPlayer

func _ready() -> void:
	# Damage function connect
	_health.on_health_changed.connect(func(_current_hits: int):
		_animation_player.play("damage_flash")
	)
	# Die function connect
	_health.on_die.connect(func(): queue_free())
	
	# Shoot variation
	_weapon.shootCooldown += randf_range(-shoot_variation, shoot_variation)
	
	target = get_tree().get_first_node_in_group("player")
	if  !target:
		push_error("Couldnt find player")


func _process(_delta: float) -> void:
	if target:
		_weapon.shoot(target.global_position - global_position, self)
		pass


func _physics_process(_delta: float) -> void:
	if target && !_navigation_agent_2d.is_navigation_finished():
		# TODO Implement enemy acceletation
		var move_direction: Vector2 = _navigation_agent_2d.get_next_path_position()
		var velocity_weight: float = _delta * (acceleration if move_direction else friction)
		
		_navigation_agent_2d.velocity = global_position.direction_to(move_direction) * move_speed
		#_navigation_agent_2d.velocity = lerp(velocity, move_speed, velocity_weight)
		
		


# Timer calculate cooldown
func _on_draw_path_timer_timeout() -> void:
	if target:
		_navigation_agent_2d.target_position = target.global_position


func _on_navigation_agent_2d_velocity_computed(safe_velocity: Vector2) -> void:
	velocity = safe_velocity
	move_and_slide()


func damage(damage: int):
	_health.remove_health(damage)
