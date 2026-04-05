using Godot;

public partial class DamageArea : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

    private void OnBodyEntered(Node2D body)
    {
		//Health health = body.GetNode<Health>("/");
	}
}
