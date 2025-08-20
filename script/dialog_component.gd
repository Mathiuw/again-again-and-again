class_name DialogueComponent
extends InteractableComponent

@export var dialogue_resource: Dialogue

func interact():
	if !dialogue_resource:
		push_error("Error finding dialogue resource")
		return
	
	super()
	SignalBus.on_display_dialog.emit(dialogue_resource.dialogue_key)
