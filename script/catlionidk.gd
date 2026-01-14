class_name Catliondk
extends CharacterBody2D


@export var move_speed: float = 100
@export_group("Attack Settings")
@export var damage_amount: float = 5
@export var knockback_force: float = 230
@export var knockback_duration: float = 0.15


@onready var _health: Health = $HealthComponent
@onready var dash_component: DashComponent = $DashComponent
@onready var navigation_agent_2d: NavigationAgent2D = $NavigationAgent2D


func _ready() -> void:
	_health.on_die.connect(on_die)


func _physics_process(_delta: float) -> void:
	var collision: KinematicCollision2D = get_last_slide_collision()
	if collision:
		var body: Node2D = collision.get_collider()
		if body is Player:
			var knockback_direction: Vector2 = (body.global_position - global_position).normalized()
			if body.can_take_damage:
				body.apply_knockback(knockback_direction, knockback_force, knockback_duration)
			body.damage(damage_amount)
	
	# Do not query when the map has never synchronized and is empty.
	if NavigationServer2D.map_get_iteration_id(navigation_agent_2d.get_navigation_map()) == 0:
		return
	
	if !dash_component.is_dashing:
		velocity = Vector2.ZERO
		return
	
	if navigation_agent_2d.is_navigation_finished():
		dash_component.stop_dash()
		return
	
	var move_direction: Vector2 = navigation_agent_2d.get_next_path_position()
	var new_velocity: Vector2 = global_position.direction_to(move_direction) * move_speed * dash_component.dash_speed_multiplier
	
	if  navigation_agent_2d.avoidance_enabled:
		navigation_agent_2d.velocity = new_velocity
	else:
		_on_navigation_agent_2d_velocity_computed(new_velocity)


func _on_navigation_agent_2d_velocity_computed(safe_velocity: Vector2) -> void:
	velocity = safe_velocity
	move_and_slide()


func damage(damageAmount: int):
	_health.remove_health(damageAmount)
	
	if !_health.dead:
		var damage_tween = create_tween().set_trans(Tween.TRANS_LINEAR)
		damage_tween.tween_property($AnimatedSprite2D, "material:shader_parameter/flash_value", 1, 0.125)
		damage_tween.chain().tween_property($AnimatedSprite2D, "material:shader_parameter/flash_value", 0, 0.125)


func on_die() -> void:
	queue_free()


func _on_target_raycast_check_2d_on_target_in_sight(target: Object) -> void:
	navigation_agent_2d.target_position = target.global_position
	dash_component.start_dash()
