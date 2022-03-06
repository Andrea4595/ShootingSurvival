using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ObjectPool<T> : Singleton<ObjectPool<T>> where T : MonoBehaviour
    {
        Transform _container;
        T _poolingObjectPrefab;
        Queue<T> _poolingObjectQueue = new Queue<T>();
        
        protected void Initialize(Transform container, int initCount, T poolingObjectPrefab)
        {
            _container = container;
            _poolingObjectPrefab = poolingObjectPrefab;

            for (int i = 0; i < initCount; i++)
                _poolingObjectQueue.Enqueue(CreateNewObject());
        }

        T CreateNewObject()
        {
            var newObj = Instantiate(_poolingObjectPrefab);
            newObj.gameObject.SetActive(false);
            newObj.transform.SetParent(_container);

            return newObj;
        }

        public T GetObject()
        {
            if (_poolingObjectQueue.Count > 0)
            {
                var obj = _poolingObjectQueue.Dequeue();
                obj.gameObject.SetActive(true);

                return obj;
            }
            else
            {
                var newObj = CreateNewObject();
                newObj.gameObject.SetActive(true);
                return newObj;
            }
        }

        public void ReturnObject(T obj)
        {
            obj.gameObject.SetActive(false);
            _poolingObjectQueue.Enqueue(obj);
        }
    }
}