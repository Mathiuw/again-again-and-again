class_name Door
extends Area2D

@export var room_transition: RoomTransition
@export var always_open: bool = false
@export var secret_door: bool = false

@onready var door_layer: TileMapLayer = $DoorLayer
@onready var open_particle: GPUParticles2D = $OpenParticle

func  _ready() -> void:
	if secret_door:
		door_layer.hide()
	
	if  always_open:
		set_door_open_state(true)


func _on_body_entered(body: Node2D) -> void:
	if room_transition && body is Player:
		var room_01: Room = room_transition.room_01.instantiate()
		if room_01 && room_01 != self:
			var transition_position: Marker2D = room_01.transition_positions.get(room_transition.transition_position_key_to_01)
			if transition_position:
				body.global_position = transition_position.global_position


func set_door_open_state(state: bool) -> void:
	if state:
		door_layer.hide()
		door_layer.collision_enabled = false
		if !secret_door:
			open_particle.emitting = true
	else:
		if always_open:
			return
		if !secret_door:
			door_layer.show()
		door_layer.collision_enabled = true
		open_particle.emitting = false
