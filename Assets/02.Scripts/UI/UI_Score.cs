using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BestScoreText;
    private GameObject _player;
    private int _score = 0;
    public int BestScore = 0;

    private void Awake()
    {
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        BestScoreText.text = $"{BestScore}";
    }

    private void Start()
    {
        _player = GameObject.Find("Player");
        _score = _player.GetComponent<Player>().Score;      
    }
    private void Update()
    {
        if (GameManager.Instance.State == GameState.Over)
        {
            gameObject.SetActive(false);
        }
        _score = _player.GetComponent<Player>().Score;
        ScoreText.text = $"{_score}";
        BestScoreText.text = $"{BestScore}";
        if (_score > BestScore)
        {
            BestScore = _score;
            PlayerPrefs.SetInt("BestScore", BestScore);
        }
    }
}
