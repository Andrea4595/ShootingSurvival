using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class Fix : Item<Fix>
    {
        [SerializeField]
        TMPro.TMP_Dropdown _key;
        [SerializeField]
        TMPro.TMP_InputField _fixTo;

        FixList _fixList;
        Data.Object.WeaponInformation.Upgrade.Fix _fix;

        string[] keyList => Data.Object.WeaponInformation.Upgrade.Fix.keyList;

        public void Initialize(FixList fixList, Data.Object.WeaponInformation.Upgrade.Fix fix)
        {
            _fixList = fixList;
            _fix = fix;

            _key.ClearOptions();
            _key.AddOptions(new List<string>(keyList));
            _key.onValueChanged.AddListener(UpdateKey);
            _key.SetValueWithoutNotify(GetKeyIndex(fix.key));
            _fixTo.onValueChanged.AddListener(UpdateValue);
            _fixTo.SetTextWithoutNotify(fix.fixTo.ToString());
        }

        public void Remove()
        {
            _fixList.RemoveItem(this);
        }

        public void UpdateKey(int index)
        {
            _fix.key = keyList[index];
            _fixList.UpdateFix(this.index, _fix);
        }

        public void UpdateValue(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);
            _fix.fixTo = value;
            _fixList.UpdateFix(this.index, _fix);
        }

        int GetKeyIndex(string key)
        {
            for (var i = 0; i < keyList.Length; i++)
                if (keyList[i].CompareTo(key) == 0)
                    return i;

            return -1;
        }
    }
}