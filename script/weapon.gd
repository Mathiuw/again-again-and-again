class_name Weapon
extends Node2D

signal on_finished_shooting

@export_group("Bullet settings")
@export var damage: int = 1;
@export var bulletAmount: int = 1
@export var bulletSpeed: float = 300
@export var bullet_scene: PackedScene = preload("uid://c2jv5crvpeg70")
@export var exception_bodies: Array[Node]

@export_group("Audio Settings")
@export var sound_variant_index: int = 0

@export_group("Shoot settings")
@export var shootCooldown: float = 0.35
@export var betweenBulletsCooldown: float = 0
var currentCooldown: float = 0

@export_group("Transform settings")
@export var transformOverrides: Array[Marker2D]

func _process(delta: float) -> void:
	# shoot cooldown logic
	if currentCooldown > 0:
		currentCooldown -= delta
		currentCooldown = clamp(currentCooldown, 0, shootCooldown)

func shoot(direction: Vector2, shooter: Node) -> void:
	if currentCooldown > 0: return
	
	var bulletsShot: int = 0
	var transformIndex: int = 0 
	
	while (bulletsShot < bulletAmount):
		var new_bullet: Bullet = bullet_scene.instantiate()
		
		if !transformOverrides.is_empty() && transformOverrides[transformIndex] != null:
			new_bullet.global_position = transformOverrides[transformIndex].global_position 
		else: 
			new_bullet.global_position = global_position
		
		if exception_bodies.size() > 0:
			for node in exception_bodies:
				new_bullet.add_collision_exception_with(node)
		new_bullet.bullet_speed = bulletSpeed
		new_bullet.rotation = direction.angle()
		new_bullet.damage = damage
		new_bullet.shooter = shooter
		new_bullet.ignore_group = shooter.get_groups()
		
		var world_root: Node = get_tree().get_first_node_in_group("world")
		if world_root:
			world_root.add_child(new_bullet)
		else:
			push_warning("No world root found, spawning bullet in tree root")
			get_tree().root.add_child(new_bullet)
		
		AudioManager.create_audio(SoundEffect.SOUND_EFFECT_TYPE.SHOOT, sound_variant_index)
		
		if (transformIndex+1 <= transformOverrides.size()-1):
			transformIndex += 1
		
		bulletsShot += 1
		
		if betweenBulletsCooldown > 0:
			await get_tree().create_timer(betweenBulletsCooldown).timeout
	
	currentCooldown = shootCooldown
	on_finished_shooting.emit()
