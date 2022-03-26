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

        public void ShowIncreaseHp() => _interface.Show(information.increaseHp, "체력");
        public void ShowIncreaseDamage() => _interface.Show(information.increaseDamage, "공격력");
        public void ShowIncreaseMoveSpeed() => _interface.Show(information.increaseMoveSpeed, "이동 속도");
        public void ShowIncreaseCreditBonus() => _interface.Show(information.increaseCreditBonus, "크레딧 보너스");
        public void ShowIncreaseChoiceCount() => _interface.Show(information.increaseChoiceCount, "선택지 개수");
    }
}