using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Sprite[] frames = new Sprite[4];
    public float frameRate = 0.15f;

    private SpriteRenderer sr;
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (frames.Length > 0)
            sr.sprite = frames[0];
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer = 0f;

            currentFrame++;

            if (currentFrame >= frames.Length)
                currentFrame = 0;

            sr.sprite = frames[currentFrame];
        }
    }
}
