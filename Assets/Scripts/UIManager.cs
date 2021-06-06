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
    GameObject restartButton;

    [SerializeField]
    GameObject quitButton;

    [SerializeField]
    Sprite[] livesImages;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        restartButton.SetActive(false);
        quitButton.SetActive(false);
        scoreText.gameObject.SetActive(true);
        displayLives.gameObject.SetActive(true);
        gameManager = FindObjectOfType<GameManager>();
        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        score += 10;
        scoreText.text = "Score: " + score.ToString();
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
        gameOverText.gameObject.SetActive(true);
        restartButton.SetActive(true);
        quitButton.SetActive(true);
        scoreText.gameObject.SetActive(false);
        displayLives.gameObject.SetActive(false);
        StartCoroutine(FlickerVFX());
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
