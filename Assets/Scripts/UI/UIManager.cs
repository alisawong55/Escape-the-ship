using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameOverUI GameOverUI;
    [SerializeField] GameObject victoryUI;
    [SerializeField] GameManager _gameManager;
    [SerializeField] Image LoadingScreen;
    [SerializeField] TMP_Text nameText;
    [SerializeField] GameObject settingUI;

    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName");
        nameText.text = playerName;
    }

        public void DisableUI()
        {
            GameOverUI.gameObject.SetActive(false);
            victoryUI.SetActive(false);
        }
        public void ShowVictoryUI(bool a){
            victoryUI.SetActive(a);
        }

        public void ShowSettingUI(){
            _gameManager.PauseGame(!settingUI.active);
            settingUI.SetActive(!settingUI.active);
        }

        public void ShowGameOverUI(bool allowRestart)
        {
            SetupGameOverUI();
            GameOverUI.gameObject.SetActive(true);
            if (!allowRestart)
            {
                GameOverUI.Restart.interactable = false;
            }
        }

        public void HideGameOverUI()
        {
            _gameManager.PauseGame(false);
            GameOverUI.gameObject.SetActive(false);
        }


        private void SetupGameOverUI()
        {
            GameOverUI.Quit.onClick.AddListener(() =>
            {
                _gameManager.Quit();
            });

            GameOverUI.Restart.onClick.AddListener(() =>
            {
                _gameManager.Restart();
            });
        }
}
