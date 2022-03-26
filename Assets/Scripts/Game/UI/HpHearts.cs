using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Game.UI
{
    public class HpHearts : MonoBehaviour
    {
        [SerializeField]
        Image[] _hearts;

        [SerializeField]
        SpriteAtlas _heartAtlas;

        [SerializeField]
        AnimationCurve _shakeCurve;
        [SerializeField]
        float _shakeRange;
        [SerializeField]
        float _shakeDuration;

        Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = transform.position;
        }

        private void OnEnable()
        {
            UpdateHearts();
            PlayerSetter.instance.player.health.onUpdate += UpdateHearts;
            PlayerSetter.instance.player.health.onHit += Shake;
        }

        void Shake()
        {
            StartCoroutine(CShake());

            IEnumerator CShake()
            {
                var time = 0f;

                while(time < _shakeDuration)
                {
                    var range = (1 - _shakeCurve.Evaluate(time / _shakeDuration)) * _shakeRange;
                    var randomOffset = new Vector3(Random.Range(-range, range), Random.Range(-range, range));
                    var position = _startPosition + randomOffset;

                    transform.position = position;
                    time += Time.smoothDeltaTime;

                    yield return null;
                }

                transform.position = _startPosition;
            }
        }

        void UpdateHearts()
        {
            void SetHeartSprite(int index, Image heart, Health health)
            {
                if (index >= health.maxHp)
                {
                    heart.gameObject.SetActive(false);
                    return;
                }

                heart.gameObject.SetActive(true);

                if (index < health.hp)
                    heart.sprite = _heartAtlas.GetSprite("Heart");
                else
                    heart.sprite = _heartAtlas.GetSprite("Heart damaged");
            }

            var player = PlayerSetter.instance.player;
            var health = player.health;

            for (int i = 0; i < _hearts.Length; i++)
                SetHeartSprite(i, _hearts[i], health);
        }
    }
}