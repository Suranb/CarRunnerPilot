using UnityEngine;
using UnityEngine.UI;
using CarRunner.Player;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
  [SerializeField] private PlayerHealth playerHealth;
  [SerializeField] private TextMeshProUGUI healthDisplayValue;

  private void Start()
  {
    healthDisplayValue.text = playerHealth.GetPlayerHealth().ToString();
    playerHealth.OnHealthChanged += UpdateHealthDisplay;
  }

  private void UpdateHealthDisplay(int currentHealth)
  {
    healthDisplayValue.text = currentHealth.ToString();
  }

  private void OnDestroy()
  {
    // Always good to unsubscribe from events when the object is destroyed.
    playerHealth.OnHealthChanged -= UpdateHealthDisplay;
  }
}
