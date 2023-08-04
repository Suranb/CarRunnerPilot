using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
  [SerializeField] private Text scoreText;
  [SerializeField] private float scoreMultiplier = 10f;
  [SerializeField] private TileManager tileManager;
  private float score = 0f;
  private float lastFrameTime = 0f;

  private void Update()
  {
    float currentFrameTime = Time.time;
    float frameDeltaTime = currentFrameTime - lastFrameTime;
    lastFrameTime = currentFrameTime;

    // Calculate the score for this frame based on the time elapsed and score multiplier
    float scoreForFrame = frameDeltaTime * scoreMultiplier * tileManager.TileSpeedMultiplier;

    // Add the score for this frame to the total score
    score += scoreForFrame;

    // Update the UI text to show the current score
    scoreText.text = Mathf.RoundToInt(score).ToString();
  }
}
