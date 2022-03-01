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

                _onUpgrade += () => player.AddNewWeapon(information);
            }

            string GetContentText(string name, float previous, float fixTo) => $"{name} : {previous} ¡æ {fixTo}\n";

            var gameData = Data.GameData.instance;
            var level = gameData.stageUpgrades.weaponLevels[information.key];

            _onUpgrade += () => gameData.stageUpgrades.weaponLevels[information.key] = level + 1;

            if (level == -1)
            {
                GetNewWeapon();
                return;
            }

            _name = $"{information.name.ToUpper()} {level + 1}";

            var language = gameData.language;
            var fixs = information.upgrades[level].fixes;
            foreach (var fix in fixs)
            {
                switch (fix.key)
                {
                    case "projectile/sprite":
                        Debug.LogError("Can't change projectile sprite.");
                        break;
                    case "projectile/maxHp":
                        _content += GetContentText(language.weapon_projectile_maxHp, information.projectile.maxHp, fix.fixTo);
                        _onUpgrade += () => information.projectile.maxHp = fix.fixTo;
                        break;
                    case "projectile/damage":
                        _content += GetContentText(language.weapon_projectile_damage, information.projectile.damage, fix.fixTo);
                        _onUpgrade += () => information.projectile.damage = fix.fixTo;
                        break;
                    case "projectile/range":
                        _content += GetContentText(language.weapon_projectile_range, information.projectile.range, fix.fixTo);
                        _onUpgrade += () => information.projectile.range = fix.fixTo;
                        break;
                    case "projectile/speed":
                        _content += GetContentText(language.weapon_projectile_speed, information.projectile.speed, fix.fixTo);
                        _onUpgrade += () => information.projectile.speed = fix.fixTo;
                        break;
                    case "projectile/scale":
                        _content += GetContentText(language.weapon_projectile_scale, information.projectile.scale, fix.fixTo);
                        _onUpgrade += () => information.projectile.scale = fix.fixTo;
                        break;
                    case "projectile/homming":
                        _content += GetContentText(language.weapon_projectile_homming, information.projectile.homming, fix.fixTo);
                        _onUpgrade += () => information.projectile.homming = fix.fixTo;
                        break;
                    case "projectile/lifetime":
                        _content += GetContentText(language.weapon_projectile_lifetime, information.projectile.lifetime, fix.fixTo);
                        _onUpgrade += () => information.projectile.lifetime = fix.fixTo;
                        break;
                    case "fireType":
                        Debug.LogError("Can't change fire type.");
                        break;
                    case "fireCount":
                        _content += GetContentText(language.weapon_fireCount, information.fireCount, fix.fixTo);
                        _onUpgrade += () => information.fireCount = (int)fix.fixTo;
                        break;
                    case "angleRange":
                        _content += GetContentText(language.weapon_angleRange, information.angleRange, fix.fixTo);
                        _onUpgrade += () => information.angleRange = fix.fixTo;
                        break;
                    case "continuousCount":
                        _content += GetContentText(language.weapon_continuousCount, information.continuousCount, fix.fixTo);
                        _onUpgrade += () => information.continuousCount = (int)fix.fixTo;
                        break;
                    case "interval":
                        _content += GetContentText(language.weapon_interval, information.interval, fix.fixTo);
                        _onUpgrade += () => information.interval = fix.fixTo;
                        break;
                }
            }
        }

        public string GetName() => _name;
        public string GetContent() => _content;
        public void Upgrade() => _onUpgrade?.Invoke();
    }
}
