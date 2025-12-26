extends AttackBase

enum AttackType {Random, Targeted}

@export_group("Lynx Rush Attack Settings")
@export var attack_type: AttackType = AttackType.Random
@export var paths: Array[EnemyPath2D]
@export var move_speed: float = 3
var is_attacking: bool = false

@onready var lynx_rush_body: PathFollow2D = %LynxRushFront

func _ready() -> void:
	lynx_rush_body.progress_ratio = 0.0


func attack() -> void:
	lynx_rush_body.progress_ratio = 0.0
	
	match attack_type:
		AttackType.Random:
			random_attack()
		AttackType.Targeted:
			targeted_attack()


func _process(delta: float) -> void:
	if !is_attacking: 
		return
	
	#print("attacking")
	
	var new_progress_ratio: float = lynx_rush_body.progress_ratio + move_speed * delta
	new_progress_ratio = clampf(new_progress_ratio, 0.0, 1.0)
	lynx_rush_body.progress_ratio = new_progress_ratio
	
	if lynx_rush_body.progress_ratio >= 1.0:
		lynx_rush_body.progress_ratio = 0.0
		is_attacking = false
		on_attack_end.emit()


func random_attack(enable_atack: bool = true) -> void:
	print("Random Attack")
	# selects a random path and attach lynx body to it
	var new_path: Path2D = select_random_path()
	if  new_path:
		lynx_rush_body.reparent(new_path)
	
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
	lynx_rush_body.reparent(on_area_attacks[index_to_attack])
	
	is_attacking = enable_atack


func select_random_path() -> Path2D:
	if paths.size() == 0: return null
	if paths.size() == 1: return paths[0]
	
	var index: int = randi_range(0, paths.size()-1)
	
	return paths[index]
