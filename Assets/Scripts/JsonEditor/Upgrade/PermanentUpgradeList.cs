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

        public void ShowIncreaseHp() => _interface.Show(information.increaseHp, "Hp");
        public void ShowIncreaseDamage() => _interface.Show(information.increaseDamage, "Damage");
        public void ShowIncreaseMoveSpeed() => _interface.Show(information.increaseMoveSpeed, "Move Speed");
        public void ShowIncreaseCreditBonus() => _interface.Show(information.increaseCreditBonus, "Credit Bonus");
        public void ShowIncreaseChoiceCount() => _interface.Show(information.increaseChoiceCount, "Choice Count");
    }
}