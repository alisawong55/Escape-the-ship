using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using TMPro;

public class NameInput : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] TMP_Text displayWarningText;

    public void SaveName()
    {
        string playerName = nameInput.text.Trim();

        if (string.IsNullOrEmpty(playerName))
        {
            displayWarningText.text = "Please enter name.";
            return;
        }

        // Check for invalid characters using a regular expression
        if (!Regex.IsMatch(playerName, @"^[a-zA-Z0-9_]+$"))
        {
            displayWarningText.text = "Invalid characters in name.";
            return;
        }

        PlayerPrefs.SetString("PlayerName", playerName);
        SceneManager.LoadScene("cut screen");
    }
}
