extends "res://script/damage_shader_player.gd"

func _ready() -> void:
	var lynx_body: LynxBody = get_parent()
	if  lynx_body:
		health_component = lynx_body.health_component
	super()
