using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CoinBehavior : MonoBehaviour
{
    [Header("Coin Settings")]
    [SerializeField] private int coinValue = 1;
    [SerializeField] private float floatAmplitude = 0.2f;
    [SerializeField] private float floatFrequency = 1f;
    [SerializeField] private float rotationSpeed = 2f;

    public delegate void CoinCollectedHandler(int value);
    public event CoinCollectedHandler OnCoinCollected;
    private AudioSource audioSource;
    [SerializeField] private GameObject coinChild; // Reference to the child coin


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ApplyRotationAnimationDOTween();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            coinChild.SetActive(false);
            CollectCoin();
        }
    }
    private void CollectCoin()
    {
        OnCoinCollected?.Invoke(coinValue);
        Destroy(this.gameObject, audioSource.clip.length);
    }
    private void ApplyRotationAnimationDOTween()
    {
        float endValue = 360f;  // Completing a full rotation around the Y axis

        transform.DORotate(new Vector3(0, endValue, 0), rotationSpeed, RotateMode.FastBeyond360)  // FastBeyond360 allows unlimited rotations
            .SetEase(Ease.Linear) // Linear for a constant rotation speed
            .SetLoops(-1, LoopType.Yoyo); // Infinite loops with incremental rotation
    }
}