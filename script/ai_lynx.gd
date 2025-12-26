extends Node2D
class_name AILynx

@export_group("Boss settings")
@export var attacks: Array[AttackBase]
@export var attack_cooldown: float = 4.0


func _ready() -> void:
	for attack in attacks:
		attack.on_attack_end.connect(on_attack_end)
	
	on_attack_end()


func get_random_attack() -> AttackBase:
	if attacks.size() == 0: return null
	if attacks.size() == 1 : return attacks[0]
	
	var random_index: int = randi_range(0, attacks.size()-1)
	
	return attacks[random_index]


func on_attack_end() -> void:
	for attack in attacks:
		attack.process_mode = Node.PROCESS_MODE_DISABLED
	
	print("attack cooldown start")
	await get_tree().create_timer(attack_cooldown).timeout
	print("attack cooldown end")
	
	var selected_attack: AttackBase = get_random_attack()
	
	if selected_attack:
		selected_attack.process_mode = Node.PROCESS_MODE_INHERIT
		selected_attack.attack()
	else:
		push_warning("No attack was selected")
