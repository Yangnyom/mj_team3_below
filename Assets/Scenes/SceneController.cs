using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadGameScene()
    {
        // "GameScene"이라는 이름의 씬으로 이동합니다.
        SceneManager.LoadScene("GameScene");
    }
}

