class_name Health
extends Node

@export var max_hits: int = 5
var _current_hits: int
var dead: bool = false

func _ready() -> void:
	_current_hits = max_hits

signal on_health_changed(current_hits: int)
signal on_die

func remove_health(value: int) -> void:
	if dead: return 
	
	_current_hits -= value
	_current_hits = clamp(_current_hits,0, max_hits)
	on_health_changed.emit(_current_hits)
	
	if _current_hits <= 0:
		dead = true
		on_die.emit()
