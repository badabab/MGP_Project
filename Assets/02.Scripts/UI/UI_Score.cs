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
        Score.text = $"Score : {_player.GetComponent<Player>().Score}";
    }
}
