extends Path2D
class_name EnemyPath2D

@onready var area_2d: Area2D = $Area2D
var enemy_on_area: bool = false


func _on_area_2d_body_entered(_body: Node2D) -> void:
	enemy_on_area = true


func _on_area_2d_body_exited(_body: Node2D) -> void:
	enemy_on_area = false
