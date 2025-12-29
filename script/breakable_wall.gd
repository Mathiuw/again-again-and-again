extends StaticBody2D

@export_group("Tilemap Settings")
@export var timemap_coords: Array[Vector2i]
@export var desired_atlas_coords: Array[Vector2i]
@export var multiple_atlas_coords: bool = false
@export var desired_door: Door
var current_break_index: int = 0

@onready var health_component: Health = $HealthComponent
@onready var wall_layer: TileMapLayer = $WallLayer

#func _ready() -> void:
	#if desired_door:
		#desired_door.open_particle.emitting = false

func damage(damageAmount: int) -> void:
	health_component.remove_health(damageAmount)
	advance_break_state()


func _on_health_component_on_die() -> void:
	if desired_door:
		desired_door.open_particle.emitting = true
	queue_free()


func advance_break_state() -> void:
	if health_component._current_hits == 0:
		return
	
	for coord in timemap_coords:
		wall_layer.set_cell(coord, 0, desired_atlas_coords[current_break_index], 0)
		
		if multiple_atlas_coords:
			current_break_index += 1
	
	if !multiple_atlas_coords:
		current_break_index += 1
