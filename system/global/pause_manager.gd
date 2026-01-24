extends CanvasLayer

var can_pause_input: bool = false

var options_menu: PackedScene = preload("uid://cq21v02bo1sap")
var pause_menu: PackedScene = preload("uid://ce55cnahm7gfg")

signal on_pause_state_changed(state: bool)

func _input(_event: InputEvent) -> void:
	if  can_pause_input && Input.is_action_just_pressed("ui_cancel"):
		pause_toggle()
		
		var is_paused: bool = get_tree().paused
		
		if  is_paused:
			spawn_pause_menu()
		on_pause_state_changed.emit(is_paused)

func pause_toggle() -> void:
	set_pause_state(!get_tree().paused)

func set_pause_state(state: bool):
	get_tree().paused = state
	on_pause_state_changed.emit(state)

func spawn_options_menu() -> void:
	var options_menu_node = options_menu.instantiate()
	add_child(options_menu_node)


func spawn_pause_menu() -> void:
	var pause_menu_node = pause_menu.instantiate()
	add_child(pause_menu_node)
