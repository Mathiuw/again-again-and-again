extends CharacterBody2D

var speed = 50;
var player = null
var player_chase = false


func _physics_process(delta):
	if player_chase == true:
		position += (player.position - position)/speed


func _on_detection_area_body_entered(body: Node2D) -> void:
	player = body
	player_chase = true
	print_debug("entrei")


func _on_detection_area_body_exited(body: Node2D) -> void:
	player = null
	player_chase = false
	print_debug("sai")
