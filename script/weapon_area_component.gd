extends Weapon
class_name WeaponArea

var target_on_area: bool = false

func _on_body_entered(_body: Node2D) -> void:
	target_on_area = true


func _on_body_exited(_body: Node2D) -> void:
	target_on_area = false
