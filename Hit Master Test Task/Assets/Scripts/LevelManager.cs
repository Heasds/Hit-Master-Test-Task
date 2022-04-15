using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool isGameStarted;

    public void StartGame()
    {
        isGameStarted = true;
    }
    public void FinishGame()
    {
        SceneManager.LoadScene(0);
    }
}
