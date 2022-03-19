using System;
using System.Collections;
using System.Collections.Generic;
using Data.Object;
using UnityEngine;

namespace JsonEditor
{
    public class Wave : Item<Wave>
    {
        [SerializeField]
        TMPro.TextMeshProUGUI _title;
        [SerializeField]
        WaveStateList _waveStateList;

        WaveList _waveList;
        StageInformation.Group _group;

        public void Initialize(WaveList waveList, StageInformation.Group group)
        {
            _waveList = waveList;
            _group = group;

            _waveStateList.Initialize(group.spawns);

            SetTitle(index + 1);
        }

        public void SetTitle(int level) => _title.text = $"Wave {level}";

        public void Select() => _waveList.ShowSpawnInterface(index);
        public void Remove() => _waveList.RemoveItem(this);
        public void MoveUp() => _waveList.Swap(index, index - 1);
        public void MoveDown() => _waveList.Swap(index, index + 1);
    }
}