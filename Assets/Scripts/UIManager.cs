using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    Image displayLives;

    [SerializeField]
    Sprite[] livesImages;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        score += 10;
        text.text = "Score: " + score.ToString();
    }

    public void UpdateLives(int lives)
    {
        displayLives.sprite = livesImages[lives];
    }
}
