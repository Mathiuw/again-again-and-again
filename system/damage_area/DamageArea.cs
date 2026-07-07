using Godot;

namespace AAA;

[GlobalClass]
public partial class DamageArea : Area2D
{
	[Export] public Node HealthComponent { get; set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		Variant damage = body.Get("damage");
		
		if (damage.AsBool())
		{
			HealthComponent?.Call("remove_health", damage);
		}
	}
}