using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("이동 속도")]
    public float moveSpeed = 2f;

    [Header("왕복 거리")]
    public float patrolDistance = 3f;

    private Vector3 startPos;
    private bool movingRight = true;

    private SpriteRenderer sr;

    void Start()
    {
        startPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 이동
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        // 방향 전환
        if (transform.position.x >= startPos.x + patrolDistance)
        {
            movingRight = false;
        }

        if (transform.position.x <= startPos.x - patrolDistance)
        {
            movingRight = true;
        }

        // 이동 방향 바라보기
        sr.flipX = movingRight;
    }
}