using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerSetter : Singleton<PlayerSetter>
    {
        public Character.Character player;

        private void Awake()
        {
            SetPlayer();
        }

        void SetPlayer()
        {
            void InitializeState()
            {
                player.health.SetMaxHp(player.information.maxHp + Data.GameData.instance.permanentUpgrades.increaseHp.current.power);
                player.damageMultiply = 1 + Data.GameData.instance.permanentUpgrades.increaseDamage.current.power;
                player.movement.moveSpeed = player.information.moveSpeed + Data.GameData.instance.permanentUpgrades.increaseMoveSpeed.current.power;
            }

            player.Initialize("player", Character.Character.Force.Player);
            player.health.onDie += GameOver;

            InitializeState();
        }

        void GameOver()
        {
            StageSpawner.instance.GameOver();
        }
    }
}