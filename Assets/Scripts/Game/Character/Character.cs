using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class Character : ObjectPoolInstance<Character>
    {
        public Movement movement;
        public Health health;

        [SerializeField]
        SpriteRenderer _sprite;

        [SerializeField]
        Hitable _hitable;

        Color _color = Color.white;
        public Color color
        {
            get => _color;
            set
            {
                _color = value;
                _sprite.color = _color;
            }
        }

        List<Weapon.Weapon> _weapons = new List<Weapon.Weapon>();

        public enum Force { Player, Enemy }

        public Force force { get; private set; }
        public Data.Object.CharacterInformation information { get; private set; }
        public event Action onDestroy;

        private void Awake()
        {
            ObjectPoolInstanceInitialize(this);
            health.onDie += () => Destroy();
        }

        public void Initialize(Data.Object.CharacterInformation information, Force force)
        {
            this.information = information;
            this.force = force;

            _sprite.sprite = Data.SpriteInformer.GetSprite(information.sprite);
            color = information.GetColor();

            movement.SetScale(information.scale);
            movement.moveSpeed = information.moveSpeed;

            ClearWeapon();

            foreach (var weaponKey in information.weapons)
                AddNewWeapon(Data.GameData.instance.GetWeaponData(weaponKey).Clone());

            health.Initialize(information.maxHp);
            _hitable.Initialize(force, Hitable.Type.Character);
        }

        public void Initialize(string key, Force force)
        {
            Initialize(Data.GameData.instance.GetCharacterData(key), force);
        }

        public new void Destroy()
        {
            onDestroy?.Invoke();
            base.Destroy();
        }

        public void ClearWeapon()
        {
            foreach (var weapon in _weapons)
                weapon.Destroy();
            _weapons.Clear();
        }

        public void AddNewWeapon(Data.Object.WeaponInformation information)
        {
            var weapon = ObjectPool<Weapon.Weapon>.instance.GetObject();
            weapon.Initialize(this, information);
            _weapons.Add(weapon);
        }

        public Weapon.Weapon GetWeapon(string key)
        {
            foreach (var weapon in _weapons)
                if (weapon.information.key.CompareTo(key) == 0)
                    return weapon;

            return null;
        }
    }
}