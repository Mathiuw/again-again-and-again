extends CanvasLayer
class_name UIFade

@export var fade_settings: FadeSettings
@onready var color_rect: ColorRect = $ColorRect

signal on_fade_end

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	if !fade_settings:
		push_error("no fade settings")
		return
	
	var start_value: float
	var end_value: float
	
	print(fade_settings.fade_type)
	
	match fade_settings.fade_type:
		fade_settings.FadeType.FadeIn:
			start_value = 0
			end_value = 1
		fade_settings.FadeType.FadeOut:
			start_value = 1
			end_value = 0
	
	color_rect.color = fade_settings.fade_color
	color_rect.color.a = start_value
	
	var fade_tween = create_tween().set_trans(Tween.TRANS_SINE)
	fade_tween.tween_property($ColorRect, "color:a", end_value, fade_settings.fade_duration).finished.connect(func(): 
		print("fade is over")
		on_fade_end.emit()
		if fade_settings.destroy_on_end:
			queue_free()
		)
