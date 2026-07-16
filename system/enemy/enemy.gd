extends Node

var enemy_root: Node

func _ready() -> void:
	enemy_root = get_parent() if get_parent() != null else null
	
	if enemy_root:
		Globals.enemies_spawned.push_back(enemy_root)
		
		for node: Node in enemy_root.get_children():
			if node is Health:
				node.on_die.connect(on_die)
				return

		enemy_root.tree_exited.connect(on_die)
	else:
		print("enemy_root is null or invalid")


func on_die() -> void:
	if enemy_root:
		Globals.enemies_spawned.erase(enemy_root)
		SignalBus.on_enemy_die.emit()
