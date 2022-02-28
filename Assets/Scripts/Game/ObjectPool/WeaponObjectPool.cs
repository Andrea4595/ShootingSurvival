using UnityEngine;

namespace Game
{
    public class WeaponObjectPool : ObjectPool<Weapon.Weapon>
    {
        [SerializeField]
        Weapon.Weapon _prefab;

        private void Awake() => Initialize(transform, 100, _prefab);
    }
}