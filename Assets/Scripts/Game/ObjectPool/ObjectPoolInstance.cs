using UnityEngine;

namespace Game
{
    public class ObjectPoolInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        T _this;

        public bool activated => gameObject.activeSelf;
        public void Destroy() => ObjectPool<T>.ReturnObject(_this);

        private void Awake() => _this = GetComponent<T>();
    }
}