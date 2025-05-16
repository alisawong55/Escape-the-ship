using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Dialog : MonoBehaviour
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
        dialogText[0] = "Woah, It's been a long time since I saw anything like this...";
        dialogText[1] = "O_O";
        dialogText[2] = "what happening.";
        dialogText[3] = "something wrong here, I must check...";
        dialogText[4] = "Ahhh, my ship was invasted by aliens.\nWhy they targeted me for no reason.\nI just want to relax and take a break from space work.";
        dialogText[5] = "My crewmate get killed. only me alive.";
        dialogText[6] = "What a very bad day. I must survive and get out of here.";
        dialogText[7] = "Here we go, grab blaster and shoot them out.";
        nextDialog();
    }

    public void nextDialog(){
        displayText.text = dialogText[count];
        //storyImage.sprite = storyImages[count];
        
        if(count == 8){
            //end cut screen
            SceneManager.LoadScene("game screen");
            return;
        }
        count++;
    }

    public void SkipDialog(){
        SceneManager.LoadScene("game screen");
    }
}
