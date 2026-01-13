extends Area2D
class_name TargetArea2D

@export var id: int = 0
var target: Node2D

func _on_body_entered(body: Node2D) -> void:
	target = body


func _on_body_exited(_body: Node2D) -> void:
	target = null
