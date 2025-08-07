extends Camera2D

func _ready() -> void:
	RoomManager.roomChange.connect(func(room):
		global_position = room.global_position;
		)
