@tool
extends Control



# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	if Engine.is_editor_hint():
		#print("tool is running!")
		if collision_shape_2d:
			var rectangle_shape: RectangleShape2D = collision_shape_2d.shape
			if rectangle_shape:
				
