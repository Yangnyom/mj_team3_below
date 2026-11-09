using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_drone : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private bool isGrounded;

    public Sprite idleSprite;
    public Sprite moveSprite;

    private float moveInput;

    private float defaultGravity;
    private bool isFloating = false;
    private float floatTimer = 0f;
    private bool hasUsedFloatThisJump = false;
    private const float maxFloatDuration = 1.5f;

    public Sprite bulletSprite;
    public float bulletSpeed = 15f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultGravity = rb.gravityScale;
    }

    void Update()
    {
        moveInput = 0f;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                moveInput = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                moveInput = 1f;
        }

        if (isFloating)
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, 0f);
        }
        else
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }

        if (moveInput != 0)
        {
            spriteRenderer.sprite = moveSprite; 
            spriteRenderer.flipX = (moveInput < 0); 
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
        }

        if (Keyboard.current != null && (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            hasUsedFloatThisJump = false;
        }

        if (!isGrounded)
        {
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && !hasUsedFloatThisJump)
            {
                isFloating = true;
                floatTimer = maxFloatDuration;
                rb.gravityScale = 0f;
                hasUsedFloatThisJump = true;
            }

            if (isFloating)
            {
                if (Keyboard.current != null && Keyboard.current.spaceKey.wasReleasedThisFrame)
                {
                    StopFloating();
                }
                else
                {
                    floatTimer -= Time.deltaTime;
                    if (floatTimer <= 0f)
                    {
                        StopFloating();
                    }
                }
            }
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletSprite == null) return;

        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        Vector2 direction = (mouseWorldPos - transform.position).normalized;
        
        // 드론과 총알이 서로 부딪히지 않도록 생성 거리를 1.2f로 늘리고, Y축 오프셋을 제거하거나 최소화하여 위쪽 발사 시 충돌 방지
        Vector3 spawnPos = transform.position + (Vector3)(direction * 1.2f);

        GameObject bullet = new GameObject("Bullet");
        bullet.transform.position = spawnPos;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        SpriteRenderer sr = bullet.AddComponent<SpriteRenderer>();
        sr.sprite = bulletSprite;

        Rigidbody2D bulletRb = bullet.AddComponent<Rigidbody2D>();
        bulletRb.bodyType = RigidbodyType2D.Dynamic;
        bulletRb.gravityScale = 0f;
        bulletRb.linearVelocity = direction * bulletSpeed;

        BoxCollider2D col = bullet.AddComponent<BoxCollider2D>();
        
        bullet.AddComponent<BulletController>();
    }

    private void StopFloating()
    {
        if (isFloating)
        {
            isFloating = false;
            rb.gravityScale = defaultGravity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y < transform.position.y)
        {
            isGrounded = true;
            StopFloating();
            hasUsedFloatThisJump = false;
        }
    }
}

public class BulletController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 플레이어 자신과의 충돌은 무시하고, 타일맵이나 벽에 닿았을 때만 파괴
        if (collision.gameObject.CompareTag("Player")) return;
        
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}