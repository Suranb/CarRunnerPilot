using UnityEngine;
using CarRunner.Player;

namespace CarRunner.Obstacles
{
  public abstract class Obstacle : MonoBehaviour
  {
    [SerializeField] private int damage;

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
      PlayerHealth.Instance.TakeDamage(damage);
    }

    public abstract void PlayDestroyAnimation();
    public abstract void PlayDestroyEffects();
  }
}