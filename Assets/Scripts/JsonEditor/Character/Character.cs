using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JsonEditor
{
    public class Character : MonoBehaviour
    {
        Data.Object.CharacterInformation _information;

        [SerializeField]
        CharacterList _characterList;

        [SerializeField]
        TMPro.TMP_InputField _key;
        [SerializeField]
        SpriteSelector _sprite;
        [SerializeField]
        Slider[] _color;
        [SerializeField]
        SliderWithInput _scale;
        [SerializeField]
        TMPro.TMP_InputField _maxHp;
        [SerializeField]
        TMPro.TMP_InputField _moveSpeed;
        [SerializeField]
        CharacterWeaponList _weaponList;

        private void Awake() => Initialize();

        void Initialize()
        {
            _key.onEndEdit.AddListener(UpdateKey);
            _sprite.onValueChanged.AddListener(UpdateSprite);
            _color[0].onValueChanged.AddListener(UpdateColorR);
            _color[1].onValueChanged.AddListener(UpdateColorG);
            _color[2].onValueChanged.AddListener(UpdateColorB);
            _scale.onValueChanged.AddListener(UpdateScale);
            _maxHp.onEndEdit.AddListener(UpdateHp);
            _moveSpeed.onEndEdit.AddListener(UpdateMoveSpeed);
            _weaponList.Initialize(this);
        }

        public void UpdateInterface(Data.Object.CharacterInformation information)
        {
            gameObject.SetActive(true);

            _information = information;

            _key.SetTextWithoutNotify(information.key);
            _sprite.SetValueWithoutNotify(information.sprite);
            _sprite.color = information.GetColor();
            for (var i = 0; i < 3; i++)
                _color[i].SetValueWithoutNotify(information.color[i]);
            _scale.SetValueWithoutNotify(information.scale);
            _maxHp.SetTextWithoutNotify(information.maxHp.ToString());
            _moveSpeed.SetTextWithoutNotify(information.moveSpeed.ToString());
            _weaponList.UpdateInterface(_information.weapons);
        }

        public void HideInterface() => gameObject.SetActive(false);

        public bool CheckKey(string key) => _information.key.CompareTo(key) == 0;

        public void UpdateKey(string text)
        {
            var from = _information.key;
            _information.key = text;

            UpdateInformation();
            SaveJsonData.instance.UpdateReferencedCharacterKey(from, _information.key);
            _characterList.UpdateInterface();
        }

        public void UpdateSprite(int index)
        {
            _information.sprite = _sprite.key;

            UpdateInformation();
            _characterList.UpdateInterface();
        }

        public void UpdateScale(float scale)
        {
            _information.scale = scale;

            UpdateInformation();
        }

        public void UpdateColorR(float value)
        {
            _information.color[0] = value;

            UpdateColor();
        }

        public void UpdateColorG(float value)
        {
            _information.color[1] = value;

            UpdateColor();
        }

        public void UpdateColorB(float value)
        {
            _information.color[2] = value;

            UpdateColor();
        }

        void UpdateColor()
        {
            Color color = _information.GetColor();
            _sprite.color = color;

            UpdateInformation();
            _characterList.UpdateInterface();
        }

        public void UpdateHp(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);
            _information.maxHp = value;

            UpdateInformation();
        }

        public void UpdateMoveSpeed(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);
            _information.moveSpeed = value;

            UpdateInformation();
        }

        public void UpdateWeapon(string[] weapons)
        {
            _information.weapons = weapons;

            UpdateInformation();
        }

        void UpdateInformation()
        {
            Data.GameData.instance.SetCharacterData(_information);
            TestPlayer.instance.UpdateCharacterInformation(_information.Clone());
            SaveJsonData.instance.SaveCharacterIfAuto();
        }
    }
}