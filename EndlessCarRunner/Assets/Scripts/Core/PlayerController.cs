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
    private float laneSwitchProgress = 1f;  // To keep track of Lerp progress


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
      if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0 && laneSwitchProgress >= 1f)
      {
        SwitchLane(-1, Vector3.left, -laneChangeRotation);
      }
      else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2 && laneSwitchProgress >= 1f)
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
        targetRotation = initialRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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

    /*TODO-SURAN: Remove this Method to its own Component to handle all collisions? */
    /*
    private void OnCollisionEnter(Collision collision)
    {
      if (!hasCollided && collision.gameObject.layer == LayerMasks.Obstacle)
      {
        hasCollided = true;
        Debug.Log("Collision with object on specified layer!");

        // Lets do our thing here before we set the hasCollided back to false
        // Maybe loose health or add damage to the car where smoke is coming out?
      }
    }
    */
  }
}