using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    int score = 0;

    int highScore = 0;

    string highScoreKey;

    const string SINGLE_PLAYER_HIGH_SCORE_KEY = "high score";

    const string CoOp_HIGH_SCORE_KEY = "CoOp High Score";

    [SerializeField]
    bool playModeSinglePlayer = false;

    [SerializeField]
    TextMeshProUGUI gameOverText;

    [SerializeField]
    TextMeshProUGUI pauseGameText;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI highScoreText;

    [SerializeField]
    Image displayLives;

    [SerializeField]
    GameObject gameOverWindow;

    [SerializeField]
    GameObject scoreTextWindow;

    [SerializeField]
    GameObject displayLivesWindow;

    [SerializeField]
    GameObject pauseMenuWindow;

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
        pauseMenuWindow.SetActive(false);
        scoreTextWindow.SetActive(true);
        displayLivesWindow.SetActive(true);

        if (playModeSinglePlayer)
            highScoreKey = SINGLE_PLAYER_HIGH_SCORE_KEY;
        else
            highScoreKey = CoOp_HIGH_SCORE_KEY;

        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        gameManager = FindObjectOfType<GameManager>();
        scoreText.text = "Score: " + score.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenuWindow.SetActive(true);
            StartCoroutine(FlickerVFX(pauseGameText, "Game Paused"));
            Time.timeScale = 0;
        }
    }

    public void AddScore()
    {
        score += 10;
        scoreText.text = "Score: " + score.ToString();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
            highScoreText.text = "High Score: " + highScore.ToString();
        }

        if (highScore > 10000)
            scoreTextWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(275, scoreTextWindow.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void UpdateLives(int lives, int player = 1)
    {
        if (player == 1)
            displayLivesWindow.GetComponent<Image>().color = new Color32(66, 192, 146, 225);
        else if (player == 2)
            displayLivesWindow.GetComponent<Image>().color = new Color32(192, 75, 66, 225);

        displayLives.sprite = livesImages[lives];

        if (lives <= 0)
            GameOverSetting();
    }

    public void GameOverSetting()
    {
        gameManager.EndGame();
        gameOverWindow.SetActive(true);
        scoreTextWindow.SetActive(false);
        displayLivesWindow.SetActive(false);
        StartCoroutine(FlickerVFX(gameOverText, "Game Over"));

        if (androidControlls)
            androidControlls.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenuWindow.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator FlickerVFX(TextMeshProUGUI setText, string textToSet)
    {
        while (true)
        {
            setText.text = textToSet;
            yield return new WaitForSecondsRealtime(0.5f);
            setText.text = "";
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
