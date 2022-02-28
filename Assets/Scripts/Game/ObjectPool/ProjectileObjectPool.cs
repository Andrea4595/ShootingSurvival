using UnityEngine;

namespace Game
{
    public class ProjectileObjectPool : ObjectPool<Weapon.Projectile>
    {
        [SerializeField]
        Weapon.Projectile _prefab;

        private void Awake() => Initialize(transform, 100, _prefab);
    }
}