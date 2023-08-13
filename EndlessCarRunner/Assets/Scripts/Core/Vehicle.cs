using System.Collections;
using UnityEngine;

namespace CarRunner.Vehicle
{
  [RequireComponent(typeof(AudioSource))]
  public class Vehicle : MonoBehaviour
  {
    private readonly string PLAYER_TAG_NAME = "Player";

    [SerializeField] private AudioClip honkClip;
    [SerializeField] private float driveSpeed = 5f;
    [SerializeField] ParticleSystem hitEffect;
    private Renderer vehicleRenderer;

    private AudioSource audioSource;

    private void Start()
    {
      vehicleRenderer = GetComponent<Renderer>();
      audioSource = GetComponent<AudioSource>();
      if (honkClip != null) audioSource.clip = honkClip;
    }

    private void Update()
    {
      Drive();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.CompareTag(PLAYER_TAG_NAME))
      {
        Debug.Log("Hit player!");
        Honk();
        PlayHitEffect();
      }
    }

    protected virtual void PlayHitEffect()
    {
      hitEffect.Play();
      gameObject.SetActive(false);
      DestroyVehicle();
    }

    protected virtual void DestroyVehicle()
    {
      Destroy(gameObject, 5f);
    }

    public virtual void Drive()
    {
      // Move the vehicle forward in the Z-axis at the specified speed.
      transform.Translate(driveSpeed * Time.deltaTime * Vector3.forward, Space.World);
    }

    public virtual void Honk()
    {
      audioSource.Play();
    }

    private void DestroyVehicle(float delay = 0f)
    {
      Destroy(gameObject, delay);
    }


  }
}
