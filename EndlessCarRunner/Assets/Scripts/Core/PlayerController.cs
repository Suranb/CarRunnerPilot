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
    private float laneSwitchProgress = 1f;
    private readonly float switchThreshold = 0.2f;  // Minimum time after which player can switch lanes again

    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Quaternion targetRotation;
    [SerializeField] private Quaternion initialRotation;
    [SerializeField] private float laneChangeRotation = 12f;

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
      }

      if (laneSwitchProgress >= 1f && transform.rotation != initialRotation)
      {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, rotationSpeed * Time.deltaTime);
      }
    }

    private void SwitchLane(int laneChange, Vector3 direction, float rotationChange)
    {
      currentLane += laneChange;
      startPosition = transform.position;
      targetPosition += direction * laneOffset;
      targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z + rotationChange);
      laneSwitchProgress = 0f; // Reset progress
    }
  }
}