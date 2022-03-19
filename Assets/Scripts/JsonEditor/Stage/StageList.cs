using System;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class StageList : ItemList<Stage>
    {
        [SerializeField]
        SpawnList _spawnList;

        Data.Table<Data.Object.StageInformation> stages => Data.GameData.instance.GetStagesData();

        private void Awake() => Initialize();

        public void Initialize()
        {
            ClearWithoutUpdate();

            foreach (var stage in stages.items)
                AddItemWithoutUpdate().Initialize(this, stage);
        }

        public void UpdateInformation(Data.Object.StageInformation information, int index)
        {
            stages.items[index] = information;
            UpdateItems();
        }

        protected override void AddFromButton(Stage item)
        {
            var newInformation = new Data.Object.StageInformation();

            item.Initialize(this, newInformation);

            var list = new List<Data.Object.StageInformation>(stages.items);
            list.Add(newInformation);
            stages.items = list.ToArray();
        }

        internal void SetLevels()
        {
            foreach (var stage in _items)
                stage.SetLevel(stage.index + 1);
        }

        protected override void Remove(Stage item)
        {
            var list = new List<Data.Object.StageInformation>(stages.items);
            list.RemoveAt(item.index);
            stages.items = list.ToArray();
        }

        protected override void SwapItems(int indexA, int indexB)
        {
            var temp = stages.items[indexA];
            stages.items[indexA] = stages.items[indexB];
            stages.items[indexB] = temp;

            Sorted();
        }

        protected override void UpdateItems()
        {
            SaveJsonData.instance.SaveStageIfAuto();
        }

        internal void ShowSpawnInterface(int stageIndex, int waveIndex)
        {
            var group = stages.items[stageIndex].groups[waveIndex];
            _spawnList.ShowInterface(group, stageIndex, waveIndex);
        }

        public void UpdateWave(int stageIndex, int waveIndex)
        {
            _items[stageIndex].UpdateWave(waveIndex);
        }

        public void Sorted()
        {
            _spawnList.HideInterface();
            SetLevels();
        }
    }
}