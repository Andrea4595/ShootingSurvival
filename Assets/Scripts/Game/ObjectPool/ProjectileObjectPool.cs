using UnityEngine;

namespace Game
{
    public class ProjectileObjectPool : ObjectPool<Projectile>
    {
        [SerializeField]
        Projectile _prefab;

        private void Awake() => Initialize(transform, 100, _prefab);
    }
}