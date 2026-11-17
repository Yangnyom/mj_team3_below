using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    // Play 버튼을 누르면 GameScene으로 이동
    public void GoToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
