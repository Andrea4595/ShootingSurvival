using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Character : ObjectPoolInstance<Character>
    {
        public Movement movement;
        public Health health;

        [SerializeField]
        SpriteRenderer _sprite;

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

        List<Weapon> _weapons = new List<Weapon>();

        public enum Force { Player, Enemy }

        public Force force { get; private set; }
        public Data.Object.Character information { get; private set; }
        public event Action onDestroy;

        public void Initialize(Data.Object.Character information, Force force)
        {
            this.information = information;
            this.force = force;

            _sprite.sprite = SpriteInformer.GetSprite(information.sprite);
            color = new Color(information.color[0], information.color[1], information.color[2]);

            movement.SetScale(information.scale);
            _weapons.Clear();

            foreach (var weaponInformation in information.weapons)
            {
                var weapon = ObjectPool<Weapon>.GetObject();
                weapon.Initialize(this, weaponInformation);
                _weapons.Add(weapon);
            }

            movement.moveSpeed = information.moveSpeed;
            health.Initialize(this);
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
    }
}