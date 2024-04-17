using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.SocialPlatforms.Impl;

public class UI_GameoverPopup : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public GameObject player;

    public void Open()
    {
        ScoreText.text = $"Score : {player.GetComponent<Player>().Score}";
        gameObject.SetActive(true);
    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void OnRestartButtonClicked()
    {
        //Debug.Log("다시하기");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void OnEndButtonClicked()
    {
        //Debug.Log("게임종료");

        // 빌드 후 실행됐을 경우 종료하는 방법
        Application.Quit();

#if UNITY_EDITOR
        // 유니티 에디터에서 실행했을 경우 종료하는 방법
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
