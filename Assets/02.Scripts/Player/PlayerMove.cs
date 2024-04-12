using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool LookingRight = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerTurn();
        }
    }

    private void PlayerTurn()
    {
        LookingRight = !LookingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
