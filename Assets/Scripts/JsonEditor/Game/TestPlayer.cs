using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class TestPlayer : Singleton<TestPlayer>
    {
        [SerializeField]
        Game.Character.Character _dummy;
        [SerializeField]
        Data.Object.CharacterInformation _dummyInformation;

        Game.Character.Character player => Game.PlayerSetter.instance.player;

        private void Awake()
        {
            _dummy.Initialize(_dummyInformation, Game.Character.Character.Force.Enemy);
            Game.StageSpawner.instance.AddRemain(_dummy);

            StartCoroutine(CInvincible());
        }

        IEnumerator CInvincible()
        {
            while(true)
            {
                yield return null;
                player.health.Heal(10000);
                _dummy.health.Heal(10000);
            }
        }

        public void UpdateCharacterInformation(Data.Object.CharacterInformation characterInformation)
        {
            player.Initialize(characterInformation, Game.Character.Character.Force.Player);
        }

        public void UpdateWeaponInformation(Data.Object.WeaponInformation weaponInformation)
        {
            player.ClearWeapon();
            player.AddNewWeapon(weaponInformation);
        }

        public void UpdateWeaponLevel(string key, int level)
        {
            var information = Data.GameData.instance.GetWeaponInformation(key).Clone();

            UpdateWeaponInformation(information);

            for (var i = 0; i < level; i++)
                foreach (var fix in information.upgrades[i].fixes)
                    Data.WeaponUpgrader.Upgrade(ref information, fix.key, fix.fixTo);
        }
    }
}