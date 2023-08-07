using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    [Header("Coin Settings")]
    [SerializeField] private int coinValue = 1;
    [SerializeField] private float floatAmplitude = 0.2f;
    [SerializeField] private float floatFrequency = 1f;
    [SerializeField] private float rotationSpeed = 2f;
    private readonly float minYPosition = 0.25f;

    public delegate void CoinCollectedHandler(int value);
    public event CoinCollectedHandler OnCoinCollected;

    private void Update()
    {
        //ApplyFloatingAnimation();
        //ApplyRotationAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    private void CollectCoin()
    {
        OnCoinCollected?.Invoke(coinValue);
        this.gameObject.SetActive(false);
    }

    private void ApplyFloatingAnimation()
    {
        Vector3 pos = transform.position;
        float floatingHeight = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        pos.y = Mathf.Clamp(floatingHeight, minYPosition, float.MaxValue);
        transform.position = pos;
    }

    private void ApplyRotationAnimation()
    {
        float randomX = Random.Range(0f, 360f);
        transform.Rotate(Vector3.forward, randomX * rotationSpeed * Time.deltaTime);
    }
}