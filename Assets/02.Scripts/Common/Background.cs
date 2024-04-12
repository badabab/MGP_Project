using UnityEngine;

public class Background : MonoBehaviour
{
    public Material Material;
    public float ScrollSpeed = 0.2f;

    private void Update()
    {
        Vector2 dir = Vector2.right;
        Material.mainTextureOffset += dir * ScrollSpeed * Time.deltaTime;
    }
}
