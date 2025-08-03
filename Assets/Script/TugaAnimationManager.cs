using UnityEngine;

public class TugaAnimationManager : AnimationManagerBase
{
    [SerializeField] protected AI_Tuga _aiTuga;

    public void TugaShootAnimationEvent() 
    {
        _aiTuga.TugaAnimationEvent();
    }
}
