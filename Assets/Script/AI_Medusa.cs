using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class AI_Medusa : AI_Shooter
{
    MedusaAnimationManager _medusaAnimationManager;

    protected void OnEnable()
    {
        base.OnEnable();

        _medusaAnimationManager = GetComponentInChildren<MedusaAnimationManager>();
    }

    public override void EnemyShoot()
    {
        _medusaAnimationManager.ChangeAnimation("Medusa_Shoot");
    }

    public void MedusaShootEvent() 
    {
        _weapon.ShootBullet(_player.position);
    }
}