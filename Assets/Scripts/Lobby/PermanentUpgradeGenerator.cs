using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobby
{
    public class PermanentUpgradeGenerator : MonoBehaviour
    {
        [SerializeField]
        PermanentUpgrade _prefab;
        [SerializeField]
        RectTransform _container;

        public void Initialize()
        {
            InstantiateUpgrade(new Upgrades.IncreaseHp());
            InstantiateUpgrade(new Upgrades.IncreaseMoveSpeed());
            InstantiateUpgrade(new Upgrades.IncreaseCreditBonus());
            InstantiateUpgrade(new Upgrades.IncreaseChoiceCount());
        }

        void InstantiateUpgrade(Upgrades.IUpgrade upgradeInformation)
        {
            var upgrade = Instantiate(_prefab, _container);
            upgrade.Fix(upgradeInformation);
        }
    }
}