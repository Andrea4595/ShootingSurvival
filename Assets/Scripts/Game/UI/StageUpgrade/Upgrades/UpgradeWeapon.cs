using System;
using UnityEngine;

namespace Game.UI.StageUpgrade
{
    public class UpgradeWeapon : IUpgrade
    {
        string _name;
        string _content;
        event Action _onUpgrade;

        public UpgradeWeapon(Data.Object.WeaponInformation information)
        {
            void GetNewWeapon()
            {
                var gameData = Data.GameData.instance;
                var player = PlayerSetter.instance.player;

                _name = information.name.ToUpper();
                _content = information.information;

                _onUpgrade += () => player.AddNewWeapon(information.Clone());
            }

            var gameData = Data.GameData.instance;
            var level = gameData.stageUpgrades.weaponLevels[information.key];

            _onUpgrade += () => gameData.stageUpgrades.weaponLevels[information.key] = level + 1;

            if (level == -1)
            {
                GetNewWeapon();
                return;
            }

            var playerWeaponInformation = PlayerSetter.instance.player.GetWeapon(information.key).information;

            _name = $"{information.name.ToUpper()} {level + 1}";
            var fixs = playerWeaponInformation.upgrades[level].fixes;
            foreach (var fix in fixs)
            {
                _content += Data.WeaponUpgrader.GetContentText(playerWeaponInformation, fix.key, fix.fixTo);
                _onUpgrade += () => Data.WeaponUpgrader.Upgrade(ref playerWeaponInformation, fix.key, fix.fixTo);
            }
        }

        public string GetName() => _name;
        public string GetContent() => _content;
        public void Upgrade() => _onUpgrade?.Invoke();
    }
}
