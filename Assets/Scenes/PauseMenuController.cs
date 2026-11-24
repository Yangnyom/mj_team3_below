using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("일시정지 패널 (ESC 누르면 등장)")]
    public GameObject pausePanel;

    [Header("이동할 타이틀 씬 이름")]
    public string titleSceneName = "TitleScene";

    private bool isPaused = false;

    void Start()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                OpenPauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        isPaused = true;
        if (pausePanel != null)
            pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        if (pausePanel != null)
            pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void GoToTitle()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(titleSceneName);
    }
}