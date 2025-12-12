extends Node2D

@export var weapons:Array[Weapon]
@onready var attack_cooldown: Timer = $AttackCooldown

var target: Node2D

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	target = get_tree().get_first_node_in_group("player")
	if  !target:
		push_error("Couldnt find player")
	
	# connect attack function to timeout timer
	attack_cooldown.timeout.connect(func():
		_on_attack_cooldown_timeout()
		)


func _on_attack_cooldown_timeout() -> void:
	var index_to_shoot: int = randi_range(0,weapons.size()-1)
	weapons[index_to_shoot].shoot(weapons[index_to_shoot].transform.x, self)
