extends Area2D

@export var fade_time: float = 0.4
@onready var color_rect: ColorRect = $CollisionShape2D/Control/ColorRect

func _on_body_entered(body: Node2D) -> void:
	if body is Player:
		var disapear_tween = create_tween().set_trans(Tween.TRANS_SINE)
		disapear_tween.tween_property(color_rect, "color:a", 0.0, fade_time)
		await disapear_tween.finished
		queue_free()
