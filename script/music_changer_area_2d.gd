extends Area2D

@export var new_music: AudioStream = null
@export var destroy_on_change: bool = true

func _on_body_entered(body: Node2D) -> void:
	if body is Player:
		AudioManager.set_music(new_music)
	if destroy_on_change:
		queue_free()
