using DG.Tweening;
using UnityEngine;

namespace CarRunner.Player
{
  public class PlayerController : MonoBehaviour
  {
    public float laneOffset = 3f;
    public float laneSwitchSpeed = 10f; // TODO-Suran: Here we can make this value less by upgrading the Car/wheels or picking up ability such a Michelin wheels or something?
    public int currentLane = 1;
    private Vector3 targetPosition;
    //private bool hasCollided = false;
    private bool isLaneSwitching = false;
    private Tween moveTween;
    private Tween rotateTween;


    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Quaternion targetRotation;
    [SerializeField] private Quaternion initialRotation;
    [SerializeField] private float laneChangeRotation = 12f;

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
        SwitchLane(-1, Vector3.left, -laneChangeRotation);
      }
      else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2)
      {
        SwitchLane(1, Vector3.right, laneChangeRotation);
      }

      if (Vector3.Distance(transform.position, targetPosition) < 1f && isLaneSwitching)
      {
        isLaneSwitching = false;
        targetRotation = initialRotation;
      }

      if (rotateTween != null && rotateTween.IsActive()) rotateTween.Kill();
      rotateTween = transform.DORotateQuaternion(targetRotation, rotationSpeed * Time.deltaTime);

      if (moveTween != null && moveTween.IsActive()) moveTween.Kill();
      moveTween = transform.DOMove(targetPosition, laneSwitchSpeed * Time.deltaTime).SetEase(Ease.InSine);

    }

    private void SwitchLane(int laneChange, Vector3 direction, float rotationChange)
    {
      isLaneSwitching = true;
      currentLane += laneChange;
      targetPosition += direction * laneOffset;
      targetRotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, initialRotation.eulerAngles.z + rotationChange);
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