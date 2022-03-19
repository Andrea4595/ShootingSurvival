using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class WaveList : ItemList<Wave>
    {
        Stage _stagInterfacee;
        Data.Object.StageInformation _stage;

        public void Initialize(Stage stageInterface, Data.Object.StageInformation stage)
        {
            _stagInterfacee = stageInterface;
            _stage = stage;

            foreach (var group in stage.groups)
                AddItemWithoutUpdate().Initialize(this, group);
        }

        protected override void AddFromButton(Wave item)
        {
            var newGroup = new Data.Object.StageInformation.Group();
            item.Initialize(this, newGroup);

            var list = new List<Data.Object.StageInformation.Group>(_stage.groups);
            list.Add(newGroup);
            _stage.groups = list.ToArray();
        }

        protected override void Remove(Wave item)
        {
            var list = new List<Data.Object.StageInformation.Group>(_stage.groups);
            list.RemoveAt(item.index);
            _stage.groups = list.ToArray();
        }

        internal void SetLevels()
        {
            foreach (var group in _items)
                group.SetTitle(group.index + 1);
        }

        protected override void SwapItems(int indexA, int indexB)
        {
            var temp = _stage.groups[indexA];
            _stage.groups[indexA] = _stage.groups[indexB];
            _stage.groups[indexB] = temp;

            _stagInterfacee.Sorted();
        }

        protected override void UpdateItems() => SaveJsonData.instance.SaveStageIfAuto();

        internal void ShowSpawnInterface(int waveIndex)
        {
            _stagInterfacee.ShowSpawnInterface(waveIndex);
        }

        internal void UpdateWave(int waveIndex)
        {
            _items[waveIndex].Initialize(this, _stage.groups[waveIndex]);
        }
    }
}