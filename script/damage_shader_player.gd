extends AnimationPlayer

var health_component: Health

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	if !health_component:
		for child in get_parent().get_children():
			if child is Health:
				health_component = child
	
	# Damage function connect
	if health_component:
		health_component.on_health_changed.connect(on_health_change)
	else:
		push_error("Cant find health_componenent")


func on_health_change(_current_hits) -> void:
	play("damage_flash")
	
