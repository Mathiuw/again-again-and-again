extends GPUParticles2D
class_name DashParticleSprite2D

@export var character_body_2d: CharacterBody2D
@export var sprite_2d: Sprite2D
@onready var dash_component: DashComponent = %DashComponent

func _process(_delta: float) -> void:
	
	if dash_component.is_dashing:
		emitting = true
	else:
		emitting = false
		return
	
	if !character_body_2d:
		push_error("charcter_owner not assigned/ not found")
		return
	
	if !sprite_2d:
		push_error("animated_sprite not assigned/ not found")
		return
	
	var particle_process_material: ParticleProcessMaterial = process_material
	particle_process_material.direction = -Vector3(character_body_2d.velocity.x, character_body_2d.velocity.y, 0)
	
	#var sprite_frames: SpriteFrames = animated_sprite.sprite_frames
	#texture = sprite_frames.get_frame_texture(animated_sprite.animation, animated_sprite.frame)
	texture = sprite_2d.texture
	
