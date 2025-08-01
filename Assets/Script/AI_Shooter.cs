using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Weapon))]
public class AI_Shooter : MonoBehaviour
{
    [Header("AI Setteings")]
    [SerializeField] protected float _initialShootDelay = 2f;
    [SerializeField] protected float _shootFrequency = 3f;
    [SerializeField] protected float _randomVariation = 1f;
    private AIDestinationSetter _destinationSetter;
    private Weapon _weaponEnemy;

    private void Awake()
    {
        _destinationSetter = GetComponent<AIDestinationSetter>();
        _weaponEnemy = GetComponent<Weapon>();

        Health health = GetComponent<Health>();
        if (health)
        {
            health.OnDie += OnDie;
        }
    }

    private void OnEnable()
    {
        float randomShootFrequency = Random.Range(-_randomVariation, _randomVariation);

        InvokeRepeating("EnemyShoot", _initialShootDelay + randomShootFrequency, _shootFrequency + randomShootFrequency);
    }

    private void Start()
    {
        Transform player = GameObject.FindWithTag("Player").transform;

        if (!_destinationSetter.target && player)
        {
            _destinationSetter.target = player;
        }
    }

    private void OnDisable()
    {
        CancelInvoke("EnemyShoot");
    }

    public void EnemyShoot() 
    {
        _weaponEnemy.ShootBullet(_destinationSetter.target.transform.position);
    }

    private void OnDie()
    {
        Destroy(gameObject);
    }
}
