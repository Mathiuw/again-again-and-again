class_name Weapon
extends Node2D

@export_category("Bullet settings")
@export var damage: int = 1;
@export var bulletSpeed: float = 300
@export var bullet: PackedScene = preload("res://scene/bullet.tscn")

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
	
	var newBullet: Bullet = bullet.instantiate()
	newBullet.add_collision_exception_with(get_parent())
	newBullet.global_position = global_position
	newBullet.rotation = direction.angle()
	newBullet.damage = damage
	
	currentCooldown = shootCooldown
	
	get_tree().root.add_child(newBullet)
