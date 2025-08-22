extends HBoxContainer

var dialogue_step_resource: DialogueOption
var button_option: PackedScene = preload("uid://bosoqve4wshsx")

func _ready() -> void:
	var file: FileAccess = FileAccess.open(dialogue_step_resource.json_file.resource_path, FileAccess.READ)
	var data = JSON.parse_string(file.get_as_text())
	file.close()
	
	var options_dict: Dictionary = data[dialogue_step_resource.key]
	
	for key in options_dict:
		var button: Button = button_option.instantiate()
		button.text = key
		button.pressed.connect(func():
			SignalBus.on_dialog_next_step.emit()
			)
		add_child(button)
