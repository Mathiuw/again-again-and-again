class_name Chest
extends StaticBody2D

var is_open: bool = false

@onready var animated_sprite_2d: AnimatedSprite2D = $AnimatedSprite2D
@onready var interactable_component: InteractableComponent = $InteractableComponent

func _ready() -> void:
	interactable_component.on_interacted.connect(func():
		open_chest()
		)

func open_chest() -> void:
	is_open = true
	animated_sprite_2d.play("open")
	print("Chest Opened")
