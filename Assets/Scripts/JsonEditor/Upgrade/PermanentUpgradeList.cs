using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace JsonEditor
{
    public class PermanentUpgradeList : MonoBehaviour
    {
        [SerializeField]
        PermanentUpgradeInterface _interface;

        UpgradeInformation.PermanentUpgrades information => GameData.instance.permanentUpgrades;

        public void ShowIncreaseHp() => _interface.Show(information.increaseHp, "Increase Hp Upgrade");
        public void ShowIncreaseMoveSpeed() => _interface.Show(information.increaseMoveSpeed, "Increase Move Speed Upgrade");
        public void ShowIncreaseCreditBonus() => _interface.Show(information.increaseCreditBonus, "Increase Credit Bonus Upgrade");
        public void ShowIncreaseChoiceCount() => _interface.Show(information.increaseChoiceCount, "Increase Choice Count Upgrade");
    }
}