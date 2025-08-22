extends CanvasLayer

var dialogue_box_scene : PackedScene = preload("uid://bqori46ibnbqm")
var dialogue_steps: Array[DialogueBase]
var dialogue_step_index: int = 0
var dialogue_box_node: Node

func _ready() -> void:
	SignalBus.on_dialog_enter.connect(func(dialogue_steps: Array[DialogueBase]):
		self.dialogue_steps = dialogue_steps
		dialogue_step_index = 0
	
		dialogue_box_node = dialogue_box_scene.instantiate()
		add_child(dialogue_box_node) 
		
		_setup_dialogue_step_box() 
		)
	
	SignalBus.on_dialog_next_step.connect(func():
		dialogue_step_index += 1
		
		if dialogue_step_index >= dialogue_steps.size():
			SignalBus.on_dialog_end.emit()
			return
		
		_setup_dialogue_step_box()
		) 
		
	SignalBus.on_dialog_end.connect(func():
		dialogue_step_index = 0
		dialogue_steps = []
		
		for node in get_children():
			node.queue_free()
		)

func _setup_dialogue_step_box():
		var dialogue_step_box = dialogue_steps[dialogue_step_index].dialog_box_scene.instantiate()
		dialogue_step_box.dialogue_step_resource = dialogue_steps[dialogue_step_index]
		
		dialogue_box_node.add_child(dialogue_step_box) 
