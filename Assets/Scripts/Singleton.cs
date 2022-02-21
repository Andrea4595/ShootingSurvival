using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    public static T instance { get; private set; }

    protected void Initialize(T self) => instance = self;
}
