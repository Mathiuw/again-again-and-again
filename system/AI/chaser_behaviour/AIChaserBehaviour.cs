using Godot;


[GlobalClass]
public partial class AIChaserBehaviour : Node
{
    [Export] private float MoveSpeed { get; set; } = 100;

    [Export] private NavigationAgent2D ChaserNavigationAgent { get; set; }

    private CharacterBody2D parentCharacterBody2D;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (GetParent() is PhysicsBody2D)
        {
            parentCharacterBody2D = (CharacterBody2D)GetParent();
        }

        if (ChaserNavigationAgent != null)
        {
            ChaserNavigationAgent.VelocityComputed += OnVelocityComputed;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (parentCharacterBody2D == null)
        {
            GD.PushError("parentPhysicsBody2D is null");
            return;
        }

        if (ChaserNavigationAgent == null)
        {
            GD.PushError("ChaserNavigationAgent is null");
            return;
        }

        if (NavigationServer2D.MapGetIterationId(ChaserNavigationAgent.GetNavigationMap()) == 0) return;

        if (ChaserNavigationAgent.IsNavigationFinished()) return;

        Vector2 moveDirection = ChaserNavigationAgent.GetNextPathPosition();
        Vector2 newVelocity = parentCharacterBody2D.GlobalPosition.DirectionTo(moveDirection) * MoveSpeed;

        if (ChaserNavigationAgent.AvoidanceEnabled)
        {
            ChaserNavigationAgent.Velocity = newVelocity;
        }
        else
        {
            OnVelocityComputed(newVelocity);
        }
    }

    private void OnVelocityComputed(Vector2 safeVelocity)
    {
        parentCharacterBody2D.Velocity = safeVelocity;
        parentCharacterBody2D.MoveAndSlide();
    }
}
