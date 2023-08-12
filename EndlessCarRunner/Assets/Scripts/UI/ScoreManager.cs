using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
  [SerializeField] private Text scoreText;
  [SerializeField] private Text coinTextValue;
  [SerializeField] private float scoreMultiplier = 10f;
  [SerializeField] private TileManager tileManager;
  [SerializeField] private SoundController soundController;

  public float Score { get; private set; } = 0f;
  private float lastFrameTime = 0f;
  private int coinAmount = 0;

  private void Update()
  {
    UpdateScore();
  }

  private void UpdateScore()
  {
    float currentFrameTime = Time.time;
    float frameDeltaTime = currentFrameTime - lastFrameTime;
    lastFrameTime = currentFrameTime;

    float scoreForFrame = frameDeltaTime * scoreMultiplier * tileManager.TileSpeedMultiplier;
    Score += scoreForFrame;

    scoreText.text = Mathf.RoundToInt(Score).ToString("000000");
  }

  public void CollectCoin(int coinValue)
  {
    coinAmount += coinValue;
    soundController.PlaySound();
    coinTextValue.text = coinAmount.ToString();
  }
}
