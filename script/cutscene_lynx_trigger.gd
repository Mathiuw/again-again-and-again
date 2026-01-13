extends Node2D

#var cutscene_border_scene: PackedScene = preload("uid://ce5ld2u2q4it5")
const BOSS_MASTER:AudioStream = preload("uid://b7vpmnspgxnx5")

@onready var animated_sprite_2d: AnimatedSprite2D = $AnimatedSprite2D

func _on_trigger_boss_area_body_entered(_body: Node2D) -> void:
	%TriggerBossArea.queue_free()
	
	print("Cutscene trigged")
	
	#var new_cutscene_border: CutsceneBorder = cutscene_border_scene.instantiate()
	#get_tree().root.add_child(new_cutscene_border)
	#new_cutscene_border.show_border()
	
	AudioManager.set_music(BOSS_MASTER)
	#PauseManager.can_pause_input = false
	animated_sprite_2d.play("default")


func _on_animated_sprite_2d_animation_finished() -> void:
	%LynxBossBase.process_mode = Node.PROCESS_MODE_INHERIT
	%LynxBossBase.active = true
	queue_free()
