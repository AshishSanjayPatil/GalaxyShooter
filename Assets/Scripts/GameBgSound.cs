using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBgSound : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType<GameBgSound>().Length > 1)
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(this.gameObject);
    }
}
