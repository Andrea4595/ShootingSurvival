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
        TMPro.TMP_InputField _name;
        [SerializeField]
        TMPro.TMP_InputField _informationText;
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
            void UpdateKey(string text)
            {
                var from = _information.key;
                _information.key = text;

                UpdateInformation();
                SaveJsonData.instance.UpdateReferencedWeaponKey(from, _information.key);
                _weaponList.UpdateInterface();
            }

            void UpdateName(string text)
            {
                GetLanguage().name = text;
                SaveJsonData.instance.SaveLanguageIfAuto();
            }

            void UpdateInformationText(string text)
            {
                GetLanguage().information = text;
                SaveJsonData.instance.SaveLanguageIfAuto();
            }

            void UpdateForPlayer(bool toggle)
            {
                _information.forPlayer = toggle;

                UpdateInformation();
            }

            void UpdateSprite(int index)
            {
                _information.projectile.sprite = Data.SpriteInformer.instance.sprites[index].key;

                UpdateInformation();
                _weaponList.UpdateInterface();
            }

            void UpdateHp(float value)
            {
                _information.projectile.maxHp = value;

                UpdateInformation();
            }

            void UpdateDamage(string text)
            {
                var value = ExceptionFilter.TryFloatParse(text);
                _information.projectile.damage = value;

                UpdateInformation();
            }

            void UpdateExplosionRange(float value)
            {
                _information.projectile.range = value;

                UpdateInformation();
            }

            void UpdateSpeed(string text)
            {
                var value = ExceptionFilter.TryFloatParse(text);
                _information.projectile.speed = value;

                UpdateInformation();
            }

            void UpdateScale(float value)
            {
                _information.projectile.scale = value;

                UpdateInformation();
            }

            void UpdateHomming(string text)
            {
                var value = ExceptionFilter.TryFloatParse(text);
                _information.projectile.homming = value;

                UpdateInformation();
            }

            void UpdateLifetime(string text)
            {
                var value = ExceptionFilter.TryFloatParse(text);
                _information.projectile.lifetime = value;

                UpdateInformation();
            }

            void UpdateHitProjectile(bool toggle)
            {
                _information.projectile.hitProjectile = toggle;

                UpdateInformation();
            }

            void UpdateFireType(int index)
            {
                _information.SetFireType((Data.Object.WeaponInformation.Type)index);

                UpdateInformation();
            }

            void UpdateFireCount(string text)
            {
                var value = ExceptionFilter.TryIntParse(text);
                _information.fireCount = value;

                UpdateInformation();
            }

            void UpdateAngleRange(string text)
            {
                var value = ExceptionFilter.TryFloatParse(text);
                _information.angleRange = value;

                UpdateInformation();
            }

            void UpdateContinuousCount(string text)
            {
                var value = ExceptionFilter.TryIntParse(text);
                _information.continuousCount = value;

                UpdateInformation();
            }

            void UpdateInterval(string text)
            {
                var value = ExceptionFilter.TryFloatParse(text);
                _information.interval = value;

                UpdateInformation();
            }

            _key.onValueChanged.AddListener(UpdateKey);
            _name.onValueChanged.AddListener(UpdateName);
            _informationText.onValueChanged.AddListener(UpdateInformationText);
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
            _name.SetTextWithoutNotify(GetLanguage().name);
            _informationText.SetTextWithoutNotify(GetLanguage().information);
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

        Data.LanguageInformation.Language.Weapon GetLanguage()
        {
            Data.LanguageInformation.Language.Weapon language;

            if (Data.GameData.instance.language.GetWeapon(_information.key, out language) == false)
            {
                language = new Data.LanguageInformation.Language.Weapon();
                language.key = _information.key;
                Data.GameData.instance.language.AddWeapon(_information.key, language);
            }

            return language;
        }
    }
}