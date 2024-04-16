using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameoverPopup : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public GameObject player;

    public void Open()
    {
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
        Debug.Log("�ٽ��ϱ�");
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void OnEndButtonClicked()
    {
        Debug.Log("��������");

        // ���� �� ������� ��� �����ϴ� ���
        Application.Quit();

#if UNITY_EDITOR
        // ����Ƽ �����Ϳ��� �������� ��� �����ϴ� ���
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
