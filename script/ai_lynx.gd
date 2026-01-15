extends Node2D
class_name AILynx

const AFTER_LYNX_FIGHT = preload("uid://cx2joiy5tdfss")

@export var attacks: Array[AttackBase]
@export var attack_cooldown: float = 4.0
@export var active: bool = true: 
	set(value):
		active = value
		if value == true:
			on_attack_end()
			print("Activated lynx")

@onready var health_component: Health = %HealthComponent

var target_areas: Array[TargetArea2D]

func _ready() -> void:
	for node in get_parent().get_children():
		if node is TargetArea2D:
			target_areas.push_back(node)
	
	var lynx_attack_ranged: AttackLynxRanged = $LynxAttackRanged
	
	for target_area in target_areas:
		for child in target_area.get_children():
			if child is Marker2D:
				lynx_attack_ranged.spawn_markers.push_back(child)
	
	health_component.on_die.connect(on_die)
	
	var lynx_attack_rush: AttackLynxRush = $LynxAttackRush

	for target_area in target_areas:
		for child in target_area.get_children():
			if child is Path2D:
				lynx_attack_rush.paths.push_back(child)

	for attack in attacks:
		attack.on_attack_end.connect(on_attack_end)
	
	if active:
		on_attack_end()


func get_random_attack() -> AttackBase:
	if attacks.size() == 0: return null
	if attacks.size() == 1 : return attacks[0]
	
	var random_index: int = randi_range(0, attacks.size()-1)
	
	return attacks[random_index]


func on_attack_end() -> void:
	for attack in attacks:
		attack.process_mode = Node.PROCESS_MODE_DISABLED
	
	if health_component.dead:
		print("lynx is dead, attack canceled")
		return
	
	print("attack cooldown start")
	await get_tree().create_timer(attack_cooldown).timeout
	print("attack cooldown end")
	
	var selected_attack: AttackBase = get_random_attack()
	
	if selected_attack:
		selected_attack.process_mode = Node.PROCESS_MODE_INHERIT
		selected_attack.attack()
	else:
		push_warning("No attack was selected")


func on_die() -> void:
	AudioManager.set_music(null)
	AudioManager.create_audio(SoundEffect.SOUND_EFFECT_TYPE.DIE)
	
	const UI_BLINK = preload("uid://dj3tmo5quukha")
	
	var new_blink: UIBlink = UI_BLINK.instantiate()
	get_parent().add_child(new_blink)
	
	await  new_blink.on_full_blink
	
	# spawn after lynx fight scene
	get_parent().add_child(AFTER_LYNX_FIGHT.instantiate()) 
	queue_free()
