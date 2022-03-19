using System;
using System.Collections;
using System.Collections.Generic;
using Data.Object;
using UnityEngine;

namespace JsonEditor
{
    public class Stage : Item<Stage>
    {
        [SerializeField]
        TMPro.TextMeshProUGUI _level;
        [SerializeField]
        TMPro.TMP_InputField _reward;
        [SerializeField]
        WaveList _waveList;

        StageList _stageList;
        StageInformation _information;

        public void Initialize(StageList stageList, StageInformation information)
        {
            _stageList = stageList;
            _information = information;

            _reward.onEndEdit.AddListener(UpdateReward);

            _waveList.Initialize(this, information);

            SetLevel(index + 1);
        }

        public void SetLevel(int level)
        {
            _level.text = $"Stage {level}";
            _waveList.SetLevels();
        }

        void UpdateReward(string text)
        {
            var value = ExceptionFilter.TryIntParse(text);
            _information.credit = value;

            UpdateInformation();
        }

        void UpdateInformation()
        {
            _stageList.UpdateInformation(_information, index);
        }

        internal void ShowSpawnInterface(int waveIndex)
        {
            _stageList.ShowSpawnInterface(index, waveIndex);
        }

        internal void UpdateWave(int waveIndex)
        {
            _waveList.UpdateWave(waveIndex);
        }

        public void Remove() => _stageList.RemoveItem(this);
        public void MoveUp() => _stageList.Swap(index, index - 1);
        public void MoveDown() => _stageList.Swap(index, index + 1);

        internal void Sorted()
        {
            _stageList.Sorted();
        }
    }
}