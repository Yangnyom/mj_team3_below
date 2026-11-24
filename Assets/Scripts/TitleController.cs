using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    // Play 버튼을 누르면 이동할 로비 씬의 이름 (유니티에 저장된 씬 이름과 정확히 일치해야 합니다)
    public string lobbySceneName = "Lobby";

    // Play 버튼에 연결할 함수
    public void GoToLobby()
    {
        SceneManager.LoadScene(lobbySceneName);
    }
}