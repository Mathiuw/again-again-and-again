class_name DialogueBoxText
extends RichTextLabel

var dialogue_step_resource: DialogueText
var dialogue_array: Array
var dialogue_index: int = 0

var text_speed: float = 0.1
var dialogue_characters: String
var loaded_dialogue_characters: String

func _ready() -> void:
	var file = FileAccess.open(dialogue_step_resource.json_file.resource_path, FileAccess.READ)
	var data = JSON.parse_string(file.get_as_text())
	file.close()
	
	dialogue_array = data[dialogue_step_resource.key]
	
	_show_curren_index_dialog_text()

func _input(_event: InputEvent) -> void:
	if Input.is_action_just_pressed("ui_accept"):
		dialogue_index += 1
		
		if dialogue_index >= dialogue_array.size():
			call_deferred("_end_dialog")
			return
		
		_show_curren_index_dialog_text()

func _show_curren_index_dialog_text() -> void:
	text = dialogue_array[dialogue_index]

func _end_dialog() -> void:
	queue_free()
	SignalBus.on_dialog_next_step.emit()
