using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobby
{
    public class PermanentUpgradeGenerator : Singleton<PermanentUpgradeGenerator>
    {
        [SerializeField]
        PermanentUpgrade _prefab;
        [SerializeField]
        RectTransform _container;
        [SerializeField]
        TMPro.TextMeshProUGUI _credit;

        public void Initialize()
        {
            Initialize(this);
            UpdateInterface();
        }

        void ClearUpgrade()
        {
            while(_container.childCount > 0)
                DestroyImmediate(_container.GetChild(0).gameObject);
        }

        void InstantiateUpgrades()
        {
            void InstantiateUpgrade(Upgrades.IUpgrade upgradeInformation)
            {
                var upgrade = Instantiate(_prefab, _container);
                upgrade.Fix(upgradeInformation);
            }

            InstantiateUpgrade(new Upgrades.IncreaseHp());
            InstantiateUpgrade(new Upgrades.IncreaseMoveSpeed());
            InstantiateUpgrade(new Upgrades.IncreaseCreditBonus());
            InstantiateUpgrade(new Upgrades.IncreaseChoiceCount());
        }

        public void UpdateInterface()
        {
            ClearUpgrade();

            InstantiateUpgrades();
            _credit.text = $"{Data.GameData.instance.credit.ToString()} Credit";
        }
    }
}