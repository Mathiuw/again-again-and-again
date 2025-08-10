class_name Health
extends Node

@export var max_hits: int = 5
var _current_hits: int

signal on_health_changed(current_hits: int)
signal on_die

func _init() -> void:
	_current_hits = max_hits

func remove_health(value: int) -> void:
	_current_hits -= value
	_current_hits = clamp(_current_hits,0, max_hits)
	on_health_changed.emit(_current_hits)
	
	if _current_hits <= 0:
		on_die.emit()
