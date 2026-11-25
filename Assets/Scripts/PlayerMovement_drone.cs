using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_drone : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("이동")]
    public float moveSpeed = 5f;
    public float flySpeed = 5f;

    [Header("맵 제한")]
    public float minX = -13f;
    public float maxY = 12f;

    [Header("스프라이트")]
    public Sprite idleSprite;
    public Sprite moveSprite;

    [Header("총알")]
    public Sprite bulletSprite;
    public float bulletSpeed = 5f;

    [Header("경고 텍스트")]
    public GameObject warningText;

    private bool isShowingMessage = false;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (warningText != null)
            warningText.SetActive(false);
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

        float verticalVelocity = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed ||
                Keyboard.current.upArrowKey.isPressed)
            {
                verticalVelocity = flySpeed;
            }
            else if (Keyboard.current.sKey.isPressed ||
                     Keyboard.current.downArrowKey.isPressed)
            {
                verticalVelocity = -flySpeed;
            }
        }

        rb.linearVelocity = new Vector2(
            moveInput * moveSpeed,
            verticalVelocity
        );

        if (moveInput != 0)
        {
            spriteRenderer.sprite = moveSprite;
            spriteRenderer.flipX = (moveInput < 0);
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
        }

        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }

        Vector3 pos = transform.position;

        if (pos.x < minX)
            pos.x = minX;

        if (pos.y > maxY)
            pos.y = maxY;

        transform.position = pos;
    }

    private IEnumerator ShowWarning()
    {
        if (warningText == null)
            yield break;

        isShowingMessage = true;

        warningText.SetActive(true);

        yield return new WaitForSeconds(2f);

        warningText.SetActive(false);

        isShowingMessage = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BlockedArea"))
        {
            if (!isShowingMessage)
            {
                StartCoroutine(ShowWarning());
            }
        }
    }

    private void Shoot()
    {
        if (bulletSprite == null) return;

        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        Vector2 direction =
            (mouseWorldPos - transform.position).normalized;

        Vector3 spawnPos =
            transform.position + (Vector3)(direction * 1.2f);

        GameObject bullet = new GameObject("Bullet");
        bullet.transform.position = spawnPos;

        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bullet.transform.rotation =
            Quaternion.Euler(0, 0, angle);

        SpriteRenderer sr =
            bullet.AddComponent<SpriteRenderer>();

        sr.sprite = bulletSprite;

        Rigidbody2D bulletRb =
            bullet.AddComponent<Rigidbody2D>();

        bulletRb.gravityScale = 0f;
        bulletRb.linearVelocity = direction * bulletSpeed;

        bullet.AddComponent<BoxCollider2D>();
        bullet.AddComponent<BulletController>();
    }
}

public class BulletController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            return;

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}