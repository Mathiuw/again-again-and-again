class_name Weapon
extends Node2D

@export_category("Bullet settings")
@export var damage: int = 1;
@export var bulletSpeed: float = 300
@export var bullet: PackedScene = preload("res://scene/bullet/bullet_base.tscn")

@export_category("Audio Settings")
@export var sound_variant_index: int = 0

@export_category("Shoot settings")
@export var shootCooldown: float = 0.35
var currentCooldown: float = 0

func _process(delta: float) -> void:
	# shoot cooldown logic
	if currentCooldown > 0:
		currentCooldown -= delta
		currentCooldown = clamp(currentCooldown, 0, shootCooldown)

func shoot(direction: Vector2) -> void:
	if currentCooldown > 0: return
	
	var new_bullet: Bullet = bullet.instantiate()
	new_bullet.add_collision_exception_with(get_parent())
	new_bullet.global_position = global_position
	new_bullet.rotation = direction.angle()
	new_bullet.damage = damage
	
	currentCooldown = shootCooldown
	
	var world_root: Node = get_tree().get_first_node_in_group("world")
	if world_root:
		world_root.add_child(new_bullet)
	else:
		push_warning("No world root found, spawning bullet in tree root")
		get_tree().root.add_child(new_bullet)
	
	AudioManager.create_audio(SoundEffect.SOUND_EFFECT_TYPE.SHOOT, sound_variant_index)
