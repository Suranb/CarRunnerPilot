using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CoinUI : MonoBehaviour
    {
        [SerializeField] private CoinBehavior coin;
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

            var scale = new Vector3(Random.Range(0f, 1.0f), Random.Range(0f, 1.0f),
                Random.Range(0f, 1.0f));

            coinCopy.transform.DOMove(uiCoinPrefab.transform.position, 1f).SetEase(Ease.InOutExpo);
            coinCopy.transform.DOScale(scale, 1f).SetEase(Ease.InOutExpo)
                .OnComplete(() =>
                {
                    scoreManager.CollectCoin(coinValue);
                    Destroy(coinCopy.gameObject);
                });
        }
    }
}