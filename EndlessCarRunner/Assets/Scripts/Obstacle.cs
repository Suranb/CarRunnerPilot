using UnityEngine;

public class Obstacle : MonoBehaviour
{
  private IObstacle obstacleType;
  const string playerTag = "Player";

  private void Start()
  {
    // The obstacle type could be set in the inspector, loaded from data, etc.
    // This is just a placeholder
    obstacleType = new DestructibleObstacle();
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag(playerTag))
    {
      PlayerController playerController = other.GetComponent<PlayerController>();
      if (playerController != null)
      {
        obstacleType.OnPlayerHit(playerController);
        // Here, the exact behavior depends on the type of the obstacle
      }
    }
  }
}
