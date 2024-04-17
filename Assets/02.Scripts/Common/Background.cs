using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject Player;
    public Material Material;
    public float ScrollSpeed = 0.05f;
    private float _fastScrollSpeed;

    private void Start()
    {
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
        if (Player.transform.position.x == Player.GetComponent<PlayerMove>().MaxX)
        {
            Material.mainTextureOffset += dir * _fastScrollSpeed * Time.deltaTime;
        }
        else
        {
            Material.mainTextureOffset += dir * ScrollSpeed * Time.deltaTime;
        }      
    }
}
