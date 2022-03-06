using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public abstract class ItemList<T> : MonoBehaviour where T : Item<T>
    {
        [SerializeField]
        T _prefab;
        [SerializeField]
        RectTransform _container;
        [SerializeField]
        RectTransform _addButton;
        [Space]

        protected List<T> _items = new List<T>();

        protected int count => _items.Count;

        public void ClearWithoutUpdate()
        {
            foreach (var item in _items)
                Destroy(item.gameObject);

            _items.Clear();
        }

        public T AddItemWithoutUpdate()
        {
            var item = Instantiate(_prefab);
            InitializeItem(item);

            return item;
        }

        public void AddItemFromButton()
        {
            var item = Instantiate(_prefab);
            InitializeItem(item);
            AddFromButton(item);

            ContainerSort();
            UpdateItems();
        }

        void InitializeItem(T item)
        {
            item.index = _items.Count;
            item.transform.SetParent(_container);

            _items.Add(item);

            _addButton.transform.SetParent(null);
            _addButton.transform.SetParent(_container);

            ContainerSort();
        }

        public void RemoveItem(T item)
        {
            for (int i = item.index + 1; i < _items.Count; i++)
                _items[i].index--;

            Remove(item);
            _items.Remove(item);
            Destroy(item.gameObject);
            ContainerSort();
            UpdateItems();
        }

        public void Swap(int indexA, int indexB)
        {
            if (indexA < 0 || indexA >= _items.Count)
                return;
            
            if (indexB < 0 || indexB >= _items.Count)
                return;

            var temp = _items[indexA];
            _items[indexA] = _items[indexB];
            _items[indexB] = temp;

            _items[indexA].index = indexA;
            _items[indexB].index = indexB;

            foreach (var item in _items)
                item.transform.SetParent(null);
            foreach (var item in _items)
                item.transform.SetParent(_container);

            _addButton.transform.SetParent(null);
            _addButton.transform.SetParent(_container);

            ContainerSort();
            SwapItems(indexA, indexB);
            UpdateItems();
        }

        protected abstract void AddFromButton(T item);
        protected abstract void Remove(T item);
        protected abstract void SwapItems(int indexA, int indexB);
        protected abstract void UpdateItems();

        void ContainerSort()
        {
            _container.gameObject.SetActive(false);
            _container.gameObject.SetActive(true);
        }
    }
}