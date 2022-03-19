using System;
using System.Collections;
using System.Collections.Generic;
using Data.Object;
using UnityEngine;
using UnityEngine.UI;

namespace JsonEditor
{
    public class WaveState : MonoBehaviour
    {
        [SerializeField]
        Image _sprite;
        [SerializeField]
        TMPro.TextMeshProUGUI _count;

        internal void Initialize(StageInformation.Group.Spawn spawn)
        {
            var information = Data.GameData.instance.GetCharacterInformation(spawn.key);
            _sprite.sprite = information.GetSprite();
            _sprite.color = information.GetColor();
            _count.text = $"x {spawn.count}";
        }
    }
}