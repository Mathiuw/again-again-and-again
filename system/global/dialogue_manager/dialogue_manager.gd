extends CanvasLayer

const DIALOGUE_CONTAINER: PackedScene = preload("uid://bqori46ibnbqm")
const TEXT_BOX: PackedScene = preload("uid://dmvvtuhuy8xh4")

@export var dialogue_box_scene : PackedScene

var dialogue_steps: Array[DialogueStep]
var dialogue_step_index: int = 0
var dialogue_container_instance: DialogueContainer

var get_current_dialogue_step_text: Array:
	get():
		if dialogue_steps.is_empty():
			push_error("dialogue_steps is empty")
			return []
		
		var file: FileAccess = FileAccess.open(dialogue_steps[dialogue_step_index].json_file.resource_path, FileAccess.READ)
		var data: Variant = JSON.parse_string(file.get_as_text())
		file.close()
	
		var dialogue_array: Array = data[dialogue_steps[dialogue_step_index].key]
		
		return dialogue_array


func _ready() -> void:
	SignalBus.on_dialog_enter.connect(_on_dialogue_enter)
	SignalBus.on_dialog_next_step.connect(_on_dialogue_next_step) 
	SignalBus.on_dialog_end.connect(_on_dialogue_end)


func _on_dialogue_enter(dialogues: Array[DialogueStep]) -> void:
	dialogue_steps = dialogues
	dialogue_step_index = 0
	
	if !dialogue_box_scene:
		push_error("Dialogue box scene not found")
		return
	
	dialogue_container_instance = DIALOGUE_CONTAINER.instantiate()
	add_child(dialogue_container_instance) 
	
	_setup_dialogue_step_box() 


func _on_dialogue_next_step() -> void:
	dialogue_step_index += 1
	
	if dialogue_step_index >= dialogue_steps.size():
		SignalBus.on_dialog_end.emit()
		return
	
	_setup_dialogue_step_box()


func _on_dialogue_end() -> void:
	dialogue_step_index = 0
	dialogue_steps = []
	
	for node: Node in get_children():
		node.queue_free()


func _setup_dialogue_step_box() -> void:
	if dialogue_container_instance:
		dialogue_container_instance.dialogue_panel.add_panel_child_from_scene(TEXT_BOX)
