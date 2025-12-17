extends Node2D

enum ShootType {Random, Line, LineTargeted, Targeted } 

@export var shoot_type:ShootType = ShootType.Random
@export var weapons:Array[WeaponArea]
@onready var attack_cooldown: Timer = $AttackCooldown

var target: Node2D

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	target = get_tree().get_first_node_in_group("player")
	if  !target:
		push_error("Couldnt find player")
	
	# connect attack function to timeout timer
	attack_cooldown.timeout.connect(_on_attack_cooldown_timeout)


func _on_attack_cooldown_timeout() -> void:
	match shoot_type:
		ShootType.Random:
			shoot_random()
		ShootType.Line:
			push_warning("Not implemented")
		ShootType.LineTargeted:
			push_warning("Not implemented")
		ShootType.Targeted:
			shoot_targeted()


func shoot_random() -> void:
	#print("Shoot random")
	
	var index_to_shoot: int = randi_range(0,weapons.size()-1)
	weapons[index_to_shoot].shoot(weapons[index_to_shoot].transform.x, self)


func shoot_targeted() -> void:
	#print("Shoot targeted")
	
	var targeted_weapons: Array[WeaponArea]
	
	for weapon in weapons:
		if weapon.target_on_area:
			targeted_weapons.push_back(weapon)
	
	if targeted_weapons.size() == 0:
		print("no weapons targeted")
		return
	
	var index_to_shoot: int = randi_range(0,targeted_weapons.size()-1)
	targeted_weapons[index_to_shoot].shoot(targeted_weapons[index_to_shoot].transform.x, self)
