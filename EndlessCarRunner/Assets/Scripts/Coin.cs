using UnityEngine;

public class Coin : MonoBehaviour
{
  private ScoreManager scoreManager;
  [SerializeField] private int coinValue = 1; // Value of the coin when collected

  // Floating animation variables
  public float floatAmplitude = 0.2f; // Amplitude of the floating motion
  public float floatFrequency = 1f; // Frequency of the floating motion

  // Rotation animation variables
  public float rotationSpeed = 2f; // Speed of rotation
  private readonly float minYPosition = 0.25f;

  private void Start()
  {
    scoreManager = FindObjectOfType<ScoreManager>();

    if (scoreManager == null) throw new("Cant find Score manager!!");
  }

  private void Update()
  {
    ApplyFloatingAnimation();
    ApplyRotationAnimation();
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

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log($"Coin hit by {other.gameObject.name}");
    if (other.CompareTag("Player"))
    {
      scoreManager.CollectCoin(coinValue);
      Destroy(gameObject);
    }
  }
}
