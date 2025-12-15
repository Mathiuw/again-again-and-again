class_name ItemTimer
extends ItemBase

var time_value_base: float = 10

func add_time_to_loop(timer: LoopTimer) -> void:
	timer.addTime(time_value_base)
	pass


#TODO: calculate the time to give based on the current time value
func calculate_time_to_give() -> float:
	return 0 
