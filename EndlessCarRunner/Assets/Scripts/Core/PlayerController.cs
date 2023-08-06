using DG.Tweening;
using UnityEngine;

namespace CarRunner.Player
{
  public class PlayerController : MonoBehaviour
  {
    public float laneOffset = 3f; // The distance between each lane
    public float laneSwitchSpeed = 2f; // Speed of lane switching
    public int currentLane = 1; // The current lane (0 for left, 1 for middle, 2 for right)
    private Vector3 targetPosition; // The position to which the car will move
    private bool hasCollided = false;
    private bool isLaneSwitching = false;


    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Quaternion targetRotation;
    [SerializeField] private Quaternion initialRotation;
    [SerializeField] private float laneChangeRotation = 15f;

    private void Start()
    {
      targetPosition = transform.position;
      initialRotation = transform.rotation;
      targetRotation = initialRotation;
    }

    private void Update()
    {
      // Detect lane switch input
      if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0)
      {
        isLaneSwitching = true;
        currentLane--;
        targetPosition += Vector3.left * laneOffset;
        targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z - laneChangeRotation);
      }
      else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2)
      {
        isLaneSwitching = true;
        currentLane++;
        targetPosition += Vector3.right * laneOffset;
        targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z + laneChangeRotation);
      }

      // If the car is close to its target position and was previously lane-switching, start transitioning back to the initial rotation
      if (Vector3.Distance(transform.position, targetPosition) < 1f && isLaneSwitching)
      {
        isLaneSwitching = false;
        targetRotation = initialRotation;
      }

      // Rotate the car
      transform.DORotateQuaternion(targetRotation, rotationSpeed * Time.deltaTime);

      // Move the car smoothly towards the target position
      transform.DOMove(targetPosition, laneSwitchSpeed * Time.deltaTime).SetEase(Ease.InSine);
    }

    private void OnCollisionEnter(Collision collision)
    {
      if (!hasCollided && collision.gameObject.layer == LayerMasks.Obstacle)
      {
        hasCollided = true;
        Debug.Log("Collision with object on specified layer!");

        // Lets do our thing here before we set the hasCollided back to false
        // Maybe loose health or add damage to the car where smoke is coming out?
        // 
      }
    }
  }
}