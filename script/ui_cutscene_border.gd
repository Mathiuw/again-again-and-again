extends CanvasLayer
class_name CutsceneBorder

@onready var animation_player: AnimationPlayer = $AnimationPlayer

func show_border() -> void:
	animation_player.play("show_border")


func hide_border(destroy_node: bool = false) -> void:
	if destroy_node:
		animation_player.animation_finished.connect(queue_free)
	
	animation_player.play("hide_border")
