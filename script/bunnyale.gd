class_name Bunnyale
extends CharacterBody2D

# Variables to control speed and detection range
@export var speed = 200
@export var detection_range = 400
@onready var health_component: Health = %HealthComponent

var target: Node2D

func _ready() -> void:
	target = get_tree().get_first_node_in_group("player")
	if  !target:
		push_error("Couldnt find player")
	
	if health_component:
		health_component.on_die.connect(func(): queue_free())
	else:
		push_error("Cant find health_component")


# Called every frame to update AI behavior
func _physics_process(_delta: float) -> void:
	# Do not query when the map has never synchronized and is empty.
	#if NavigationServer2D.map_get_iteration_id(_navigation_agent_2d.get_navigation_map()) == 0:
		#return
	#if _navigation_agent_2d.is_navigation_finished():
		#return
	#
	#if target:
		#var move_direction: Vector2 = _navigation_agent_2d.get_next_path_position()
		#var new_velocity: Vector2 = global_position.direction_to(move_direction) * move_speed
		#
		#if  _navigation_agent_2d.avoidance_enabled:
			#_navigation_agent_2d.velocity = new_velocity
		#else:
			#_on_navigation_agent_2d_velocity_computed(new_velocity)
	if target == null:
		return # Don't do anything if no target is assigned
	
	# Get the direction vector from the AI to the target
	var to_target = target.position - position
	var distance_to_target = to_target.length()

	# If target is within the detection range, the AI runs away
	if distance_to_target < detection_range:
		# Calculate the opposite direction and normalize it
		var direction = (position - target.position).normalized()
		
		# Move the AI in the opposite direction of the target
		velocity = direction * speed
		
		# Apply movement (CharacterBody2D will handle collisions automatically)
		move_and_slide()


func _on_navigation_agent_2d_velocity_computed(safe_velocity: Vector2) -> void:
	velocity = safe_velocity
	move_and_slide()


func damage(damageAmount: int):
	health_component.remove_health(damageAmount)
