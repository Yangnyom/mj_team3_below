using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    // 사격장 버튼을 누르면 ShootingScene으로 이동
    public void GoToShootingScene()
    {
        SceneManager.LoadScene("ShootingScene");
    }
}
