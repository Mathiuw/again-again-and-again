extends AnimationPlayer
class_name  CutsceneBase

#var cutscene_border_scene: PackedScene = preload("uid://ce5ld2u2q4it5")

func _on_trigger_boss_area_body_entered(_body: Node2D) -> void:
	%TriggerBossArea.queue_free()
	
	
	print("Cutscene trigged")
	
	#var new_cutscene_border: CutsceneBorder = cutscene_border_scene.instantiate()
	#get_tree().root.add_child(new_cutscene_border)
	#new_cutscene_border.show_border()
	
	#PauseManager.can_pause_input = false
	
	play("boss_intro")
