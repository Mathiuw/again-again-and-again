class_name InteractComponent
extends Area2D

func try_to_interact() -> bool:
	if has_overlapping_areas():
		for node: InteractableComponent in get_overlapping_areas():
			node.interact()
			return true
	return false
