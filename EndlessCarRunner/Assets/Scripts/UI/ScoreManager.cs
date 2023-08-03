using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
  public Text scoreText;
  private float startTime;
  private float score = 0f;
  private float scoreMultiplier = 10f;

  private void Start()
  {
    startTime = Time.time;
  }

  private void Update()
  {
    // Calculate the score based on the elapsed time since the game started
    float elapsedSeconds = Time.time - startTime;
    score = elapsedSeconds * scoreMultiplier;

    // Update the UI text to show the current score
    scoreText.text = Mathf.RoundToInt(score).ToString();
  }
}
