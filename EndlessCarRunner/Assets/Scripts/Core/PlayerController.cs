using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float laneOffset = 3f; // The distance between each lane
  public float laneSwitchSpeed = 10f; // Speed of lane switching
  public int currentLane = 1; // The current lane (0 for left, 1 for middle, 2 for right)

  private Vector3 targetPosition; // The position to which the car will move

  private void Start()
  {
    targetPosition = transform.position;
  }

  private void Update()
  {
    // Detect lane switch input (for simplicity, use keyboard input here, but you can adapt it to touch controls for mobile)
    if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
    {
      currentLane--;
      targetPosition += Vector3.left * laneOffset;
    }
    else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2)
    {
      currentLane++;
      targetPosition += Vector3.right * laneOffset;
    }

    // Move the car smoothly towards the target position
    transform.position = Vector3.Lerp(transform.position, targetPosition, laneSwitchSpeed * Time.deltaTime);
  }
}
