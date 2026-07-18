@tool
extends Area2D

const EDITOR_MODULATE_OPACITY: float = 0.8

@export var size: Vector2 = Vector2(48,48): 
	set(value):
		size = value
		queue_redraw()

@export var fade_time: float = 0.4
@onready var color_rect: ColorRect = %ColorRect
@onready var color_root: Control = %ColorRoot
@onready var collision_shape_2d: CollisionShape2D = $CollisionShape2D

func _draw() -> void:
	_draw_shape()
	if Engine.is_editor_hint():
		color_rect.modulate.a = EDITOR_MODULATE_OPACITY
	else:
		color_rect.modulate.a = 1.0


func _draw_shape() -> void:
	if collision_shape_2d:
		var rectangle_shape: RectangleShape2D = collision_shape_2d.shape
		if rectangle_shape:
			rectangle_shape.size = size
			if color_root:
				color_root.position = Vector2.ZERO
				color_root.size = rectangle_shape.size
				color_root.position.x = -color_rect.size.x/2
				color_root.position.y = -color_rect.size.y/2


func _on_body_entered(body: Node2D) -> void:
	if Engine.is_editor_hint(): return
	
	if body is TopDownController2d:
		var disapear_tween: Tween = create_tween().set_trans(Tween.TRANS_SINE)
		disapear_tween.tween_property(color_rect, "color:a", 0.0, fade_time)
		await disapear_tween.finished
		queue_free()
