class_name DialogueComponent
extends InteractableComponent

@export var dialogue_steps: Array[DialogueBase]

func interact():
	if !dialogue_steps:
		push_error("Error finding dialogue resource")
		return
	
	super()
	
	SignalBus.on_dialog_enter.emit(dialogue_steps)
