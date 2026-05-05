using UnityEngine;

namespace MaiNull
{
    [CreateAssetMenu(fileName = "Projectile_Data", menuName = "Projectile Data")]
    public class ProjectileData : ScriptableObject
    {
        public GameObject prefab;
        public float damage = 1f;
        public float bulletSpeed = 100f;
        public float bulletLifeTime = 7f;
    }
}