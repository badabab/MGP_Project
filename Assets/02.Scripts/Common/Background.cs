using UnityEngine;

public class Background : MonoBehaviour
{
    public Material Material;
    public float ScrollSpeed = 0.2f;
    public bool IsRight = true;
    public GameObject Player;

    private void Update()
    {
        IsRight = Player.GetComponent<PlayerMove>().LookingRight;
        Vector2 dir;
        if (IsRight)
        {
            dir = Vector2.right;
        }
        else
        {
            dir = Vector2.left;
        }
        Material.mainTextureOffset += dir * ScrollSpeed * Time.deltaTime;
    }
}
