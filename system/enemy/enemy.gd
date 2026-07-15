extends Node

func _enter_tree() -> void:
	Globals.enemies_spawned.push_back(self)

func _exit_tree() -> void:
	Globals.enemies_spawned.erase(self)
