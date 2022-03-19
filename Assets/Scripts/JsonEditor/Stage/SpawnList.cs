using System.Collections.Generic;
using Data.Object;
using UnityEngine;

namespace JsonEditor
{
    public class SpawnList : ItemList<Spawn>
    {
        [SerializeField]
        StageList _stageList;

        [SerializeField]
        TMPro.TextMeshProUGUI _title;

        StageInformation.Group _group;
        int _stageIndex;
        int _waveIndex;

        public void ShowInterface(StageInformation.Group group, int stageIndex, int waveIndex)
        {
            gameObject.SetActive(true);

            ClearWithoutUpdate();

            _group = group;
            _stageIndex = stageIndex;
            _waveIndex = waveIndex;

            foreach (var spawn in group.spawns)
                AddItemWithoutUpdate().UpdateInterface(this, spawn);

            _title.text = $"Stage {stageIndex + 1} Wave {waveIndex + 1}";
        }

        public void HideInterface()
        {
            gameObject.SetActive(false);
        }

        protected override void AddFromButton(Spawn item)
        {
            var newSpawn = new StageInformation.Group.Spawn();
            item.UpdateInterface(this, newSpawn);

            var list = new List<StageInformation.Group.Spawn>(_group.spawns);
            list.Add(newSpawn);
            _group.spawns = list.ToArray();
        }

        protected override void Remove(Spawn item)
        {
            var list = new List<StageInformation.Group.Spawn>(_group.spawns);
            list.RemoveAt(item.index);
            _group.spawns = list.ToArray();
        }

        protected override void SwapItems(int indexA, int indexB)
        {
            var temp = _group.spawns[indexA];
            _group.spawns[indexA] = _group.spawns[indexB];
            _group.spawns[indexB] = temp;
        }

        protected override void UpdateItems()
        {
            var stagesData = Data.GameData.instance.GetStagesData();
            stagesData.items[_stageIndex].groups[_waveIndex] = _group;

            UpdateWave();
            SaveJsonData.instance.SaveStageIfAuto();
        }

        internal void UpdateWave()
        {
            _stageList.UpdateWave(_stageIndex, _waveIndex);
        }
    }
}