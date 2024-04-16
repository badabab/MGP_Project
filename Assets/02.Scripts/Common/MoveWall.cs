using UnityEngine;

public class MoveWall : MonoBehaviour
{
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
        transform.Translate(Vector3.left * 1f * Time.deltaTime);
    }
}
