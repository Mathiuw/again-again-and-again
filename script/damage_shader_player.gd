extends AnimationPlayer

@onready var health_component: Health = %HealthComponent

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	
	# Damage function connect
	if health_component:
		health_component.on_health_changed.connect(func(_current_hits):
			play("damage_flash")
		)
	else:
		push_error("Cant find health_componenent")
