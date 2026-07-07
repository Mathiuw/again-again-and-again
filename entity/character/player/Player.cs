using Godot;

namespace AAA;

public partial class Player : CharacterBody2D
{
    // Signals
    [Signal] public delegate void OnPlayerDieEventHandler();
    [Signal] public delegate void OnPlayerDamageEventHandler();

    private const float MoveSpeed = 115.0f;
    
    // Components
    private AnimatedSprite2D _animatedSprite2D;
    private Node2D _weapon;
    private Area2D _interactComponent;
    private Node _dashComponent;

    // knockback variables
    private Vector2 _knockbackDirection = Vector2.Zero;
    private float _knockbackDuration = 0.0f;

    // damage variables
    [Export] private int _invincibleFramesAmount = 15;
    private bool _canTakeDamage = true;
    
    private Timer _loopTimer;
    
    public override void _Ready()
    {
        _loopTimer = GetTree().GetFirstNodeInGroup("loop_timer") as Timer;
        if (_loopTimer != null)
        {
            _loopTimer.Timeout += LoopTimerTimeout;
        }
        else GD.PushWarning("No loop timer found in scene tree!");
        
        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _interactComponent = GetNode<Area2D>("InteractComponent");
        _weapon = GetNode<Node2D>("Weapon");
        _dashComponent = GetNode<Node>("DashComponent");
    }

    public override void _ExitTree()
    {
        if (_loopTimer != null)
        {
            _loopTimer.Timeout -= LoopTimerTimeout;
        }
    }

    private void LoopTimerTimeout()
    {
        throw new System.NotImplementedException();
    }

    private void SetMoveState(bool state)
    {
        SetPhysicsProcess(state);
        SetProcess(state);
        SetPlayerIdleAnimation();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("interact"))
        {
            base._UnhandledInput(@event);
        }
    }

    public override void _Process(double delta)
    {
        if (Velocity.Length() > 0)
        {
            base._Process(delta);
        }
    }

    private void SetPlayerAnimation(Vector2 desiredDirection)
    {
        switch (desiredDirection)
        {
            case { Y: 0, X: > 0 }:
                _animatedSprite2D.Play("walk_right");
                break;
            case { Y: 0, X: < 0 }:
                _animatedSprite2D.Play("walk_left");
                break;
            case { Y: > 0 }:
                _animatedSprite2D.Play("walk_front");
                break;
            case { Y: < 0 }:
                _animatedSprite2D.Play("walk_back");
                break;
            default:
                SetPlayerIdleAnimation();
                break;
        }
    }
    
    private void SetPlayerIdleAnimation()
    {
        switch (_animatedSprite2D.Animation)
        {
            case "walk_back": 
                _animatedSprite2D.Play("idle_back");
                return;
            case "walk_front":
                _animatedSprite2D.Play("idle_front");
                return;
            case "walk_right":
                _animatedSprite2D.Play("idle_right");
                return;
            case "walk_left":
                _animatedSprite2D.Play("idle_left");
                return;
            default:
                _animatedSprite2D.Play("idle");
                return;
        }
    }
    
    
}