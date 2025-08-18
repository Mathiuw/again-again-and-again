class_name Room
extends Node2D

@export_category("Room Settings")
@export var initial_room: bool = false

signal on_no_enemies_left

func _ready() -> void:
	RoomManager.on_room_change.connect(func(room: Room, _smooth_transition: bool):
		if room != self:
			set_room_state(false)
		else:
			set_room_state(true)
		)
	
	call_deferred("check_initial_room")
	
	if get_enemy_count() == 0:
		return
	
	for node in get_children():
		if node is Zemerlin:
			node._health.on_die.connect(func():
				get_enemy_count()
				)


func check_initial_room() -> void:
		if  initial_room: 
			RoomManager.on_room_change.emit(self, false)


func open_room_doors() -> void:
	for node in get_children():
		if node is Door:
			node.set_door_open_state(true)


func get_enemy_count() -> int:
	var count: int = 0
	for node in get_children():
		if node is Zemerlin && !node._health.dead:
			count =+ 1
	
	if count == 0:
		open_room_doors()
		on_no_enemies_left.emit()
	
	return count


func set_room_state(state: bool) -> void:
	for node: Node in get_children():
		node.set_process(state)
		node.set_physics_process(state)
