extends GPUParticles2D
class_name DashParticleAnimatedSprite2D

@export var character_owner: CharacterBody2D
@export var animated_sprite: AnimatedSprite2D
@onready var dash_component: DashComponent = %DashComponent

func _process(_delta: float) -> void:
	if dash_component.is_dashing:
		emitting = true
	else:
		emitting = false
		return
	
	if !character_owner:
		push_error("charcter_owner not assigned/ not found")
		return
	
	if !animated_sprite:
		push_error("animated_sprite not assigned/ not found")
		return
	
	var particle_process_material: ParticleProcessMaterial = process_material
	particle_process_material.direction = -Vector3(character_owner.velocity.x, character_owner.velocity.y, 0)
	
	var sprite_frames: SpriteFrames = animated_sprite.sprite_frames
	texture = sprite_frames.get_frame_texture(animated_sprite.animation, animated_sprite.frame)
	
