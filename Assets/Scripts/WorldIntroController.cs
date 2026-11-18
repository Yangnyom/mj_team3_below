using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldIntroController : MonoBehaviour
{
    public Image backgroundImage;
    public TMP_Text subtitleText;
    public TMP_Text spacePromptText;

    public Sprite[] backgroundImages;
    public string[] subtitles;

    private int currentIndex = 0;
    private bool canAdvance = false;

    void Start()
    {
        ShowScene();
    }

    void Update()
    {
        if (canAdvance &&
            (Input.GetKeyDown(KeyCode.Space) ||
             Input.GetMouseButtonDown(0)))
        {
            NextScene();
        }
    }

    void ShowScene()
    {
        canAdvance = false;

        backgroundImage.sprite = backgroundImages[currentIndex];
        subtitleText.text = subtitles[currentIndex];

        spacePromptText.gameObject.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(ShowSpacePrompt());
    }

    IEnumerator ShowSpacePrompt()
    {
        yield return new WaitForSeconds(5f);

        spacePromptText.gameObject.SetActive(true);
        canAdvance = true;
    }

    void NextScene()
{
    if (currentIndex < backgroundImages.Length - 1)
    {
        currentIndex++;
        ShowScene();
    }
    else
    {
        SceneManager.LoadScene("TitleScene");
    }
}
}
