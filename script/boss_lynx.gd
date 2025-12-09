extends Node2D

@onready var _health: Health = $HealthComponent

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	# Die function connect
	_health.on_die.connect(func(): queue_free())

func damage(damageAmount: int):
	_health.remove_health(damageAmount)
