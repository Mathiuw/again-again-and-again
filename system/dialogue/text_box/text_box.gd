class_name DialogueBoxText
extends RichTextLabel

@export var text_speed: float = 0.05

var dialogue_array: Array
var dialogue_index: int = 0


func _ready() -> void:
	dialogue_array = DialogueManager.get_current_dialogue_step_text
	if !dialogue_array.is_empty():
		_load_current_index_dialog_text()


func _input(_event: InputEvent) -> void:
	if Input.is_action_just_pressed("interact"):
		if visible_characters != -1:
			visible_characters = -1
			return
		
		dialogue_index += 1
		
		if dialogue_index >= dialogue_array.size():
			_end_dialog.call_deferred()
			return
		
		await  _load_current_index_dialog_text()


func _load_current_index_dialog_text() -> void:
	visible_characters = 0
	text = dialogue_array[dialogue_index]
	for n in text.length():
		if visible_characters == -1: return
		visible_characters += 1
		await get_tree().create_timer(text_speed).timeout
	visible_characters = -1


func _end_dialog() -> void:
	queue_free()
	SignalBus.on_dialog_next_step.emit()
