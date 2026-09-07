extends Node

@export var enemy_root: Node
@export var health: Health


func _ready() -> void:
	enemy_root = get_parent() if get_parent() != null else null

	await get_tree().process_frame

	Globals.enemies_spawned.push_back(enemy_root)

	if enemy_root:
		if RoomManager.is_enemy_cleared_from_current_room(enemy_root.name):
			enemy_root.queue_free()
			Globals.enemies_spawned.erase(enemy_root)
			print("Enemy is cleared, deleting!")
			return
		
		print("Enemy is not cleared")

		if !health:
			for node: Node in enemy_root.get_children():
				if node is Health:
					health = node
					break
		
		if health:
			health.on_die.connect(on_die)
	else:
		print("enemy_root is null or invalid")


func on_die() -> void:
	if !enemy_root:
		push_error("enemy_root is Null")
		return
	
	Globals.enemies_spawned.erase(enemy_root)

	if !RoomManager.is_enemy_cleared_from_current_room(enemy_root.name):
		RoomManager.add_enemy_cleared_to_current_room(enemy_root.name)

	SignalBus.on_enemy_die.emit()
