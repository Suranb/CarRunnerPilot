using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
  public float speed;
  public float damage;

  // Declare your common methods here
  public virtual void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      // Damage player
      // You can call specific function here which will be different for each Obstacle

      Debug.Log($"This gameobjects: {this.gameObject.name} got hit by the Player");

      PlayDestroyAnimation();
      PlayDestroyEffects();
    }
  }

  // Declare abstract methods for specific obstacle behavior
  public abstract void PlayDestroyAnimation();
  public abstract void PlayDestroyEffects();

}