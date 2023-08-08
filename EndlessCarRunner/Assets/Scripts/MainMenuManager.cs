using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string mainSceneName = "Main";
    [SerializeField] private string scoreboardSceneName = "ScoreboardScene";
    public void LoadMainScene()
    {
        SceneManager.LoadScene(mainSceneName);
    }

    public void LoadScoreBoardScene()
    {
        SceneManager.LoadScene(scoreboardSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}