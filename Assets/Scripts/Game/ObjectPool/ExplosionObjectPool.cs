using UnityEngine;

namespace Game
{
    public class ExplosionObjectPool : ObjectPool<Explosion>
    {
        [SerializeField]
        Explosion _prefab;

        private void Awake() => Initialize(transform, 50, _prefab);
    }
}