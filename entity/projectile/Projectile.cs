using System.Linq;
using Godot;

namespace AAA;

public partial class Projectile : StaticBody2D
{
	private float _bulletSpeed;
	private int _damage;
	private Node _shooter;
	private StringName[] _ignoreGroup;

	public override void _Ready()
	{
		Timer timer = GetNode<Timer>("DestroyTimer");
		if (timer != null)
		{
			timer.Timeout += Timeout;
		}
		else GD.PrintErr("No timer found");
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Vector2 motion = Transform.X * _bulletSpeed * (float)delta;
		KinematicCollision2D collision2D = MoveAndCollide(motion);
		if (collision2D?.GetCollider() is Node2D body)
		{
			CheckCollision(body);
		}
	}

	private void CheckCollision(Node2D body)
	{
		GD.Print(_shooter.GetGroups());

		if (!_shooter.GetGroups().Any(body.IsInGroup)) return;
		
		AddCollisionExceptionWith(body);
		GD.Print("collision ignored");
	}

	public void InitProjectile(float bulletSpeed, int damage, Node shooter)
	{
		_bulletSpeed = bulletSpeed;
		_damage = damage;
		_shooter = shooter;
	}
	
	private void Timeout()
	{
		QueueFree();
	}
}