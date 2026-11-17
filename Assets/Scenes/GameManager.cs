using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("화면 암전용 페이드 패널")]
    public GameObject fadePanel;
    public float fadeDuration = 1.0f;

    void Start()
    {
        // 게임 씬이 처음 시작될 때 검은 화면 패널 끄기
        if (fadePanel != null)
            fadePanel.SetActive(false);
    }

    // 드론 탐험 화면을 시작할 때 호출하는 함수
    public void StartDroneExploration()
    {
        StartCoroutine(FadeAndStartRoutine());
    }

    IEnumerator FadeAndStartRoutine()
    {
        if (fadePanel != null)
        {
            fadePanel.SetActive(true);
            Image panelImage = fadePanel.GetComponent<Image>();
            Color color = panelImage.color;
            float elapsedTime = 0f;

            // 화면이 서서히 검은색으로 변함
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
                panelImage.color = color;
                yield return null;
            }
        }

        // 여기에 드론 탐험 HUD나 드론 조작 시스템을 활성화하는 코드를 넣으면 됩니다.
        Debug.Log("드론 탐험 화면 전환 완료!");
    }
}
