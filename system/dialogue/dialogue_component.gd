class_name DialogueComponent
extends InteractableComponent

signal on_dialogue_start

@export var audio_bip_index: int = -1
@export var dialogue_steps: Array[DialogueStep]

func interact() -> void:
	if !dialogue_steps:
		push_error("Error finding dialogue resource")
		return
	
	super()
	
	if audio_bip_index >= -1:
		AudioManager.current_dialogue_bip_audio_index = audio_bip_index
	SignalBus.on_dialog_enter.emit(dialogue_steps)
	on_dialogue_start.emit()
