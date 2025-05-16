using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField]
    public Button playAgain;

    [SerializeField]
    public Button quit;

    void Start(){
        gameManager = FindObjectOfType<GameManager>();
        playAgain.onClick.AddListener(() =>
        {
            gameManager.Restart();
        });

        quit.onClick.AddListener(() =>
        {
            gameManager.Quit();
        });

    }

    
}