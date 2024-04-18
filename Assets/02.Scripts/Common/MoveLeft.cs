using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    void Update()
    {
        // 부모 오브젝트의 Transform을 가져옵니다.
        Transform parentTransform = transform;

        // 부모 오브젝트의 모든 자식 오브젝트에 대해 반복합니다.
        foreach (Transform childTransform in parentTransform)
        {
            // 자식 오브젝트의 현재 위치를 가져옵니다.
            Vector3 currentPosition = childTransform.position;

            // 새로운 위치를 계산합니다 (왼쪽으로 이동).
            Vector3 newPosition = currentPosition + Vector3.left * 0.3f * Time.deltaTime;

            // 새로운 위치로 자식 오브젝트의 위치를 설정합니다.
            childTransform.position = newPosition;
        }
    }
}
