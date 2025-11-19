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
func _process(_delta: float):
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


func damage(damageAmount: int):
	health_component.remove_health(damageAmount)
