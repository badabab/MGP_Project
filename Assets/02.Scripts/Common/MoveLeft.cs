using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    void Update()
    {
        // �θ� ������Ʈ�� Transform�� �����ɴϴ�.
        Transform parentTransform = transform;

        // �θ� ������Ʈ�� ��� �ڽ� ������Ʈ�� ���� �ݺ��մϴ�.
        foreach (Transform childTransform in parentTransform)
        {
            // �ڽ� ������Ʈ�� ���� ��ġ�� �����ɴϴ�.
            Vector3 currentPosition = childTransform.position;

            // ���ο� ��ġ�� ����մϴ� (�������� �̵�).
            Vector3 newPosition = currentPosition + Vector3.left * 0.3f * Time.deltaTime;

            // ���ο� ��ġ�� �ڽ� ������Ʈ�� ��ġ�� �����մϴ�.
            childTransform.position = newPosition;
        }
    }
}
