@tool
extends GPUParticles2D
class_name DashParticle

@export var character_owner: CharacterBody2D
@export var animated_sprite: AnimatedSprite2D
@export var roll_component: Roll

func _process(_delta: float) -> void:
	
	if !Engine.is_editor_hint():
		if roll_component.is_dashing:
			emitting = true
		else:
			emitting = false
	
	if !emitting: return
	
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
	
