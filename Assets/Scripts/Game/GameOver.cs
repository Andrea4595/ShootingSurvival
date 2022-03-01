using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField]
        NextLevel _goToLobby;
        [SerializeField]
        float _wait;
        [SerializeField]
        TMPro.TextMeshProUGUI _creditReward;

        public void Run(int creditReward)
        {
            _creditReward.text = $"{creditReward.ToString()} Credit";
            Data.GameData.instance.credit += creditReward;
            Data.GameData.instance.playerData.Save();

            Time.instance.Fade(0, 5);

            gameObject.SetActive(true);

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