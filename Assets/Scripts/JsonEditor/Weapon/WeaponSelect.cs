using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JsonEditor
{
    public class WeaponSelect : Item<WeaponSelect>
    {
        [SerializeField]
        Image _sprite;
        [SerializeField]
        TMPro.TextMeshProUGUI _key;

        WeaponList _list;

        public void Initialize(WeaponList list, string key)
        {
            _list = list;
            UpdateInterface(key);
        }

        public void UpdateInterface(string key)
        {
            var information = Data.GameData.instance.GetWeaponInformation(key);

            _key.text = key;
            _sprite.sprite = Data.SpriteInformer.GetSprite(information.projectile.sprite);
        }

        public void Remove() => _list.RemoveItem(this);

        public void ShowWeaponInformation() => _list.ShowWeaponInterface(index);
    }
}