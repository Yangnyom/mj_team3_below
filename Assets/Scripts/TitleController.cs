using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    // Hierarchy에 있는 Lobby 씬 이름과 정확히 같아야 합니다.
    public string Lobby = "Lobby";

    // Hierarchy에 있는 Control 씬 이름과 정확히 같아야 합니다.
    public string controlSceneName = "ControlScene";

    public void GoToLobby()
    {
        SceneManager.LoadScene(Lobby);
    }

    public void GoToControl()
    {
        SceneManager.LoadScene(controlSceneName);
    }
}