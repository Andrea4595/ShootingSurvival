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

        public void ShowIncreaseHp() => _interface.Show(information.increaseHp, "ü��");
        public void ShowIncreaseDamage() => _interface.Show(information.increaseDamage, "���ݷ�");
        public void ShowIncreaseMoveSpeed() => _interface.Show(information.increaseMoveSpeed, "�̵� �ӵ�");
        public void ShowIncreaseCreditBonus() => _interface.Show(information.increaseCreditBonus, "ũ���� ���ʽ�");
        public void ShowIncreaseChoiceCount() => _interface.Show(information.increaseChoiceCount, "������ ����");
    }
}