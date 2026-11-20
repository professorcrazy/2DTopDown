using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    int score = 0;
    TMP_Text scoreText;

    private void Start()
    {
        scoreText = GetComponentInChildren<TMP_Text>();
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        
        instance = this;    
    }
    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null)
        {
            scoreText.text = score.ToString("0");
        }
    }
    public void ResetScore()
    {
        score = 0;
        if (scoreText != null)
        {
            scoreText.text = score.ToString("0");
        }
    }
}
