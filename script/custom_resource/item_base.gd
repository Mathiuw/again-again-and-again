class_name ItemBase
extends Resource

@export var item_name: String = "Item"
@export var sprite_frame_index: int = 0

func activate_item(_scene_tree: SceneTree) -> void:
	print("Ativated item")
