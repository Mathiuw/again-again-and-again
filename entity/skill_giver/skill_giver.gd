extends Area2D

@export var skill: PackedScene

func _on_body_entered(body: Node2D) -> void:
	if body is Player:
		var skill_instance: SkillBase = skill.instantiate()
		
		for child in body.get_children():
			# check if character has already skill, if has enables it
			if skill_instance == child:
				var existing_skill: SkillBase = child
				existing_skill.enabled = true
				skill_instance.queue_free()
				return
		
		body.add_child(skill_instance)
