using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_OptionPopup : MonoBehaviour
{
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
        //gameObject.SetActive(false);
    }
    public void OnContinueButtonClicked()
    {
        //Debug.Log("����ϱ�");
        GameManager.Instance.Continue();
        Close();
    }
    public void OnRestartButtonClicked()
    {
        //Debug.Log("�ٽ��ϱ�");
        SoundManager.instance.StopBgm();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void OnEndButtonClicked()
    {
        //Debug.Log("��������");

        // ���� �� ������� ��� �����ϴ� ���
        Application.Quit();

#if UNITY_EDITOR
        // ����Ƽ �����Ϳ��� �������� ��� �����ϴ� ���
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
