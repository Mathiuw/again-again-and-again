extends Node2D
class_name DamageParticle

@export var damage_amount: int = 999
@onready var label: Label = $SubViewport/Label

func _ready() -> void:
	label.text = '-' + str(damage_amount)
	$GPUParticles2D.emitting = true

func _on_damage_particle_finished() -> void:
	queue_free()
