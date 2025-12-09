class_name Door
extends Area2D

@export var desired_room: Room
@export var always_open: bool = false
@onready var desired_position: Marker2D = $DesiredPosition
@onready var door_static_body: StaticBody2D = $DoorStaticBody
@onready var open_particle: GPUParticles2D = $OpenParticle

func  _ready() -> void:
	if  always_open:
		set_door_open_state(true)

func _on_body_entered(body: Node2D) -> void:
	if desired_room && body is Player:
		RoomManager.on_room_change.emit(desired_room, true)
		body.global_position = desired_position.global_position

func set_door_open_state(state: bool) -> void:
	if state:
		door_static_body.hide()
		door_static_body.process_mode = Node.PROCESS_MODE_DISABLED
		open_particle.emitting = true
	else:
		if always_open:
			return
		door_static_body.show()
		door_static_body.process_mode = Node.PROCESS_MODE_INHERIT
		open_particle.emitting = false
