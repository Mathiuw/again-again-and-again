using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class AIShooterBehaviour : AIChaserBehaviour
{
    [Header("Shooter Settings")]
    [SerializeField] private float initialShootDelay = 2f;
    [SerializeField] private float shootFrequency = 3f;
    [SerializeField] private float randomVariation = 1f;
    private Transform target;
    protected Weapon weapon;

    private void Awake()
    {
        weapon = GetComponent<Weapon>();
    }

    protected void OnEnable()
    {
        float randomShootFrequency = Random.Range(-randomVariation, randomVariation);

        InvokeRepeating("EnemyShoot", initialShootDelay + randomShootFrequency, shootFrequency + randomShootFrequency);
    }

    private void Start()
    {
        Transform playerTransform = FindFirstObjectByType<Player>()?.transform;
        if (playerTransform)
        {
            target = playerTransform;
        }
    }

    private void OnDisable()
    {
        CancelInvoke("EnemyShoot");
    }

    public void ShootTarget() 
    {
        weapon.Shoot(target.position);
    }
}