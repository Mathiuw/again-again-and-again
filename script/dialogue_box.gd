class_name DialogueBox
extends PanelContainer

var dialogue_key: String
var dialogue_array: Array
var dialogue_index: int = 0

@onready var rich_text_label: RichTextLabel = $VBoxContainer/RichTextLabel

func _input(_event: InputEvent) -> void:
	if Input.is_action_just_pressed("ui_accept"):
		dialogue_index += 1
		
		if dialogue_index >= dialogue_array.size():
			call_deferred("_end_dialog") 
			return
		
		_show_curren_index_dialog_text()

func _ready() -> void:
	var file = FileAccess.open("res://json/keeper.json", FileAccess.READ)
	var data = JSON.parse_string(file.get_as_text())
	file.close()
	
	dialogue_array = data[dialogue_key]
	_show_curren_index_dialog_text()

func _show_curren_index_dialog_text() -> void:
	rich_text_label.text = dialogue_array[dialogue_index]

func _end_dialog() -> void:
	queue_free()
	SignalBus.on_dialog_end.emit()
