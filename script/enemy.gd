class_name Enemy
extends CharacterBody2D

var speed: float = 50;
var player: Node2D = null
var player_chase: bool = false
@onready var _health: Health = $Health
@onready var _weapon: Weapon = $Weapon

func _ready() -> void:
	_health.on_die.connect(func():
		queue_free()
		)

func _process(_delta: float) -> void:
	if player_chase:
		_weapon.shoot(player.position - position)

func _physics_process(_delta) -> void:
	if player_chase:
		position += (player.position - position)/speed

func _on_detection_area_body_entered(body: Node2D) -> void:
	_set_chase_state(body, true)

func _on_detection_area_body_exited(body: Node2D) -> void:
	_set_chase_state(body, false)

func _set_chase_state(body: Node2D, state: bool):
	if body.is_in_group("player"):
		if state:
			player = body
			print_debug("entra")
		else:
			player = null
			print_debug("sai")
		
		player_chase = state
