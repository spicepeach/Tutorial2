using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAudio : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
