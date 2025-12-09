class_name InteractableComponent
extends Area2D

@export var interactable_once: bool = false

signal on_interacted

func interact() -> void:
	print("Interact")
	on_interacted.emit()
	if interactable_once:
		queue_free()
