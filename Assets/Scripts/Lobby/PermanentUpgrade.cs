using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class PermanentUpgrade : MonoBehaviour
    {
        [SerializeField]
        TMPro.TextMeshProUGUI _name;
        [SerializeField]
        TMPro.TextMeshProUGUI _content;
        [SerializeField]
        TMPro.TextMeshProUGUI _cost;

        [SerializeField]
        Image[] _levels;
        [SerializeField]
        Color _reachedLevel;
        [SerializeField]
        Color _remainingLevel;

        Upgrades.IUpgrade _upgradeInformation;
        Data.UpgradeInformation.PermanentUpgrades.Upgrade _information;

        public void Fix(Upgrades.IUpgrade upgradeInformation)
        {
            _upgradeInformation = upgradeInformation;

            UpdateInterface();
        }

        void UpdateInterface()
        {
            _name.text = _upgradeInformation.GetName();
            _content.text = _upgradeInformation.GetContent();

            _information = _upgradeInformation.information;

            if (_information.level + 1 < _information.levels.Length)
                _cost.text = $"{_information.levels[_information.level + 1].cost.ToString()} Credit";
            else
                _cost.text = "";

            SetLevelIcons();
        }

        void SetLevelIcons()
        {
            var reachedLevel = Mathf.Min(_levels.Length, _information.levels.Length - 1, _information.level);
            var remainingLevel = Mathf.Min(_levels.Length, _information.levels.Length - 1);

            for (int i = 0; i < reachedLevel; i++)
                _levels[i].color = _reachedLevel;

            for (int i = reachedLevel; i < remainingLevel; i++)
                _levels[i].color = _remainingLevel;

            for (int i = remainingLevel; i < _levels.Length; i++)
                _levels[i].gameObject.SetActive(false);
        }

        public void Select()
        {
            if (_information.level >= _information.levels.Length - 1)
                return;

            var gameData = Data.GameData.instance;
            var cost = _information.next.cost;

            if (gameData.credit < cost)
                return;

            gameData.credit -= cost;
            
            _information.level++;
            UpdateInterface();
            PermanentUpgradeGenerator.instance.UpdateInterface();

            gameData.playerData.Save();
        }
    }
}