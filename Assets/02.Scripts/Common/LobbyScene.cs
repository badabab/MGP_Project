using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene(1);
    }
}
