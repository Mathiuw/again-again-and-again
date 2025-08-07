extends Area2D

@export var desiredRoom: Node2D
@onready var desiredPosition: Marker2D = $desiredPosition

func _on_body_entered(body: Node2D) -> void:
	if desiredRoom != null:
		RoomManager.roomChange.emit(desiredRoom);
		body.global_position = desiredPosition.global_position;
