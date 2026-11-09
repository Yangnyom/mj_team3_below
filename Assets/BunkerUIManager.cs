using UnityEngine;
using TMPro;

public class BunkerUIManager : MonoBehaviour
{
    [Header("UI Text References")]
    public TextMeshProUGUI resourceText;
    public TextMeshProUGUI dialogueText;

    // 자원 수치 데이터
    private int food = 100;
    private int water = 100;
    private int energy = 100;

    void Start()
    {
        // 게임 시작 시 기본 자원 표시 및 요청하신 초기 자막 설정
        UpdateResourceUI();
        SetDialogue("벙커 시스템이 정상적으로 가동되었습니다.");
    }

    // 자원 텍스트 업데이트 함수
    public void UpdateResourceUI()
    {
        if (resourceText != null)
        {
            resourceText.text = $"식량: {food} | 물: {water} | 전력: {energy}";
        }
    }

    // 자막(대화창) 변경 함수
    public void SetDialogue(string message)
    {
        if (dialogueText != null)
        {
            dialogueText.text = message;
        }
    }

    // 드론이 자원을 가져왔을 때 호출할 함수 (예시로 식량 +10, 물 +10, 전력 +5 증가)
    public void OnDroneResourceCollected()
    {
        food += 10;
        water += 10;
        energy += 5;

        // UI 갱신
        UpdateResourceUI();

        // 하단 자막에 벙커 시스템 문구 출력
        SetDialogue("벙커 시스템: 드론이 외부 탐색을 마치고 자원을 성공적으로 회수했습니다.");
    }
}