using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene management

public class GameManager : MonoBehaviour
{
  public ScoreManager scoreManager;
  public TileManager tileManager;
  public static GameManager Instance { get; private set; } // Singleton pattern
  [SerializeField] private GameObject gameOverUI;
  [SerializeField] private GameObject pauseGameUI;
  public int scoreForSpeedIncrease = 500; // For every 500 points, increase the speed
  private int lastSpeedIncreaseScore = 0;

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

  private void Update()
  {
    CheckForSpeedIncrease();
    if (lastSpeedIncreaseScore > 499)
      Debug.Log($"lastSpeedIncreaseScore {lastSpeedIncreaseScore}");
  }
  void CheckForSpeedIncrease()
  {
    if (scoreManager.Score - lastSpeedIncreaseScore >= scoreForSpeedIncrease)
    {
      tileManager.IncreaseTileSpeed();
      lastSpeedIncreaseScore = (int)scoreManager.Score;
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
