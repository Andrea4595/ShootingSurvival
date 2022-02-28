using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField]
        RectTransform _bar;
        [SerializeField]
        TMPro.TextMeshProUGUI _label;

        private void OnEnable()
        {
            var player = PlayerSetter.instance.player;
            player.health.onUpdate += UpdateState;
        }

        void UpdateState()
        {
            var playerHealth = PlayerSetter.instance.player.health;

            _bar.localScale = new Vector3(playerHealth.hpPercent, 1, 1);
            _label.text = $"{Mathf.RoundToInt(Mathf.Max(0, playerHealth.hp))} / {Mathf.RoundToInt(playerHealth.maxHp)}";
        }
    }
}