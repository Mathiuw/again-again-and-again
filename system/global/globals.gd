extends Node

static var player_controller: Node2D

static var enemies_spawned: Array[Node2D]

# Room data
static var current_room_loaded : Room

static var previous_room_loaded: Room

static var room_saves = JSON.stringify("")

# DEBUG room_saves check
func _input(event: InputEvent) -> void:
    if event is InputEventKey:
        if event.pressed and event.keycode == KEY_F1:
            print(room_saves)