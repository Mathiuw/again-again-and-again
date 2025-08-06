using UnityEngine;
using Pathfinding;

public class AnimationManager_Zehmerlin : AnimationManager_Base
{
    private AIPath _aiPath;

    private void Awake()
    {
        _aiPath = GetComponent<AIPath>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_aiPath.velocity.y > 0)
        {
            ChangeAnimation("Zehmerlin_Walk_Back");
        }
        else if (_aiPath.velocity.y < 0)
        {
            ChangeAnimation("Zehmerlin_Walk_Front");
        }
        else if (_aiPath.velocity.x > 0)
        {
            ChangeAnimation("Zehmerlin_Walk_Right");
        }
        else if (_aiPath.velocity.x < 0)
        {
            ChangeAnimation("Zehmerlin_Walk_Right");
        }
        else
        {
            ChangeAnimation("Zehmerlin_Idle");
        }
    }
}
