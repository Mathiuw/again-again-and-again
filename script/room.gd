class_name Room
extends Node2D

func set_enemy_state(state: bool) -> void:
	var nodes: Array[Node]  = get_children()
	for node: Node in nodes:
		if node is Zemerlin:
			if state:
				node.show()
			else:
				node.hide()
