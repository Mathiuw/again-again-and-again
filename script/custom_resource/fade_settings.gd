extends Resource
class_name FadeSettings

enum FadeType {FadeIn, FadeOut}

@export var fade_color: Color = Color.WHITE
@export var fade_type: FadeType = FadeType.FadeIn
@export var fade_duration: float = 0.5
@export var destroy_on_end: bool = false
