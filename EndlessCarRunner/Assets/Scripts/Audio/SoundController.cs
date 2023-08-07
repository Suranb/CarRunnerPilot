using UnityEngine;

public class SoundController : MonoBehaviour
{
  [SerializeField] private AudioSource audioSource;

  private void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }

  public void PlaySound()
  {
    audioSource.Play();
  }

  // If you want to stop the sound
  public void StopSound()
  {
    if (audioSource.isPlaying)
    {
      audioSource.Stop();
    }
  }
}
