class_name SoundEffect
extends Resource

enum SOUND_EFFECT_TYPE
{
	HIT,
	SHOOT
}

@export_range(0, 10) var limit: int = 5 ## Maximum number of this SoundEffect to play
@export var type: SOUND_EFFECT_TYPE ## The unique sound effect in the SOUND_EFFECT_TYPE enum
@export var sound_effects: Array[AudioStream] ## The MP3 audio resource to play
@export_range(-40, 20) var volume: float = 0 ## Audio volume
@export_range(0.0, 4.0, .01) var pitch_scale: float = 1.0 ## The pitch scale of the sound effect
@export_range(0.0, 1.0, .01) var pitch_randomness: float = 0.0 ## The pitch randomness of the sound effect 

var audio_count: int = 0 ## Amount of instaces of the audio

func change_audio_count(amount: int) -> void:
	audio_count = max(0, audio_count + amount)

func has_open_limit() -> bool:
	return audio_count < limit

func on_audio_finished() -> void:
	change_audio_count(-1)
