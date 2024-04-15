using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float ArrowSpeed = 3f; // 화살 속도

    void Update()
    {
        MoveArrows();
    }

    void MoveArrows()
    {
        transform.Translate(Vector3.right * ArrowSpeed * Time.deltaTime);
        StartCoroutine(DisableArrowAfterDelay(gameObject, 2f));
    }

    System.Collections.IEnumerator DisableArrowAfterDelay(GameObject arrow, float delay)
    {
        yield return new WaitForSeconds(delay);
        arrow.SetActive(false);
    }
}
