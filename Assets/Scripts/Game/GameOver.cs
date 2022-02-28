using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField]
        Animation _animation;
        [SerializeField]
        NextLevel _goToLobby;
        [SerializeField]
        float _wait;

        public void Run()
        {
            Time.instance.Fade(0, 5);

            gameObject.SetActive(true);
            _animation.Play();

            StartCoroutine(CInputWait(_wait));
        }

        IEnumerator CInputWait(float wait)
        {
            yield return new WaitForSeconds(wait);

            while (Input.anyKeyDown == false)
                yield return null;

            _goToLobby.Run();
        }
    }
}