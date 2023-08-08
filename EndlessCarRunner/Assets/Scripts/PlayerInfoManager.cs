using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField] private GameObject userInfoObject;
    [SerializeField] private TextMeshProUGUI usernameInput;
    [SerializeField] private TextMeshProUGUI usernameDisplayField;
    [SerializeField] private Button submitButton;

    private const string USERNAME_KEY = "PlayerUsername";

    private void Start()
    {
        PlayerPrefs.DeleteKey("PlayerUsername");
        submitButton.onClick.AddListener(HandleSubmit);

        // Check if the username exists in PlayerPrefs
        if (PlayerPrefs.HasKey(USERNAME_KEY))
        {
            usernameDisplayField.text = PlayerPrefs.GetString(USERNAME_KEY);
            userInfoObject.SetActive(false);
        }
        else
        {
            userInfoObject.SetActive(true);
        }
    }

    void HandleSubmit()
    {
        string username = usernameInput.text;
        if (!string.IsNullOrEmpty(username))
        {
            PlayerPrefs.SetString(USERNAME_KEY, username);
            usernameDisplayField.text = username;
            userInfoObject.SetActive(false);
        }
    }
}
