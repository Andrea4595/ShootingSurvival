using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class NextLevel : MonoBehaviour
    {
        [SerializeField]
        string _nextLevel;

        public void Run()
        {
            SceneManager.LoadSceneAsync(_nextLevel);
        }
    }
}