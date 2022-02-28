using UnityEngine;

namespace Game
{
    public class ExplosionObjectPool : ObjectPool<Weapon.Explosion>
    {
        [SerializeField]
        Weapon.Explosion _prefab;

        private void Awake() => Initialize(transform, 50, _prefab);
    }
}