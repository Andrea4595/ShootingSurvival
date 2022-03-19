using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class StageUpgradeLevel : Item<StageUpgradeLevel>
    {
        [SerializeField]
        TMPro.TMP_InputField _power;

        StageUpgradeLevelList _levelList;

        private void Awake() => Initialize();

        void Initialize()
        {
            _power.onEndEdit.AddListener(UpdatePower);
        }

        public void UpdateInterface(StageUpgradeLevelList levelList, float power)
        {
            _levelList = levelList;
            _power.SetTextWithoutNotify(power.ToString());
        }

        void UpdatePower(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);
            _levelList.UpdatePower(index, value);
        }

        public void Remove() => _levelList.RemoveItem(this);
    }
}