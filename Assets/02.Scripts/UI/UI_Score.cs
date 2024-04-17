using TMPro;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    public TextMeshProUGUI Score;
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (GameManager.Instance.State == GameState.Over)
        {
            gameObject.SetActive(false);
        }
        Score.text = $"Score : {_player.GetComponent<Player>().Score}";
    }
}
