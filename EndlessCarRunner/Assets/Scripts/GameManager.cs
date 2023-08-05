using UnityEngine;
public class GameManager : MonoBehaviour
{
  public static GameManager Instance { get; private set; }

  public enum GameState
  {
    Playing,
    Paused,
    GameOver
  }

  // Current game state
  private GameState currentGameState = GameState.Playing;

  // Method to handle game over
  public void GameOver()
  {
    // Handle game over logic, UI, and transitions
    currentGameState = GameState.GameOver;
  }

  public void PauseGame()
  {
    currentGameState = GameState.Paused;
    Time.timeScale = 0f;
  }
  public void ResumeGame()
  {
    currentGameState = GameState.Playing;
    Time.timeScale = 1f;
  }
}