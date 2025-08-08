extends Node2D

@export var damage: int = 1;
@export var bulletSpeed: float = 300
@export var shootCooldown: float = 0.35
var currentCooldown: float = 0
@export var bullet: PackedScene

func _process(delta: float) -> void:
	if currentCooldown > 0:
		currentCooldown -= delta
		currentCooldown = clamp(currentCooldown, 0, shootCooldown)
	
	var shootVector: Vector2 = Vector2(Input.get_axis("shoot left", "shoot right"), Input.get_axis("shoot up","shoot down"))
	if shootVector && currentCooldown == 0:
		_shoot(shootVector)
		currentCooldown = shootCooldown

func _shoot(direction: Vector2) -> void:
	var newBullet: Bullet = bullet.instantiate()
	newBullet.add_collision_exception_with(get_parent())
	newBullet.global_position = global_position
	newBullet.rotation = direction.angle()
	
	get_tree().root.add_child(newBullet)
