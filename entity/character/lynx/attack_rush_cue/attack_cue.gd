extends Node2D
class_name AttackCue

signal on_cue_end

@export var cue_time: float = 1.0

func _ready() -> void:
	print_debug("Cue started")
	await get_tree().create_timer(cue_time).timeout
	print_debug("Cue ended")
	on_cue_end.emit()
	queue_free()
