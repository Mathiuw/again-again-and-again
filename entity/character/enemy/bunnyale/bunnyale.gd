extends CharacterBody2D
class_name Bunnyale

# Variables to control speed and detection range
@export_group("AI Escape Settings")
@export var move_speed: float = 100
@export var escape_range: float = 250

@onready var health_component: Health = %HealthComponent
@onready var navigation_agent_2d: NavigationAgent2D = $NavigationAgent2D
@onready var animated_sprite_2d: AnimatedSprite2D = $AnimatedSprite2D

var target_escape: Node2D
var escape_point: Vector2

func _ready() -> void:
	target_escape = get_tree().get_first_node_in_group("player")
	if  !target_escape:
		push_error("Couldnt find player")
		return
	
	if health_component:
		health_component.on_die.connect(on_die)
	else:
		push_error("Cant find health_component")
		return


func _process(_delta: float) -> void:
	set_animations()


# Called every frame to update AI behavior
func _physics_process(_delta: float) -> void:
	#print(velocity)
	
	if !target_escape:
		return
	
	# Do not query when the map has never synchronized and is empty.
	if NavigationServer2D.map_get_iteration_id(navigation_agent_2d.get_navigation_map()) == 0:
		return
	
	if navigation_agent_2d.is_navigation_finished():
		return
	
	var move_direction: Vector2 = navigation_agent_2d.get_next_path_position()
	var new_velocity: Vector2 = global_position.direction_to(move_direction) * move_speed
	
	if  navigation_agent_2d.avoidance_enabled:
		navigation_agent_2d.velocity = new_velocity
	else:
		_on_navigation_agent_2d_velocity_computed(new_velocity)


func on_die() -> void:
	if animated_sprite_2d.animation == "die": return
	
	set_process(false)
	set_physics_process(false)
	
	navigation_agent_2d.queue_free()
	$CollisionShape2D.queue_free()
	
	animated_sprite_2d.play("die")
	AudioManager.create_audio(SoundEffect.SOUND_EFFECT_TYPE.DIE)
	
	await animated_sprite_2d.animation_finished
	
	queue_free()


func _on_navigation_agent_2d_velocity_computed(safe_velocity: Vector2) -> void:
	velocity = safe_velocity
	move_and_slide()


func set_animations() -> void:
	if velocity.length() > 0:
		set__animation(velocity.normalized())
	else:
		animated_sprite_2d.play("idle")


func set__animation(desiredDirection: Vector2) -> void:
	var margin: float = 0.4
	
	if desiredDirection.y <= margin && desiredDirection.y >= -margin && desiredDirection.x > 0:
		animated_sprite_2d.play("walk_right")
	elif desiredDirection.y <= margin && desiredDirection.y >= -margin && desiredDirection.x < 0:
		animated_sprite_2d.play("walk_left")
	elif desiredDirection.y > margin:
		animated_sprite_2d.play("walk_front")
	elif desiredDirection.y < margin:
		animated_sprite_2d.play("walk_back")


func calculate_escape_point() -> void:
	# Calculate new escape point based on the escape_range
	var new_escape_point: Vector2 = target_escape.position
	
	while new_escape_point.distance_to(target_escape.global_position) < escape_range:
		new_escape_point = NavigationServer2D.region_get_random_point(NavigationServer2D.map_get_closest_point_owner(navigation_agent_2d.get_navigation_map(), global_position), 1, false)
	
	escape_point = new_escape_point
	navigation_agent_2d.target_position = escape_point
	print(new_escape_point)


func damage(damageAmount: int)-> void:
	health_component.remove_health(damageAmount)
	
	if !health_component.dead:
		var damage_tween = create_tween().set_trans(Tween.TRANS_LINEAR)
		damage_tween.tween_property($AnimatedSprite2D, "material:shader_parameter/flash_value", 1, 0.125)
		damage_tween.chain().tween_property($AnimatedSprite2D, "material:shader_parameter/flash_value", 0, 0.125)


func _on_get_escape_point_timer_timeout() -> void:
	if navigation_agent_2d && navigation_agent_2d.is_navigation_finished() && global_position.distance_to(target_escape.global_position) < escape_range:
		calculate_escape_point()
