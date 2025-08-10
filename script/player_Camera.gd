extends Camera2D

func _ready() -> void:
	RoomManager.on_room_change.connect(func(room):
		global_position = room.global_position;
		)
