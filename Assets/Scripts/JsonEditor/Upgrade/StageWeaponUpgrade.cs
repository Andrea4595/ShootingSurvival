using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class StageWeaponUpgrade : Item<StageWeaponUpgrade>
    {
        [SerializeField]
        TMPro.TextMeshProUGUI _title;
        [SerializeField]
        TMPro.TextMeshProUGUI _rate;

        StageUpgradeList _upgradeList;
        Data.Object.WeaponInformation _weapon;

        public void Initialize(StageUpgradeList upgradeList, Data.Object.WeaponInformation weapon, string rateText)
        {
            _upgradeList = upgradeList;
            _weapon = weapon;
            _title.text = $"Weapon: {weapon.key}";
            _rate.text = rateText;
        }

        public void Show() => _upgradeList.ShowWeaponWeight(_weapon);
    }
}