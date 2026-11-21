using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float minX = -5f;

    public Vector3 offset = new Vector3(3f, 0f, -10f);

    void LateUpdate()
    {
        if (player == null) return;

        float targetX = player.position.x + offset.x;

        // 왼쪽 제한
        targetX = Mathf.Max(targetX, minX);

        transform.position = new Vector3(
            targetX,
            player.position.y + offset.y,
            offset.z
        );
    }
}