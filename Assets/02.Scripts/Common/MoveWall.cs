using UnityEngine;

public class MoveWall : MonoBehaviour
{
    public float WallSpeed = 0.7f;
    void Update()
    {
        if (GameManager.Instance.State != GameState.Go)
        {
            return;
        }

        if (transform.position.x <= 5)
        {
            return;
        }
        transform.Translate(Vector3.left * WallSpeed * Time.deltaTime);
    }
}
