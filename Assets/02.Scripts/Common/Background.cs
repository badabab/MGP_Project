using UnityEngine;

public class Background : MonoBehaviour
{
    private GameObject _player;
    public Material Material;
    public float ScrollSpeed = 0.05f;
    private float _fastScrollSpeed;

    private void Start()
    {
        _player = GameObject.Find("Player");
        _fastScrollSpeed = ScrollSpeed * 2;
    }
    private void Update()
    {
        if (GameManager.Instance.State != GameState.Go)
        {
            return;
        }
        Vector2 dir;
        dir = Vector2.right;
        if (_player.transform.position.x == _player.GetComponent<PlayerMove>().MaxX)
        {
            Material.mainTextureOffset += dir * _fastScrollSpeed * Time.deltaTime;
        }
        else
        {
            Material.mainTextureOffset += dir * ScrollSpeed * Time.deltaTime;
        }      
    }

    public void ChangeBackground()
    {

    }
}
