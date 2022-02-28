using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class StageUpgradeChoice : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _name;
        [SerializeField]
        TextMeshProUGUI _content;

        public event Action onSelect;

        public void Initialize(string key)
        {
            _content.text = "";

            switch (key)
            {
                case "increaseHp":
                    HpUpgradeInitialize();
                    return;
                case "increaseMoveSpeed":
                    MoveSpeedUpgradeInitialize();
                    return;
                case "increaseCredit":
                    CreditUpgradeInitialize();
                    return;
                case "heal":
                    HealInitialize();
                    return;
                case "credit":
                    CreditInitialize();
                    return;
            }

            if (key.Substring(0, 2).CompareTo("w_") != 0)
            {
                Debug.LogError($"no upgrade key named {key}");
                return;
            }

            var weaponKey = key.Substring(2);
            WeaponUpgradeInitialize(weaponKey);
        }

        public void Select()
        {
            onSelect?.Invoke();
            StageUpgradeSelector.instance.Close();
        }

        void HpUpgradeInitialize()
        {
            var gameData = Data.GameData.instance;
            var player = PlayerSetter.instance.player;

            _name.text = $"체력 증가 {gameData.stageUpgradeLevel.increaseHp + 1}";

            var baseHp = gameData.GetCharacterData("player").maxHp;
            var permanentIncreasedHp = gameData.permanentUpgrades.increaseHp[gameData.permanentUpgradeLevel.increaseHp].power;
            var stageIncreasedHp = gameData.stageUpgrades.increaseHp.power[gameData.stageUpgradeLevel.increaseHp];
            var nextStageIncreasedHp = gameData.stageUpgrades.increaseHp.power[gameData.stageUpgradeLevel.increaseHp + 1];
            var nowHp = baseHp + permanentIncreasedHp + stageIncreasedHp;
            var nextHp = baseHp + permanentIncreasedHp + nextStageIncreasedHp;
            _content.text = $"체력 : {nowHp} → {nextHp}";

            onSelect += () =>
            {
                player.health.SetMaxHp(nextHp);
                gameData.stageUpgradeLevel.increaseHp++;
            };
        }

        void MoveSpeedUpgradeInitialize()
        {
            var gameData = Data.GameData.instance;
            var player = PlayerSetter.instance.player;

            _name.text = $"이동 속도 증가 {gameData.stageUpgradeLevel.increaseMoveSpeed + 1}";

            var baseMoveSpeed = gameData.GetCharacterData("player").moveSpeed;
            var permanentIncreasedMoveSpeed = gameData.permanentUpgrades.increaseMoveSpeed[gameData.permanentUpgradeLevel.increaseMoveSpeed].power;
            var stageIncreasedMoveSpeed = gameData.stageUpgrades.increaseMoveSpeed.power[gameData.stageUpgradeLevel.increaseMoveSpeed];
            var nextStageIncreasedMoveSpeed = gameData.stageUpgrades.increaseMoveSpeed.power[gameData.stageUpgradeLevel.increaseMoveSpeed + 1];
            var nowMoveSpeed = baseMoveSpeed * (1 + permanentIncreasedMoveSpeed + stageIncreasedMoveSpeed);
            var nextMoveSpeed = baseMoveSpeed * (1 + permanentIncreasedMoveSpeed + nextStageIncreasedMoveSpeed);
            _content.text = $"이동 속도 : {nowMoveSpeed} → {nextMoveSpeed}";

            onSelect += () =>
            {
                player.movement.moveSpeed = nextMoveSpeed;
                gameData.stageUpgradeLevel.increaseMoveSpeed++;
            };
        }

        void CreditUpgradeInitialize()
        {
            var gameData = Data.GameData.instance;
            var player = PlayerSetter.instance.player;

            _name.text = $"크레딧 보너스 증가 {gameData.stageUpgradeLevel.increaseCredit + 1}";

            var permanentIncreasedCredit = gameData.permanentUpgrades.increaseCredit[gameData.permanentUpgradeLevel.increaseCredit].power;
            var stageIncreasedCredit = gameData.stageUpgrades.increaseCredit.power[gameData.stageUpgradeLevel.increaseCredit];
            var nextStageIncreasedCredit = gameData.stageUpgrades.increaseCredit.power[gameData.stageUpgradeLevel.increaseCredit + 1];
            var nowCredit = permanentIncreasedCredit + stageIncreasedCredit;
            var nextCredit = permanentIncreasedCredit + nextStageIncreasedCredit;
            _content.text = $"크레딧 보너스 : {nowCredit * 100}% → {nextCredit * 100}%";
            
            onSelect += () =>
            {
                //TODO : Credit Bonus
                gameData.stageUpgradeLevel.increaseCredit++;
            };
        }

        void HealInitialize()
        {
            _name.text = "회복";
            var gameData = Data.GameData.instance;
            var healAmount = gameData.stageUpgrades.heal.power[0];
            _content.text = $"체력 {healAmount * 100}% 회복";

            onSelect += () => PlayerSetter.instance.player.health.Heal(PlayerSetter.instance.player.health.maxHp * healAmount);
        }

        void CreditInitialize()
        {
            _name.text = "크레딧";
            var gameData = Data.GameData.instance;
            var creditAmount = gameData.stageUpgrades.credit.power[0];
            _content.text = $"크레딧 {creditAmount} 획득";

            onSelect += () =>
            {
                //TODO : get credit
            };
        }

        void WeaponUpgradeInitialize(string key)
        {
            void GetNewWeapon()
            {
                var gameData = Data.GameData.instance;
                var weaponInformation = gameData.GetWeaponData(key);
                var player = PlayerSetter.instance.player;

                _name.text = weaponInformation.name.ToUpper();
                _content.text = weaponInformation.information;

                onSelect += () => player.AddNewWeapon(key);
            }

            var gameData = Data.GameData.instance;
            var weaponInformation = gameData.GetWeaponData(key);
            var level = gameData.stageUpgradeLevel.weapons[key];

            onSelect += () => gameData.stageUpgradeLevel.weapons[key] = level + 1;

            if (level == -1)
            {
                GetNewWeapon();
                return;
            }

            _name.text = $"{weaponInformation.name.ToUpper()} {level + 1}";

            var fixs = weaponInformation.upgrades[level].fixes;
            foreach (var fix in fixs)
            {
                switch(fix.key)
                {
                    case "projectile/sprite":
                        Debug.LogError("Can't change projectile sprite.");
                        break;
                    case "projectile/maxHp":
                        _content.text += $"탄환 체력 : {weaponInformation.projectile.maxHp} → {fix.fixTo}\n";
                        onSelect += () => weaponInformation.projectile.maxHp = fix.fixTo;
                        break;
                    case "projectile/damage":
                        _content.text += $"탄환 공격력 : {weaponInformation.projectile.damage} → {fix.fixTo}\n";
                        onSelect += () => weaponInformation.projectile.damage = fix.fixTo;
                        break;
                    case "projectile/range":
                        _content.text += $"탄환 폭발 범위 : {weaponInformation.projectile.range} → {fix.fixTo}\n";
                        onSelect += () => weaponInformation.projectile.range = fix.fixTo;
                        break;
                    case "projectile/speed":
                        _content.text += $"탄환 이동 속도 : {weaponInformation.projectile.speed} → {fix.fixTo}\n";
                        onSelect += () => weaponInformation.projectile.speed = fix.fixTo;
                        break;
                    case "projectile/scale":
                        _content.text += $"탄환 크기 : {weaponInformation.projectile.scale} → {fix.fixTo}\n";
                        onSelect += () => weaponInformation.projectile.scale = fix.fixTo;
                        break;
                    case "projectile/homming":
                        _content.text += $"탄환 선회력 : {weaponInformation.projectile.homming} → {fix.fixTo}\n";
                        onSelect += () => weaponInformation.projectile.homming = fix.fixTo;
                        break;
                    case "projectile/lifetime":
                        _content.text += $"탄환 비행 시간 : {weaponInformation.projectile.lifetime} → {fix.fixTo}\n";
                        onSelect += () => weaponInformation.projectile.lifetime = fix.fixTo;
                        break;
                    case "fireType":
                        Debug.LogError("Can't change fire type.");
                        break;
                    case "fireCount":
                        _content.text += $"동시 발사 횟수 : {weaponInformation.fireCount} → {fix.fixTo}\n";
                        onSelect += () => weaponInformation.fireCount = (int)fix.fixTo;
                        break;
                    case "angleRange":
                        _content.text += $"동시 발사각 : {weaponInformation.angleRange} → {fix.fixTo}\n";
                        onSelect += () => weaponInformation.angleRange = fix.fixTo;
                        break;
                    case "continuousCount":
                        _content.text += $"연사 횟수 : {weaponInformation.continuousCount} → {fix.fixTo}\n";
                        onSelect += () => weaponInformation.continuousCount = (int)fix.fixTo;
                        break;
                    case "interval":
                        _content.text += $"공격 속도 : {weaponInformation.interval} → {fix.fixTo}\n";
                        onSelect += () => weaponInformation.interval = fix.fixTo;
                        break;
                }
            }
        }
    }
}