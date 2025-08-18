class_name InteractComponent
extends Area2D

func try_to_interact() -> void:
	if has_overlapping_bodies():
		for node: InteractableComponent in get_overlapping_areas():
			node.interact()
			break
