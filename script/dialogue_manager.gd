extends CanvasLayer

var dialogue_box : PackedScene = preload("res://scene/ui/dialogue_box.tscn")

func _ready() -> void:
	SignalBus.on_display_dialog.connect(func(key: String):
		var new_dialogue_box: DialogueBox = dialogue_box.instantiate()
		new_dialogue_box.dialogue_key = key
		add_child(new_dialogue_box) 
		)
