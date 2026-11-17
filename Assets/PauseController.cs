using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [Header("일시정지 메뉴 패널")]
    public GameObject pausePanel;

    private bool isPaused = false;

    void Start()
    {
        // 게임 시작 시에는 시간이 정상 흐르고 일시정지 패널은 꺼져있음
        Time.timeScale = 1f;
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        // ESC 키를 누르면 일시정지 상태를 토글(켜고 끄기)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // 1. 게임 일시정지 실행
    public void PauseGame()
    {
        isPaused = true;
        if (pausePanel != null)
            pausePanel.SetActive(true);
        Time.timeScale = 0f; // 게임 내 모든 움직임과 시간 정지
    }

    // 2. 게임 계속하기 (취소/재개)
    public void ResumeGame()
    {
        isPaused = false;
        if (pausePanel != null)
            pausePanel.SetActive(false);
        Time.timeScale = 1f; // 게임 시간 정상화
    }

    // 3. 타이틀 화면으로 나가기
    public void GoToTitle()
    {
        Time.timeScale = 1f; // 씬 전환 전 시간을 반드시 원복해야 함
        SceneManager.LoadScene("TitleScene");
    }

    // 4. 설정 버튼 (추후 설정 창 연결)
    public void OpenSettings()
    {
        Debug.Log("설정 창 열기 기능 구현 예정");
        // 나중에 설정 팝업창을 활성화하는 코드를 여기에 넣으면 됩니다.
    }
}