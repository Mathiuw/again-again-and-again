class_name Door
extends Area2D

@export var room_transition: RoomTransition
@export var always_open: bool = false
@export var secret_door: bool = false

@onready var door_layer: TileMapLayer = $DoorLayer
@onready var open_particle: GPUParticles2D = $OpenParticle

func  _ready() -> void:
	await get_tree().process_frame

	if secret_door:
		door_layer.hide()

	if  always_open || Globals.enemies_spawned.size() == 0:
		set_state(true)

	SignalBus.on_enemy_die.connect(on_enemy_die)


func _on_body_entered(body: Node2D) -> void:
	if room_transition && body is TopDownController2d:
		var room: Room = await RoomManager.load_room_into_world_from_scene(room_transition.room_uid, room_transition.scene_load_direction, false)
		if room.transition_positions.is_empty(): return
		var transition_position: Marker2D = room.transition_positions.get(room_transition.transition_position_key)
		if transition_position:
			body.global_position = transition_position.global_position


func set_state(state: bool) -> void:
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


func on_enemy_die() -> void:
	if Globals.enemies_spawned.size() == 0:
		set_state(true)
