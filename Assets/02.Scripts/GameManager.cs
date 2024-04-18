using System.Collections;
using TMPro;
using UnityEngine;

public enum GameState
{
    Ready, Go, Pause, Over
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    public GameState State { get; private set; } = GameState.Ready;
    public TextMeshProUGUI StateText;
    public UI_GameoverPopup GameoverUI;
    public UI_OptionPopup OptionUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        OptionUI.Close();
        Time.timeScale = 1f;
        StartCoroutine(Start_Coroutine());
    }
    private IEnumerator Start_Coroutine()
    {
        State = GameState.Ready;
        StateText.gameObject.SetActive(true);
        Refresh();

        yield return new WaitForSeconds(1.6f);
        State = GameState.Go;
        Refresh();

        yield return new WaitForSeconds(0.4f);
        StateText.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.2f);
        StageManager stageManager = GameObject.FindAnyObjectByType<StageManager>();
        stageManager.NextStage();
    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        State = GameState.Over;
        StateText.gameObject.SetActive(true);
        Refresh();
        GameoverUI.Open();
    }

    public void Refresh()
    {
        switch (State)
        {
            case GameState.Ready:
            {
                StateText.color = new Color(0.75f, 0.85f, 0.76f, 1);
                StateText.text = "Ready...";
                break;
            }
            case GameState.Go:
            {
                StateText.color = new Color32(231, 206, 247, 255);
                StateText.text = "Start!";
                             
                break;
            }
            case GameState.Over:
            {
                StateText.color = new Color32(230, 180, 175, 255);
                StateText.text = "Game Over";
                break;
            }
        }
    }
    public void Pause()
    {
        State = GameState.Pause;
        Time.timeScale = 0f;
    }
    public void Continue()
    {
        State = GameState.Go;
        Time.timeScale = 1f;
    }

    public void OnOptionButtonClicked()
    {
        //Debug.Log("옵션 버튼 클릭");
        OptionUI.Open();
        Pause();
    }
}
