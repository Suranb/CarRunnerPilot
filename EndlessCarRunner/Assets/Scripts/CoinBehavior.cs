using System.Collections;
using UnityEngine;

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