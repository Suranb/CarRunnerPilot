using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CoinUI : MonoBehaviour
{
  [SerializeField] private CoinBehavior coin;
  [SerializeField] private Vector3 endScale = new Vector3(0.2f, 0.2f, 0.2f);
  private Canvas uiCanvas;
  private Image uiCoinPrefab;
  private ScoreManager scoreManager;

  private void Start()
  {
    scoreManager = FindObjectOfType<ScoreManager>();
    uiCanvas = FindObjectOfType<Canvas>();
    uiCoinPrefab = GameObject.FindGameObjectWithTag("UI_COIN").GetComponent<Image>();
    coin.OnCoinCollected += HandleCoinCollection;
  }

  private void HandleCoinCollection(int coinValue)
  {
    AnimateCoinToUI(coinValue);
  }

  private void AnimateCoinToUI(int coinValue)
  {
    Image coinCopy = Instantiate(uiCoinPrefab, uiCanvas.transform);
    Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
    coinCopy.transform.position = screenPosition;

    coinCopy.transform.DOMove(uiCoinPrefab.transform.position, 1f).SetEase(Ease.InBack);
    coinCopy.transform.DOScale(endScale, 1.0f).SetEase(Ease.InSine)
        .OnComplete(() =>
        {
          scoreManager.CollectCoin(coinValue);
          Destroy(coinCopy.gameObject);
        });
  }
}
