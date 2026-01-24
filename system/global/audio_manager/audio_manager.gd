extends Node2D

@export var sound_effects: Array[SoundEffect]
var sound_effect_dict: Dictionary = {}

@onready var _music_stream_player: AudioStreamPlayer = $MusicStreamPlayer


func _ready() -> void:
	if _music_stream_player.stream:
		_music_stream_player.play()
	
	for sound_effect : SoundEffect in sound_effects:
		sound_effect_dict[sound_effect.type] = sound_effect


func set_music(new_music: AudioStream):
	if new_music == null:
		_music_stream_player.stop()
		_music_stream_player.stream = null
		print("music stopped")
		return
	
	if new_music == _music_stream_player.stream: 
		if _music_stream_player.stream_paused:
			_music_stream_player.stream_paused = false
		print("same music")
		return
	
	_music_stream_player.stream = new_music
	_music_stream_player.play()
	print("music started")


func create_2d_audio_at_location(location: Vector2, type: SoundEffect.SOUND_EFFECT_TYPE, variant: int = 0) -> void:
	if sound_effect_dict.has(type):
		var sound_effect: SoundEffect = sound_effect_dict[type]
		if sound_effect.has_open_limit():
			sound_effect.change_audio_count(1)
			var new_2d_audio: AudioStreamPlayer2D = AudioStreamPlayer2D.new()
			add_child(new_2d_audio)
			new_2d_audio.bus = "Sfx"
			new_2d_audio.position = location
			new_2d_audio.stream = sound_effect.sound_effects[variant]
			new_2d_audio.volume_db = sound_effect.volume
			new_2d_audio.pitch_scale = sound_effect.pitch_scale
			new_2d_audio.pitch_scale += randf_range(-sound_effect.pitch_randomness, sound_effect.pitch_randomness)
			new_2d_audio.finished.connect(sound_effect.on_audio_finished)
			new_2d_audio.finished.connect(new_2d_audio.queue_free)
			new_2d_audio.play()
	else:
		push_error("AudioManager: failed to find setting for type ", type)


func create_audio(type: SoundEffect.SOUND_EFFECT_TYPE, variant: int = 0) -> void:
	if sound_effect_dict.has(type):
		var sound_effect: SoundEffect = sound_effect_dict[type]
		if sound_effect.has_open_limit():
			sound_effect.change_audio_count(1)
			var new_audio: AudioStreamPlayer = AudioStreamPlayer.new()
			add_child(new_audio)
			new_audio.bus = "Sfx"
			new_audio.stream = sound_effect.sound_effects[variant]
			new_audio.volume_db = sound_effect.volume
			new_audio.pitch_scale = sound_effect.pitch_scale
			new_audio.pitch_scale += randf_range(-sound_effect.pitch_randomness, sound_effect.pitch_randomness)
			new_audio.finished.connect(sound_effect.on_audio_finished)
			new_audio.finished.connect(new_audio.queue_free)
			new_audio.play()
	else:
		push_error("AudioManager: failed to find setting for type ", type)
