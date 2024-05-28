using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public AudioSource coinAudio;
    public static ScoreManager Instance { get; set; }
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;

    public void Start()
    {
        Instance = this;
    }
    public void SetScore(int score)
    {
        this.score += score;
        scoreText.text = "Ñ÷¸ò: " + this.score.ToString();
        coinAudio.Play();
    }
}
