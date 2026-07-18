extends Node2D
class_name DamageParticle

@export var damage_amount: int = 999
@onready var label: Label = $SubViewport/Label
@onready var gpu_particles_2d: GPUParticles2D = $GPUParticles2D

func _ready() -> void:
	label.text = '-' + str(damage_amount)
	gpu_particles_2d.emitting = true

func _on_damage_particle_finished() -> void:
	queue_free()
