using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class WaveStateList : MonoBehaviour
    {
        [SerializeField]
        WaveState _prefab;
        [SerializeField]
        RectTransform _container;

        public void Initialize(Data.Object.StageInformation.Group.Spawn[] spawns)
        {
            for (var i = 0; i <  _container.childCount; i++)
                Destroy(_container.GetChild(i).gameObject);

            foreach(var spawn in spawns)
                Instantiate(spawn);

            _container.gameObject.SetActive(false);
            _container.gameObject.SetActive(true);
        }

        void Instantiate(Data.Object.StageInformation.Group.Spawn spawn)
        {
            var instance = Instantiate(_prefab);
            instance.transform.SetParent(_container);
            instance.Initialize(spawn);
        }
    }
}