using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndingDialog : MonoBehaviour
{
    [SerializeField] TMP_Text displayText;
    [SerializeField] TMP_Text playerName;
    [SerializeField] Image storyImage;
    [SerializeField] Sprite[] storyImages;
    int count = 0;

    string[] dialogText;
    
    void Start()
    {
        playerName.text = PlayerPrefs.GetString("PlayerName");
        dialogText = new string[10];
        dialogText[0] = "world saved";
        dialogText[1] = "woah";
        dialogText[2] = "gg";
        nextDialog();
    }

    public void nextDialog(){
        displayText.text = dialogText[count];
        //storyImage.sprite = storyImages[count];
        
        if(count == 8){
            //end cut screen
            SceneManager.LoadScene("main menu");
            return;
        }
        count++;
    }

    public void SkipDialog(){
        SceneManager.LoadScene("main menu");
    }
}