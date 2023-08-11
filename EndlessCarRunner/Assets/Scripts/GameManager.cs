using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene management

public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; } // Singleton pattern

  [SerializeField] private GameObject gameOverUI;
  [SerializeField] private GameObject pauseGameUI;

  private void Awake()
  {
    // Singleton pattern
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void GameOver()
  {
    gameOverUI.SetActive(true);
    DOTween.KillAll();
    Time.timeScale = 0f;
  }

  public void Pause()
  {
    Time.timeScale = 0f;
    DOTween.PauseAll();
    pauseGameUI.SetActive(true);
  }

  public void Retry()
  {
    Time.timeScale = 1f;
    DOTween.RestartAll();
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void MainMenu()
  {
    Time.timeScale = 1f;
    DOTween.KillAll();
    SceneManager.LoadScene("MainMenu");
  }
}
