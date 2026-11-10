using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("일시정지 패널 (정지 버튼 누르거나 ESC 누르면 등장)")]
    public GameObject pausePanel;

    [Header("타이틀 이동 확인 팝업 (예/아니요 창)")]
    public GameObject confirmPopup;

    private bool isPaused = false;

    void Update()
    {
        // ESC 키를 누르면 일시정지 토글
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }

    // 1. 일시정지 메뉴 열기 (시간 멈춤)
    public void OpenPauseMenu()
    {
        isPaused = true;
        if (confirmPopup != null) confirmPopup.SetActive(false); // 확인창은 일단 끔
        if (pausePanel != null) pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // 2. 게임 재개 (계속하기)
    public void ResumeGame()
    {
        isPaused = false;
        if (pausePanel != null) pausePanel.SetActive(false);
        if (confirmPopup != null) confirmPopup.SetActive(false);
        Time.timeScale = 1f;
    }

    // 3. '타이틀로 가기' 버튼을 눌렀을 때 확인 팝업 띄우기
    public void OnClickGoToTitleButton()
    {
        if (confirmPopup != null) confirmPopup.SetActive(true);
    }

    // 4. 확인 팝업에서 [아니요]를 눌렀을 때
    public void OnClickNo()
    {
        if (confirmPopup != null) confirmPopup.SetActive(false);
    }

    // 5. 확인 팝업에서 [예]를 눌렀을 때 (타이틀로 이동)
    public void OnClickYesToTitle()
    {
        Time.timeScale = 1f; // 씬 넘어가기 전 시간 정상화
        SceneManager.LoadScene("TitleScene");
    }
}