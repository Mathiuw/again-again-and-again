@tool
extends Resource
class_name RoomTransition

@export var room_uid: StringName
@export var scene_load_direction: RoomManager.LoadDirection:
	set(value):
		scene_load_direction = value
		if !transition_position_key.is_empty(): return
		match value:
			RoomManager.LoadDirection.LEFT:
				transition_position_key = "right"
			RoomManager.LoadDirection.RIGHT:
				transition_position_key = "left"
			RoomManager.LoadDirection.UP:
				transition_position_key = "down"
			RoomManager.LoadDirection.DOWN:
				transition_position_key = "up"

@export var transition_position_key: String
