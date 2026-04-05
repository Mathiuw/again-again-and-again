using UnityEngine;


public class TugaAnimator : MonoBehaviour
{
    [SerializeField] protected Tuga _aiTuga;

    public void TugaShootAnimationEvent() 
    {
        _aiTuga.TugaAnimationEvent();
    }

    public void ShootTarget()
    {
        _tugaAnimationManager.ChangeAnimation("tuga_shoot");
    }

    public void TugaAnimationEvent()
    {
        foreach (Transform shootPosition in shootPositions)
        {
            weapon.Shoot(target.position, shootPosition);
        }
    }
}
