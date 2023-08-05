using UnityEngine;
using CarRunner.Player;

namespace CarRunner.Obstacles
{
  public abstract class Obstacle : MonoBehaviour
  {
    [SerializeField] private int damage;

    // Declare your common methods here
    public virtual void OnTriggerEnter(Collider other)
    {
      if (other.gameObject.CompareTag("Player"))
      {
        TakeDamage();
        PlayDestroyAnimation();
        PlayDestroyEffects();
      }
    }

    public virtual void TakeDamage()
    {
      // Default behavior: damage the player
      PlayerHealth.Instance.TakeDamage(damage);
    }

    // Declare abstract methods for specific obstacle behavior
    public abstract void PlayDestroyAnimation();
    public abstract void PlayDestroyEffects();
  }
}