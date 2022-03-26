using System;
using Data;
using UnityEngine;

namespace Game.UI.StageUpgrade
{
    public class UpgradeWeapon : IUpgrade
    {
        string _name;
        string _content;

        public UpgradeInformation.StageUpgrades.Upgrade upgrade => null;

        event Action _onUpgrade;

        public UpgradeWeapon(Data.Object.WeaponInformation information)
        {
            void GetNewWeapon()
            {
                var gameData = GameData.instance;
                var player = PlayerSetter.instance.player;

                _name = information.name.ToUpper();
                _content = information.information;

                _onUpgrade += () => player.AddNewWeapon(information.Clone());
            }

            var gameData = GameData.instance;
            var level = gameData.stageUpgrades.weaponLevels[information.key];

            _onUpgrade += () => gameData.stageUpgrades.weaponLevels[information.key] = level + 1;

            if (level == -1)
            {
                GetNewWeapon();
                return;
            }

            var playerWeaponInformation = PlayerSetter.instance.player.GetWeapon(information.key).information;

            string levelText;
            if (level + 1 >= playerWeaponInformation.upgrades.Length)
                levelText = "MAX";
            else
                levelText = (level + 1).ToString();

            _name = $"{information.name.ToUpper()} {levelText}";
            var fixs = playerWeaponInformation.upgrades[level].fixes;
            foreach (var fix in fixs)
            {
                _content += WeaponUpgrader.GetContentText(playerWeaponInformation, fix.key, fix.fixTo);
                _onUpgrade += () => WeaponUpgrader.Upgrade(ref playerWeaponInformation, fix.key, fix.fixTo);
            }
        }

        public string GetName() => _name;
        public string GetContent() => _content;
        public void Upgrade() => _onUpgrade?.Invoke();
    }
}
