using Godot;

namespace AAA;

[GlobalClass]
public partial class AiChaserBehaviour : Node
{
    [Export] private float MoveSpeed { get; set; } = 100;
    [Export] private NavigationAgent2D NavigationAgent { get; set; }

    private CharacterBody2D _parentCharacterBody2D;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (GetParent() is PhysicsBody2D)
        {
            _parentCharacterBody2D = (CharacterBody2D)GetParent();
        }

        if (NavigationAgent != null)
        {
            NavigationAgent.VelocityComputed += OnVelocityComputed;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (_parentCharacterBody2D == null)
        {
            GD.PushError("parentPhysicsBody2D is null");
            return;
        }

        if (NavigationAgent == null)
        {
            GD.PushError("ChaserNavigationAgent is null");
            return;
        }

        if (NavigationServer2D.MapGetIterationId(NavigationAgent.GetNavigationMap()) == 0) return;

        if (NavigationAgent.IsNavigationFinished()) return;

        Vector2 moveDirection = NavigationAgent.GetNextPathPosition();
        Vector2 newVelocity = _parentCharacterBody2D.GlobalPosition.DirectionTo(moveDirection) * MoveSpeed;

        if (NavigationAgent.AvoidanceEnabled)
        {
            NavigationAgent.Velocity = newVelocity;
        }
        else
        {
            OnVelocityComputed(newVelocity);
        }
    }

    private void OnVelocityComputed(Vector2 safeVelocity)
    {
        _parentCharacterBody2D.Velocity = safeVelocity;
        _parentCharacterBody2D.MoveAndSlide();
    }
}