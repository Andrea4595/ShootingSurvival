using UnityEngine;

namespace Game
{
    public class CharacterObjectPool : ObjectPool<Character>
    {
        [SerializeField]
        Character _prefab;

        private void Awake() => Initialize(transform, 50, _prefab);
    }
}