using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Object
{
    static T _instance;
    public static T instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<T>(true);
            return _instance;
        }
    }
}
