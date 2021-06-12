using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    int score = 0;

    [SerializeField]
    TextMeshProUGUI gameOverText;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    Image displayLives;

    [SerializeField]
    GameObject gameOverWindow;

    [SerializeField]
    GameObject scoreTextWindow;

    [SerializeField]
    Sprite[] livesImages;

    [SerializeField]
    GameObject androidControlls;

    [SerializeField]
    GameObject postProcessing;

    GameManager gameManager;

    void Start()
    {
        gameOverWindow.SetActive(false);
        scoreTextWindow.SetActive(true);
        displayLives.gameObject.SetActive(true);
        gameManager = FindObjectOfType<GameManager>();
        scoreText.text = "Score: " + score.ToString();

#if UNITY_ANDROID
        if (androidControlls)
            androidControlls.SetActive(true);

        if (postProcessing)
            postProcessing.SetActive(false);
#else
        if (androidControlls)
            androidControlls.SetActive(false);

        if (postProcessing)
            postProcessing.SetActive(true);
#endif
    }

    public void AddScore()
    {
        score += 10;
        scoreText.text = "Score: " + score.ToString();

        if (score > 1000)
            scoreTextWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(225, scoreTextWindow.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void UpdateLives(int lives)
    {
        displayLives.sprite = livesImages[lives];

        if (lives <= 0)
            GameOverSetting();
    }

    public void GameOverSetting()
    {
        gameManager.EndGame();
        gameOverWindow.SetActive(true);
        scoreTextWindow.SetActive(false);
        displayLives.gameObject.SetActive(false);
        StartCoroutine(FlickerVFX());

        if (androidControlls)
            androidControlls.SetActive(false);
    }

    IEnumerator FlickerVFX()
    {
        while (true)
        {
            gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
