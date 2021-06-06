using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    int currentIndex;

    void Start()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Application.Quit();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentIndex + 1);
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(currentIndex);
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
