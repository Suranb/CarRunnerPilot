using UnityEngine;

namespace CarRunner.Player
{
  public class PlayerController : MonoBehaviour
  {
    public float laneOffset = 3f;
    public float laneSwitchSpeed = 10f; // TODO-Suran: Here we can make this value less by upgrading the Car/wheels or picking up ability such a Michelin wheels or something?
    public int currentLane = 1;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    [SerializeField] private float laneSwitchProgress = 1f;
    private readonly float switchThreshold = 0.2f;  // Minimum time after which player can switch lanes again

    [SerializeField] private float rotationSpeed = 45f;
    [SerializeField] private Quaternion targetRotation;
    [SerializeField] private Quaternion initialRotation;
    [SerializeField] private float laneChangeRotation = 12f;

    [SerializeField] private float bumpMagnitude = 5f;  // The rotational offset induced by the bump
    [SerializeField] private float bumpSettleSpeed = 25f;  // How quickly the car settles back after the bump
    private float currentBumpRotation = 0f;  // Tracks the current rotation due to the bump

    private void Start()
    {
      startPosition = transform.position;
      targetPosition = startPosition;
      initialRotation = transform.rotation;
      targetRotation = initialRotation;
    }

    private void Update()
    {
      // Detect lane switch input
      if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0 && laneSwitchProgress >= switchThreshold)
      {
        SwitchLane(-1, Vector3.left, -laneChangeRotation);
      }
      else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2 && laneSwitchProgress >= switchThreshold)
      {
        SwitchLane(1, Vector3.right, laneChangeRotation);
      }

      if (laneSwitchProgress < 1f)
      {
        laneSwitchProgress += Time.deltaTime * laneSwitchSpeed;
        transform.position = Vector3.Lerp(startPosition, targetPosition, laneSwitchProgress);
        transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, laneSwitchProgress);

        if (Mathf.Abs(currentBumpRotation) > 0f)
        {
          float settleAmount = bumpSettleSpeed * Time.deltaTime;
          currentBumpRotation = Mathf.MoveTowards(currentBumpRotation, 0f, settleAmount);
          Quaternion bumpRotation = Quaternion.Euler(0f, 0f, currentBumpRotation);
          transform.rotation = transform.rotation * bumpRotation;
        }
      }

      if (transform.rotation != initialRotation)
      {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, rotationSpeed * Time.deltaTime);
      }
    }

    private void SwitchLane(int laneChange, Vector3 direction, float rotationChange)
    {
      currentLane += laneChange;
      startPosition = transform.position;
      targetPosition += direction * laneOffset;
      currentBumpRotation = laneChange < 0 ? bumpMagnitude : -bumpMagnitude;  // Adjust the direction of the bump
      targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y + rotationChange, initialRotation.eulerAngles.z + currentBumpRotation);
      laneSwitchProgress = 0f; // Reset progress
    }
  }
}