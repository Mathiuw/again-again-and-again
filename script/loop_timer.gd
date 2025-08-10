class_name LoopTimer
extends Timer

func _on_timeout() -> void:
	print("Loop ended")
	get_tree().reload_current_scene()
