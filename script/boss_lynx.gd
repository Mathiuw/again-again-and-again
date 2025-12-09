extends Node2D

@export_group("Line Settings")
@export var line_distance: float = 10

@onready var _health: Health = $HealthComponent
@onready var line_2d: Line2D = $Line2D
@onready var zemerlin: Zemerlin = $Zemerlin

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	# Die function connect
	_health.on_die.connect(func(): queue_free())

func _draw() -> void:
	pass

func _physics_process(_delta: float) -> void:
	var points_count: int = line_2d.get_point_count()
	
	line_2d.set_point_position(0, zemerlin.global_position)
	
	for point in points_count:
		if  point == 0: continue
		line_2d.points[point] = ConstrainDistance(line_2d.points[point], line_2d.points[point-1], line_distance)

func ConstrainDistance(point:Vector2, anchor:Vector2, distance: float) -> Vector2:
	return ((point - anchor).normalized() * distance) + anchor

func damage(damageAmount: int):
	_health.remove_health(damageAmount)
