using UnityEngine;

public class AnimationManager_Medusa : AnimationManager_Base
{
    [SerializeField] AI_Medusa _aiMedusa;

    public void ShootPellet() 
    {
        _aiMedusa.MedusaShootEvent();
    }
}
