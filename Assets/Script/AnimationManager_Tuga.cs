using UnityEngine;

public class AnimationManager_Tuga : AnimationManager_Base
{
    [SerializeField] protected AI_Tuga _aiTuga;

    public void TugaShootAnimationEvent() 
    {
        _aiTuga.TugaAnimationEvent();
    }
}
