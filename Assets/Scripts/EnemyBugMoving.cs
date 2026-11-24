using UnityEngine;

public class EnemyBugMoving : MonoBehaviour
{
    [Header("날갯짓 이미지 4장")]
    public Sprite[] wingFrames = new Sprite[4];

    [Header("프레임 속도")]
    public float frameRate = 0.1f;

    private SpriteRenderer sr;
    private int currentFrame;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (wingFrames.Length > 0)
            sr.sprite = wingFrames[0];
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer = 0f;

            currentFrame++;

            if (currentFrame >= wingFrames.Length)
                currentFrame = 0;

            sr.sprite = wingFrames[currentFrame];
        }
    }
}
