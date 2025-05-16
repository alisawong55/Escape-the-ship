using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIManager _uiManager;

    public void GameOver()
    {
        _uiManager.ShowGameOverUI(true);
        PauseGame(true);
    }
    public void Victory(){
        _uiManager.ShowVictoryUI(true);
        PauseGame(true);
    }
    public void Restart()
    {
        _uiManager.DisableUI();
        PauseGame(false);
        //set up new game
        // Reload the currently active scene
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private bool IsGameOnPause()
    {
        return Time.timeScale == 0;
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void Quit()
    {
        SceneManager.LoadScene("main menu");
    }
}
