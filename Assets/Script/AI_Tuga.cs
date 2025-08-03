using UnityEngine;

public class AI_Tuga : AI_Shooter
{
    [Header("Tuga Settings")]
    [SerializeField] Transform[] shootPositions;
    TugaAnimationManager _tugaAnimationManager;

    protected void OnEnable()
    {
        base.OnEnable();

        _tugaAnimationManager = GetComponentInChildren<TugaAnimationManager>();
    }

    public override void EnemyShoot()
    {
        _tugaAnimationManager.ChangeAnimation("tuga_shoot");
    }

    public void TugaAnimationEvent() 
    {
        foreach (Transform shootPosition in shootPositions)
        {
            _weapon.ShootBullet(_player.position, shootPosition);
        }
    }
}
