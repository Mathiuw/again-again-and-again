using UnityEngine;

namespace MaiNull
{
    public interface IDamageable
    {
        public void Damage(int damage, Transform Instigator);

    }
}