using UnityEngine;
using TMPro; // TextMeshPro를 사용하기 위함
using System.Collections;

public class DroneHUDManager : MonoBehaviour
{
    [Header("UI 연결")]
    public TextMeshProUGUI metalCountText;    // 금속 재료 수량 표시 텍스트
    public GameObject notificationPanel;     // 획득 알림 팝업 패널
    public TextMeshProUGUI notificationText; // "금속 재료 +1" 텍스트

    [Header("데이터")]
    private int metalCount = 0; // 현재 보유한 금속 재료 수

    void Start()
    {
        UpdateMetalUI();
        if (notificationPanel != null)
            notificationPanel.SetActive(false); // 시작할 때는 알림 창 끄기
    }

    // 금속 재료를 획득했을 때 호출하는 함수 (예: 아이템과 충돌했을 때)
    public void AddMetal(int amount)
    {
        metalCount += amount;
        UpdateMetalUI();

        // 획득 알림 팝업 띄우기
        StopAllCoroutines();
        StartCoroutine(ShowNotificationRoutine($"금속 재료 +{amount}"));
    }

    // 재료를 전부 잃었을 때 (드론 파괴 시 호출)
    public void LoseAllMetal()
    {
        metalCount = 0;
        UpdateMetalUI();
        StartCoroutine(ShowNotificationRoutine("드론 파괴! 금속 재료를 모두 잃었습니다."));
    }

    void UpdateMetalUI()
    {
        if (metalCountText != null)
        {
            metalCountText.text = $"금속 재료: {metalCount}";
        }
    }

    // 토스트 메시지 형식으로 알림을 잠깐 띄웠다 사라지게 하는 코루틴
    IEnumerator ShowNotificationRoutine(string message)
    {
        if (notificationPanel != null && notificationText != null)
        {
            notificationText.text = message;
            notificationPanel.SetActive(true);

            yield return new WaitForSeconds(2.0f); // 2초 동안 유지

            notificationPanel.SetActive(false);
        }

        void Update()
        {
            // 테스트용: T키를 누르면 금속 재료 획득
            if (Input.GetKeyDown(KeyCode.T))
            {
                AddMetal(1);
            }
            // 테스트용: L키를 누르면 재료 손실 (드론 파괴 상황)
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoseAllMetal();
            }
        }
    }
}
