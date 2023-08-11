using System.Collections;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    [Header("Coin Settings")]
    [SerializeField] private int coinValue = 1;

    public delegate void CoinCollectedHandler(int value);
    public event CoinCollectedHandler OnCoinCollected;
    private AudioSource audioSource;
    [SerializeField] private GameObject coinChild;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
}