class_name Door
extends Area2D

@export var room_transition: RoomTransition
@export var always_open: bool = false
@export var secret_door: bool = false

@onready var door_layer: TileMapLayer = $DoorLayer
@onready var open_particle: GPUParticles2D = $OpenParticle
var desired_room: Room = null

func  _ready() -> void:
	if secret_door:
		door_layer.hide()
	
	if room_transition:
		var world: Node = get_tree().get_first_node_in_group("world")
		if world:
			for node in world.get_children():
				if node is Room:
					if node.id == room_transition.desired_room_id:
						desired_room = node
	
	if  always_open:
		set_door_open_state(true)


func _on_body_entered(body: Node2D) -> void:
	if desired_room && room_transition && body is Player:
		var transition_position: Marker2D = desired_room.transition_positions.get(room_transition.transition_key)
		if transition_position:
			RoomManager.on_room_change.emit(desired_room, true)
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
