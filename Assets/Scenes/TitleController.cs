using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    // PLAY 버튼을 누르면 로비 씬(LobbyScene)으로 이동
    public void GoToMainScene()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
