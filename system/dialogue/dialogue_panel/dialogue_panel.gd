extends NinePatchRect
class_name DialoguePanel

@onready var margin_container: MarginContainer = %MarginContainer

func add_panel_child(node: Node) -> void:
	margin_container.add_child(node)


func add_panel_child_from_scene(scene: PackedScene) -> void:
	var instance: Node = scene.instantiate()
	margin_container.add_child(instance)
