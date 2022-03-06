using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JsonEditor
{
    public class CharacterSelect : Item<CharacterSelect>
    {
        [SerializeField]
        Image _sprite;
        [SerializeField]
        TMPro.TextMeshProUGUI _key;

        CharacterList _list;

        public void Initialize(CharacterList list, string key)
        {
            _list = list;
            UpdateInterface(key);
        }

        public void UpdateInterface(string key)
        {
            var information = Data.GameData.instance.GetCharacterData(key);

            _key.text = key;
            _sprite.sprite = Data.SpriteInformer.GetSprite(information.sprite);
            _sprite.color = information.GetColor();
        }

        public void Remove() => _list.RemoveItem(this);

        public void ShowCharacterInformation() => _list.ShowCharacterInterface(index);
    }
}