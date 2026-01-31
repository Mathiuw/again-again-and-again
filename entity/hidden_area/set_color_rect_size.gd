@tool
extends Control

@onready var collision_shape_2d: CollisionShape2D = $".."

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	if Engine.is_editor_hint():
		#print("tool is running!")
		if collision_shape_2d:
			var rectangle_shape: RectangleShape2D = collision_shape_2d.shape
			if rectangle_shape:
				size = rectangle_shape.size

		position.x = -size.x/2
		position.y = -size.y/2
