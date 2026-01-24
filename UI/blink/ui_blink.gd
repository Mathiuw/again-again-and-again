extends CanvasLayer
class_name UIBlink

@export var blink_settings: BlinkSettings
@onready var color_rect: ColorRect = $ColorRect

signal on_full_blink
signal on_blink_end

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	if !blink_settings:
		push_error("no fade settings")
		return
	
	var start_value: float = 0
	var end_value: float = 1
	
	#print(fade_settings.fade_type)
	
	color_rect.color = blink_settings.blink_color
	color_rect.color.a = start_value
	
	var blink_tween = create_tween().set_trans(Tween.TRANS_SINE)
	blink_tween.tween_property($ColorRect, "color:a", end_value, blink_settings.blink_duration/2).finished.connect(func(): 
		#print("blink halfway mark")
		on_full_blink.emit()
		)
	blink_tween.chain().tween_property($ColorRect, "color:a", start_value, blink_settings.blink_duration/2).finished.connect(func(): 
		#print("blink ended")
		on_blink_end.emit()
		if blink_settings.destroy_on_end:
			queue_free()
		)
