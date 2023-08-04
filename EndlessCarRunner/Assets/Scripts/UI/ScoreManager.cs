using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
  [SerializeField] private Text scoreText;
  [SerializeField] private Text coinTextValue;
  [SerializeField] private float scoreMultiplier = 10f;
  [SerializeField] private TileManager tileManager;

  private float score = 0f;
  private float lastFrameTime = 0f;
  private int coinAmount = 0;

  private void Update()
  {
    float currentFrameTime = Time.time;
    float frameDeltaTime = currentFrameTime - lastFrameTime;
    lastFrameTime = currentFrameTime;

    float scoreForFrame = frameDeltaTime * scoreMultiplier * tileManager.TileSpeedMultiplier;
    score += scoreForFrame;

    scoreText.text = Mathf.RoundToInt(score).ToString("000000");
  }

  public void CollectCoin(int coinValue)
  {
    coinAmount += coinValue;
    coinTextValue.text = coinAmount.ToString();
  }
}
