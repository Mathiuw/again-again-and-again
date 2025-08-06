using Pathfinding;
using UnityEngine;

[SelectionBase]
public class AI_Base : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] protected bool _followPlayer = false;
    protected Transform _player;
    protected AIDestinationSetter _destinationSetter;

    protected void Awake()
    {
        Health health = GetComponent<Health>();
        if (health)
        {
            health.OnDie += OnDie;
        }
    }

    protected void Start()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        if (player)
        {
            _player = player;
        }

        if (_followPlayer)
        {
            _destinationSetter = GetComponent<AIDestinationSetter>();
            if (_destinationSetter)
            {
                _destinationSetter.target = player;
            }
        }
    }

    private void OnDie()
    {
        Destroy(gameObject);
    }
}
