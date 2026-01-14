extends Area2D
class_name TargetArea2D

var target: Node2D

func _on_body_entered(body: Node2D) -> void:
	target = body


func _on_body_exited(_body: Node2D) -> void:
	target = null
