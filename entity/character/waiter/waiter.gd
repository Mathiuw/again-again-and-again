extends StaticBody2D

@onready var animated_sprite_2d: AnimatedSprite2D = $AnimatedSprite2D

func _ready() -> void:
	SignalBus.on_dialog_end.connect(on_dialogue_end)

func _on_dialogue_component_on_dialogue_start() -> void:
	animated_sprite_2d.play("talking")

func on_dialogue_end() -> void:
	animated_sprite_2d.play("idle")
