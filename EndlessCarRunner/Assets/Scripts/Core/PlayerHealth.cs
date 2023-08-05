using UnityEngine;

namespace CarRunner.Player
{
  public class PlayerHealth : MonoBehaviour
  {
    public static PlayerHealth Instance { get; private set; } // Static instance
    [SerializeField] private int health = 3;

    private void Awake()
    {
      if (Instance == null)
      {
        Instance = this;
      }
      else
      {
        Destroy(gameObject);
      }
    }

    public void TakeDamage(int damage)
    {
      health -= damage;

      if (health <= 0)
      {
        Debug.Log("Game Over");
        // TODO: Add game over logic here
      }
    }
  }
}
