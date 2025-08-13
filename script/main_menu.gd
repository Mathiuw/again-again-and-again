extends Control

@onready var options_menu: TextureRect = $OptionsMenu

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	options_menu.hide()

func _on_options_button_pressed() -> void:
	options_menu.show()

func _on_exit_options_button_pressed() -> void:
	options_menu.hide()

func _on_play_button_pressed() -> void:
	get_tree().change_scene_to_file("res://scene/main.tscn")

func _on_exit_button_pressed() -> void:
	get_tree().quit()
