using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JsonEditor
{
    public class SpriteSelector : MonoBehaviour
    {
        [SerializeField]
        Image _sprite;
        [SerializeField]
        TMPro.TMP_Dropdown _selector;

        public string key { get; private set; }
        public Sprite sprite
        {
            get => _sprite.sprite;
            private set => _sprite.sprite = value;
        }
        public Color color
        {
            get => _sprite.color;
            set => _sprite.color = value;
        }
        public UnityEvent<int> onValueChanged;

        private void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            _selector.ClearOptions();

            var options = new List<TMPro.TMP_Dropdown.OptionData>();

            foreach(var sprite in Data.SpriteInformer.instance.sprites)
                options.Add(new TMPro.TMP_Dropdown.OptionData(sprite.key, sprite.sprite));

            _selector.AddOptions(options);

            onValueChanged.AddListener(ValueChanged);
            _selector.onValueChanged.AddListener((int value) => onValueChanged?.Invoke(value));
        }

        void ValueChanged(int index)
        {
            var spriteInformation = Data.SpriteInformer.instance.sprites[index];
            sprite = spriteInformation.sprite;
            key = spriteInformation.key;
        }

        public void SetValueWithoutNotify(string key)
        {
            this.key = key;
            sprite = Data.SpriteInformer.GetSprite(key);
            _selector.SetValueWithoutNotify(Data.SpriteInformer.GetSpriteIndex(key));
        }
    }
}