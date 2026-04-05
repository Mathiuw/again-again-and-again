using UnityEngine;
using Pathfinding;

public class ZemerlinAnimator : MonoBehaviour
{
    private AIPath aiPath;
    private Animator animator;

    private int walkFrontHash = Animator.StringToHash("Zehmerlin_Walk_Back");
    private int walkLeftHash = Animator.StringToHash("Zehmerlin_Walk_Back");
    private int walkRightHash = Animator.StringToHash("Zehmerlin_Walk_Back");
    private int walkBackHash = Animator.StringToHash("Zehmerlin_Walk_Back");
    private int walkIdleHash = Animator.StringToHash("Zehmerlin_Idle");

    private void Awake()
    {
        aiPath = GetComponent<AIPath>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (aiPath.velocity.y > 0)
        {
            animator.Play(walkBackHash);
        }
        else if (aiPath.velocity.y < 0)
        {
            animator.Play(walkFrontHash);
        }
        else if (aiPath.velocity.x > 0)
        {
            animator.Play(walkRightHash);
        }
        else if (aiPath.velocity.x < 0)
        {
            animator.Play(walkLeftHash);
        }
        else
        {
            animator.Play(walkIdleHash);
        }
    }
}
