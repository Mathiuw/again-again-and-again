using UnityEngine;

public class MedusaAnimationManager : AnimationManagerBase
{
    [SerializeField] AI_Medusa _aiMedusa;

    public void ShootPellet() 
    {
        _aiMedusa.MedusaShootEvent();
    }
}
