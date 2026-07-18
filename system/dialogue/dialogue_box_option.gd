extends HBoxContainer

var dialogue_step: DialogueStep
var button_option: PackedScene = preload("uid://bosoqve4wshsx")

func _ready() -> void:
	var file: FileAccess = FileAccess.open(dialogue_step.json_file.resource_path, FileAccess.READ)
	var data: Variant = JSON.parse_string(file.get_as_text())
	file.close()
	
	var options_dict: Dictionary = data[dialogue_step.key]
	
	var first_buttom: bool = true
	var previous_buttom: Button = null
	
	for key: String in options_dict:
		var button: Button = button_option.instantiate()
		button.text = key
		button.pressed.connect(func() -> void:
			SignalBus.on_dialog_next_step.emit()
			)
		add_child(button)
		
		if first_buttom:
			button.grab_focus()
			first_buttom = false
		
		if previous_buttom:
			button.focus_neighbor_left = previous_buttom.get_path()
		
		previous_buttom = button
