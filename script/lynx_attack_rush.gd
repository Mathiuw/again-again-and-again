extends Node2D

@export_group("Lynx Rush Attack Settings")
@export var paths:Array[Path2D]
@export var move_speed: float = 10
@export var attack_cooldown: float = 3
var is_attacking: bool = false

@onready var lynx_rush_body: PathFollow2D = %LynxRushFront
@onready var timer: Timer = $Timer

func _ready() -> void:
	lynx_rush_body.progress_ratio = 0.0
	
	timer.timeout.connect(on_timeout)
	timer.start(attack_cooldown)


func _process(delta: float) -> void:
	if !is_attacking: 
		return
	
	#print("attacking")
	
	var new_progress_ratio: float = lynx_rush_body.progress_ratio + move_speed * delta
	new_progress_ratio = clampf(new_progress_ratio, 0.0, 1.0)
	lynx_rush_body.progress_ratio = new_progress_ratio
	
	if lynx_rush_body.progress_ratio >= 1.0:
		lynx_rush_body.progress_ratio = 0.0
		timer.start(attack_cooldown)
		is_attacking = false


func on_timeout() -> void: 
	setup_lynx_attack()


func setup_lynx_attack(enable_atack: bool = true) -> void:
	# selects a random path and attach lynx body to it
	var new_path: Path2D = select_random_path()
	if  new_path:
		lynx_rush_body.reparent(new_path)
	
	# reset path progress
	lynx_rush_body.progress_ratio = 0.0
	
	is_attacking = enable_atack


func select_random_path() -> Path2D:
	if paths.size() == 0: return null
	
	if paths.size() == 1: return paths[0]
	
	var index: int = randi_range(0, paths.size()-1)
	
	return paths[index]
