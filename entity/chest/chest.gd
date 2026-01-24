class_name Chest
extends StaticBody2D

var is_open: bool = false

@export_group("Chest settings")
@export var item_pickup_animation: PackedScene
@export var stored_item: ItemBase

@onready var animated_sprite_2d: AnimatedSprite2D = $AnimatedSprite2D
@onready var interactable_component: InteractableComponent = $InteractableComponent


func _ready() -> void:
	interactable_component.on_interacted.connect(open_chest)


func open_chest() -> void:
	is_open = true
	animated_sprite_2d.play("open")
	
	# activate stored_item
	if stored_item:
		stored_item.on_item_pickup(get_tree())
	
	# item pickup animation
	var item_show_animation: Node2D = item_pickup_animation.instantiate()
	#item_show_animation.global_position = global_position
	
	var item_animation_sprite_2D: Sprite2D = item_show_animation.find_child("Sprite2D")
	if item_animation_sprite_2D && stored_item:
		item_animation_sprite_2D.frame = stored_item.sprite_frame_index
	
	add_child(item_show_animation)
	

	
	print("Chest Opened")
