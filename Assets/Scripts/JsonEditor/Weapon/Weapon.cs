using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JsonEditor
{
    public class Weapon : MonoBehaviour
    {
        Data.Object.WeaponInformation _information;

        [SerializeField]
        WeaponList _weaponList;

        [SerializeField]
        TMPro.TMP_InputField _key;
        [SerializeField]
        Toggle _forPlayer;
        [SerializeField]
        SpriteSelector _sprite;
        [SerializeField]
        ToggleInputField _hp;
        [SerializeField]
        TMPro.TMP_InputField _damage;
        [SerializeField]
        ToggleInputField _explosionRange;
        [SerializeField]
        TMPro.TMP_InputField _speed;
        [SerializeField]
        SliderWithInput _scale;
        [SerializeField]
        TMPro.TMP_InputField _homming;

        [SerializeField]
        TMPro.TMP_InputField _lifetime;
        [SerializeField]
        Toggle _hitProjectile;

        [SerializeField]
        TMPro.TMP_Dropdown _fireType;
        [SerializeField]
        TMPro.TMP_InputField _fireCount;
        [SerializeField]
        TMPro.TMP_InputField _angleRange;
        [SerializeField]
        TMPro.TMP_InputField _continuousCount;
        [SerializeField]
        TMPro.TMP_InputField _interval;
        [SerializeField]
        UpgradeList _upgradeList;

        private void Awake() => Initialize();

        void Initialize()
        {
            _key.onValueChanged.AddListener(UpdateKey);
            _forPlayer.onValueChanged.AddListener(UpdateForPlayer);
            _sprite.onValueChanged.AddListener(UpdateSprite);
            _hp.onValueChanged.AddListener(UpdateHp);
            _damage.onValueChanged.AddListener(UpdateDamage);
            _explosionRange.onValueChanged.AddListener(UpdateExplosionRange);
            _speed.onValueChanged.AddListener(UpdateSpeed);
            _scale.onValueChanged.AddListener(UpdateScale);
            _homming.onValueChanged.AddListener(UpdateHomming);
            _lifetime.onValueChanged.AddListener(UpdateLifetime);
            _hitProjectile.onValueChanged.AddListener(UpdateHitProjectile);
            _fireType.onValueChanged.AddListener(UpdateFireType);
            _fireCount.onValueChanged.AddListener(UpdateFireCount);
            _angleRange.onValueChanged.AddListener(UpdateAngleRange);
            _continuousCount.onValueChanged.AddListener(UpdateContinuousCount);
            _interval.onValueChanged.AddListener(UpdateInterval);
            _upgradeList.Initialize(this);
        }

        public void UpdateInterface(Data.Object.WeaponInformation information)
        {
            gameObject.SetActive(true);

            _information = information;

            _key.SetTextWithoutNotify(information.key);
            _forPlayer.SetIsOnWithoutNotify(information.forPlayer);
            _sprite.SetValueWithoutNotify(information.projectile.sprite);
            _hp.SetValueWithoutNotify(information.projectile.maxHp);
            _damage.SetTextWithoutNotify(information.projectile.damage.ToString());
            _explosionRange.SetValueWithoutNotify(information.projectile.range);
            _speed.SetTextWithoutNotify(information.projectile.speed.ToString());
            _scale.SetValueWithoutNotify(information.projectile.scale);
            _homming.SetTextWithoutNotify(information.projectile.homming.ToString());
            _lifetime.SetTextWithoutNotify(information.projectile.lifetime.ToString());
            _hitProjectile.SetIsOnWithoutNotify(information.projectile.hitProjectile);
            _fireType.SetValueWithoutNotify((int)information.GetFireType());
            _fireCount.SetTextWithoutNotify(information.fireCount.ToString());
            _angleRange.SetTextWithoutNotify(information.angleRange.ToString());
            _continuousCount.SetTextWithoutNotify(information.continuousCount.ToString());
            _interval.SetTextWithoutNotify(information.interval.ToString());
            _upgradeList.UpdateInterface(information.upgrades);
        }

        public void HideInterface() => gameObject.SetActive(false);

        public bool CheckKey(string key) => _information.key.CompareTo(key) == 0;

        public void UpdateKey(string text)
        {
            var from = _information.key;
            _information.key = text;

            UpdateInformation();
            SaveJsonData.instance.UpdateReferencedWeaponKey(from, _information.key);
            _weaponList.UpdateInterface();
        }

        public void UpdateForPlayer(bool toggle)
        {
            _information.forPlayer = toggle;

            UpdateInformation();
        }

        public void UpdateSprite(int index)
        {
            _information.projectile.sprite = Data.SpriteInformer.instance.sprites[index].key;

            UpdateInformation();
            _weaponList.UpdateInterface();
        }

        public void UpdateHp(float value)
        {
            _information.projectile.maxHp = value;

            UpdateInformation();
        }

        public void UpdateDamage(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);
            _information.projectile.damage = value;

            UpdateInformation();
        }

        public void UpdateExplosionRange(float value)
        {
            _information.projectile.range = value;

            UpdateInformation();
        }

        public void UpdateSpeed(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);
            _information.projectile.speed = value;

            UpdateInformation();
        }

        public void UpdateScale(float value)
        {
            _information.projectile.scale = value;

            UpdateInformation();
        }

        public void UpdateHomming(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);
            _information.projectile.homming = value;

            UpdateInformation();
        }

        public void UpdateLifetime(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);
            _information.projectile.lifetime = value;

            UpdateInformation();
        }

        public void UpdateHitProjectile(bool toggle)
        {
            _information.projectile.hitProjectile = toggle;

            UpdateInformation();
        }

        public void UpdateFireType(int index)
        {
            _information.SetFireType((Data.Object.WeaponInformation.Type)index);

            UpdateInformation();
        }

        public void UpdateFireCount(string text)
        {
            var value = ExceptionFilter.TryIntParse(text);
            _information.fireCount = value;

            UpdateInformation();
        }

        public void UpdateAngleRange(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);
            _information.angleRange = value;

            UpdateInformation();
        }

        public void UpdateContinuousCount(string text)
        {
            var value = ExceptionFilter.TryIntParse(text);
            _information.continuousCount = value;

            UpdateInformation();
        }

        public void UpdateInterval(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);
            _information.interval = value;

            UpdateInformation();
        }

        public void UpdateUpgrade(Data.Object.WeaponInformation.Upgrade[] upgrades)
        {
            _information.upgrades = upgrades;

            UpdateInformation();
        }

        void UpdateInformation()
        {
            Data.GameData.instance.SetWeaponData(_information);
            TestPlayer.instance.UpdateWeaponInformation(_information.Clone());
            SaveJsonData.instance.SaveWeaponIfAuto();
        }

        internal void SelectUpgradeLevel(int level)
        {
            TestPlayer.instance.UpdateWeaponLevel(_information.key, level);
        }
    }
}