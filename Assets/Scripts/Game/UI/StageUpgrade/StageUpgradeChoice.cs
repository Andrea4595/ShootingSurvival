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

            _name.text = $"ü�� ���� {gameData.stageUpgradeLevel.increaseHp + 1}";

            var baseHp = gameData.GetCharacterData("player").maxHp;
            var permanentIncreasedHp = gameData.permanentUpgrades.increaseHp[gameData.permanentUpgradeLevel.increaseHp].power;
            var stageIncreasedHp = gameData.stageUpgrades.increaseHp.power[gameData.stageUpgradeLevel.increaseHp];
            var nextStageIncreasedHp = gameData.stageUpgrades.increaseHp.power[gameData.stageUpgradeLevel.increaseHp + 1];
            var nowHp = baseHp + permanentIncreasedHp + stageIncreasedHp;
            var nextHp = baseHp + permanentIncreasedHp + nextStageIncreasedHp;
            _content.text = $"ü�� : {nowHp} �� {nextHp}";

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

            _name.text = $"�̵� �ӵ� ���� {gameData.stageUpgradeLevel.increaseMoveSpeed + 1}";

            var baseMoveSpeed = gameData.GetCharacterData("player").moveSpeed;
            var permanentIncreasedMoveSpeed = gameData.permanentUpgrades.increaseMoveSpeed[gameData.permanentUpgradeLevel.increaseMoveSpeed].power;
            var stageIncreasedMoveSpeed = gameData.stageUpgrades.increaseMoveSpeed.power[gameData.stageUpgradeLevel.increaseMoveSpeed];
            var nextStageIncreasedMoveSpeed = gameData.stageUpgrades.increaseMoveSpeed.power[gameData.stageUpgradeLevel.increaseMoveSpeed + 1];
            var nowMoveSpeed = baseMoveSpeed * (1 + permanentIncreasedMoveSpeed + stageIncreasedMoveSpeed);
            var nextMoveSpeed = baseMoveSpeed * (1 + permanentIncreasedMoveSpeed + nextStageIncreasedMoveSpeed);
            _content.text = $"�̵� �ӵ� : {nowMoveSpeed} �� {nextMoveSpeed}";

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

            _name.text = $"ũ���� ���ʽ� ���� {gameData.stageUpgradeLevel.increaseCredit + 1}";

            var permanentIncreasedCredit = gameData.permanentUpgrades.increaseCredit[gameData.permanentUpgradeLevel.increaseCredit].power;
            var stageIncreasedCredit = gameData.stageUpgrades.increaseCredit.power[gameData.stageUpgradeLevel.increaseCredit];
            var nextStageIncreasedCredit = gameData.stageUpgrades.increaseCredit.power[gameData.stageUpgradeLevel.increaseCredit + 1];
            var nowCredit = permanentIncreasedCredit + stageIncreasedCredit;
            var nextCredit = permanentIncreasedCredit + nextStageIncreasedCredit;
            _content.text = $"ũ���� ���ʽ� : {nowCredit * 100}% �� {nextCredit * 100}%";
            
            onSelect += () =>
            {
                //TODO : Credit Bonus
                gameData.stageUpgradeLevel.increaseCredit++;
            };
        }

        void HealInitialize()
        {
            _name.text = "ȸ��";
            var gameData = Data.GameData.instance;
            var healAmount = gameData.stageUpgrades.heal.power[0];
            _content.text = $"ü�� {healAmount * 100}% ȸ��";

            onSelect += () => PlayerSetter.instance.player.health.Heal(PlayerSetter.instance.player.health.maxHp * healAmount);
        }

        void CreditInitialize()
        {
            _name.text = "ũ����";
            var gameData = Data.GameData.instance;
            var creditAmount = gameData.stageUpgrades.credit.power[0];
            _content.text = $"ũ���� {creditAmount} ȹ��";

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
                        _content.text += $"źȯ ü�� : {weaponInformation.projectile.maxHp} �� {fix.fixTo}\n";
                        onSelect += () => weaponInformation.projectile.maxHp = fix.fixTo;
                        break;
                    case "projectile/damage":
                        _content.text += $"źȯ ���ݷ� : {weaponInformation.projectile.damage} �� {fix.fixTo}\n";
                        onSelect += () => weaponInformation.projectile.damage = fix.fixTo;
                        break;
                    case "projectile/range":
                        _content.text += $"źȯ ���� ���� : {weaponInformation.projectile.range} �� {fix.fixTo}\n";
                        onSelect += () => weaponInformation.projectile.range = fix.fixTo;
                        break;
                    case "projectile/speed":
                        _content.text += $"źȯ �̵� �ӵ� : {weaponInformation.projectile.speed} �� {fix.fixTo}\n";
                        onSelect += () => weaponInformation.projectile.speed = fix.fixTo;
                        break;
                    case "projectile/scale":
                        _content.text += $"źȯ ũ�� : {weaponInformation.projectile.scale} �� {fix.fixTo}\n";
                        onSelect += () => weaponInformation.projectile.scale = fix.fixTo;
                        break;
                    case "projectile/homming":
                        _content.text += $"źȯ ��ȸ�� : {weaponInformation.projectile.homming} �� {fix.fixTo}\n";
                        onSelect += () => weaponInformation.projectile.homming = fix.fixTo;
                        break;
                    case "projectile/lifetime":
                        _content.text += $"źȯ ���� �ð� : {weaponInformation.projectile.lifetime} �� {fix.fixTo}\n";
                        onSelect += () => weaponInformation.projectile.lifetime = fix.fixTo;
                        break;
                    case "fireType":
                        Debug.LogError("Can't change fire type.");
                        break;
                    case "fireCount":
                        _content.text += $"���� �߻� Ƚ�� : {weaponInformation.fireCount} �� {fix.fixTo}\n";
                        onSelect += () => weaponInformation.fireCount = (int)fix.fixTo;
                        break;
                    case "angleRange":
                        _content.text += $"���� �߻簢 : {weaponInformation.angleRange} �� {fix.fixTo}\n";
                        onSelect += () => weaponInformation.angleRange = fix.fixTo;
                        break;
                    case "continuousCount":
                        _content.text += $"���� Ƚ�� : {weaponInformation.continuousCount} �� {fix.fixTo}\n";
                        onSelect += () => weaponInformation.continuousCount = (int)fix.fixTo;
                        break;
                    case "interval":
                        _content.text += $"���� �ӵ� : {weaponInformation.interval} �� {fix.fixTo}\n";
                        onSelect += () => weaponInformation.interval = fix.fixTo;
                        break;
                }
            }
        }
    }
}