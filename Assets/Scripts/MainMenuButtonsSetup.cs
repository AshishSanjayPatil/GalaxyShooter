using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonsSetup : MonoBehaviour
{
    [SerializeField]
    GameObject coOpButton;

    void Start()
    {
#if UNITY_ANDROID
        coOpButton.GetComponent<Button>().interactable = false;
#endif
    }
}
