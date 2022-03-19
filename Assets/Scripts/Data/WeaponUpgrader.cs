using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public static class WeaponUpgrader
    {
        static LanguageInformation.Language language => GameData.instance.language;

        public static void Upgrade(ref Object.WeaponInformation information, string upgradeKey, float fixTo)
        {
            switch (upgradeKey)
            {
                case "projectile/maxHp":
                    information.projectile.maxHp = fixTo;
                    break;
                case "projectile/damage":
                    information.projectile.damage = fixTo;
                    break;
                case "projectile/range":
                    information.projectile.range = fixTo;
                    break;
                case "projectile/speed":
                    information.projectile.speed = fixTo;
                    break;
                case "projectile/scale":
                    information.projectile.scale = fixTo;
                    break;
                case "projectile/homming":
                    information.projectile.homming = fixTo;
                    break;
                case "projectile/lifetime":
                    information.projectile.lifetime = fixTo;
                    break;
                case "fireCount":
                    information.fireCount = (int)fixTo;
                    break;
                case "angleRange":
                    information.angleRange = fixTo;
                    break;
                case "continuousCount":
                    information.continuousCount = (int)fixTo;
                    break;
                case "interval":
                    information.interval = fixTo;
                    break;
            }
        }

        public static string GetContentText(Object.WeaponInformation information, string upgradeKey, float fixTo)
        {
            string GetContentText(string name, float previous, float fixTo) => $"{name} : {previous} ¡æ {fixTo}\n";

            switch (upgradeKey)
            {
                case "projectile/maxHp":
                    return GetContentText(language.weapon_projectile_maxHp, information.projectile.maxHp, fixTo);
                case "projectile/damage":
                    return GetContentText(language.weapon_projectile_damage, information.projectile.damage, fixTo);
                case "projectile/range":
                    return GetContentText(language.weapon_projectile_range, information.projectile.range, fixTo);
                case "projectile/speed":
                    return GetContentText(language.weapon_projectile_speed, information.projectile.speed, fixTo);
                case "projectile/scale":
                    return GetContentText(language.weapon_projectile_scale, information.projectile.scale, fixTo);
                case "projectile/homming":
                    return GetContentText(language.weapon_projectile_homming, information.projectile.homming, fixTo);
                case "projectile/lifetime":
                    return GetContentText(language.weapon_projectile_lifetime, information.projectile.lifetime, fixTo);
                case "fireCount":
                    return GetContentText(language.weapon_fireCount, information.fireCount, fixTo);
                case "angleRange":
                    return GetContentText(language.weapon_angleRange, information.angleRange, fixTo);
                case "continuousCount":
                    return GetContentText(language.weapon_continuousCount, information.continuousCount, fixTo);
                case "interval":
                    return GetContentText(language.weapon_interval, information.interval, fixTo);
            }

            return "";
        }
    }
}