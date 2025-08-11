extends Control

@onready var progress_bar: ProgressBar = $Timer/ProgressBar
@onready var hearts: TextureRect = $Hearts

@onready var _loop_timer: LoopTimer
@onready var _player_health: Health

func _ready() -> void:
	_loop_timer = get_tree().get_first_node_in_group("loop_timer")
	if  _loop_timer:
		progress_bar.max_value = _loop_timer.wait_time
	else:
		push_error("HUD: cant find loop timer")
		return
	
	var player = get_tree().get_first_node_in_group("player")
	if player:
		_player_health = player.get_node("%Health")
		if _player_health:
			await _player_health.ready
			_player_health.on_health_changed.connect(func(current_hits:int):
				_set_health_UI(current_hits)
			)
		else:
			push_error("HUD: cant find player health")
			return
		
		_set_health_UI(_player_health._current_hits)

func _process(_delta: float) -> void:
	if _loop_timer:
		progress_bar.value = _loop_timer.time_left

func _set_health_UI(current_hits: int):
	var atlas_texture: AtlasTexture = hearts.texture as AtlasTexture
	
	if !atlas_texture:
		return
	
	match current_hits:
		0: atlas_texture.region.position = Vector2(atlas_texture.get_size().x * 10, 0)
		1: atlas_texture.region.position = Vector2(atlas_texture.get_size().x * 8, 0)
		2: atlas_texture.region.position = Vector2(atlas_texture.get_size().x * 6, 0)
		3: atlas_texture.region.position = Vector2(atlas_texture.get_size().x * 4, 0)
		4: atlas_texture.region.position = Vector2(atlas_texture.get_size().x * 2, 0)
		5: atlas_texture.region.position = Vector2(atlas_texture.get_size().x * 0, 0)
		_: atlas_texture.region.position = Vector2(atlas_texture.get_size().x * 10, 0)
	pass
