using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Weapon))]
public class AI_Shooter : AI_Base
{
    [Header("Shooter Settings")]
    [SerializeField] protected float _initialShootDelay = 2f;
    [SerializeField] protected float _shootFrequency = 3f;
    [SerializeField] protected float _randomVariation = 1f;
    protected Weapon _weapon;

    protected void OnEnable()
    {
        _weapon = GetComponent<Weapon>();

        float randomShootFrequency = Random.Range(-_randomVariation, _randomVariation);

        InvokeRepeating("EnemyShoot", _initialShootDelay + randomShootFrequency, _shootFrequency + randomShootFrequency);
    }

    private void OnDisable()
    {
        CancelInvoke("EnemyShoot");
    }

    public virtual void EnemyShoot() 
    {
        _weapon.ShootBullet(_player.position);
    }
}