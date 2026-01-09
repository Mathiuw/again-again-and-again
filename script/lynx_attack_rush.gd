extends AttackBase

const LYNX_RUSH_BODY: PackedScene = preload("uid://bft146fxu4jge")

enum AttackType {Random, Targeted}

@export_group("Lynx Rush Attack Settings")
@export var attack_type: AttackType = AttackType.Random
@export var paths: Array[EnemyPath2D]
@export var move_speed: float = 3

var is_attacking: bool = false
var current_lynx_rush_body: LynxBody

func attack() -> void:
	match attack_type:
		AttackType.Random:
			random_attack()
		AttackType.Targeted:
			targeted_attack()


func _process(delta: float) -> void:
	if !is_attacking: 
		return
	
	#print("attacking")
	
	if current_lynx_rush_body == null:
		push_error("lynx body not found")
	
	var new_progress_ratio: float = current_lynx_rush_body.progress_ratio + move_speed * delta
	new_progress_ratio = clampf(new_progress_ratio, 0.0, 1.0)
	current_lynx_rush_body.progress_ratio = new_progress_ratio
	
	if current_lynx_rush_body.progress_ratio >= 1.0:
		current_lynx_rush_body.queue_free()
		current_lynx_rush_body = null
		is_attacking = false
		on_attack_end.emit()


func spawn_lynx_body(path_to_spawn:EnemyPath2D) -> void:
	current_lynx_rush_body = LYNX_RUSH_BODY.instantiate()
	current_lynx_rush_body.health_component = boss_health_component
	path_to_spawn.add_child(current_lynx_rush_body)


func random_attack(enable_atack: bool = true) -> void:
	print("Random Attack")
	# selects a random path and attach lynx body to it
	var new_path: Path2D = select_random_path()
	if  new_path:
		spawn_lynx_body(new_path)
	
	is_attacking = enable_atack


func targeted_attack(enable_atack: bool = true) -> void:
	print("Targeted Attack")
	
	var on_area_attacks: Array[EnemyPath2D]
	
	for path in paths:
		if path.target_area.target:
			on_area_attacks.push_back(path)
	
	if on_area_attacks.size() == 0:
		print("no attack on area targeted")
		return
	
	var index_to_attack: int = randi_range(0,on_area_attacks.size()-1)
	
	spawn_lynx_body(on_area_attacks[index_to_attack])
	
	is_attacking = enable_atack


func select_random_path() -> Path2D:
	if paths.size() == 0: return null
	if paths.size() == 1: return paths[0]
	
	var index: int = randi_range(0, paths.size()-1)
	
	return paths[index]
