using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    [SerializeField] Sprite uiMap;
    [SerializeField] Sprite uiMapActive;


    [SerializeField] Sprite warningIcon;
    [SerializeField] Sprite dangerIcon;
    [SerializeField] Sprite bossIcon;
    [SerializeField] Sprite lootIcon;
    [SerializeField] Sprite clearIcon;
    [SerializeField] Sprite empty;

    
    Sprite UIImage;
    MapGenerator mapGenerator;
    GameObject[,] roomsUI;
    [SerializeField] Transform uiMapContainer;

    void Start(){
        mapGenerator = FindObjectOfType<MapGenerator>();
        roomsUI = mapGenerator.roomsUI;
    }

    public void SetActiveRoomUI(GameObject room, bool active){
        if(active){
            UIImage = uiMapActive;
            //StartCoroutine(ShowMapUI());
        }else{
            UIImage = uiMap;
        }

        Room roomComponent = room.GetComponent<Room>();
        Image mapIcon = roomsUI[roomComponent.y, roomComponent.x].gameObject.GetComponent<Image>();
        mapIcon.sprite = UIImage;
        
        //show map ui when player change room
    }

    public void SetMapTypeUI(GameObject room, string roomType){
            switch (roomType)
            {
            case "loot":
                UIImage = lootIcon;
                break;
            case "warning":
                UIImage = warningIcon;
                break;
            case "danger":
                UIImage = dangerIcon;
                break;
            case "boss":
                UIImage = bossIcon;
                break;
            case "clear":
                UIImage = clearIcon;
                break;
            default:
                UIImage = empty;
                break;
            }

        Room roomComponent = room.GetComponent<Room>();
        roomsUI = mapGenerator.roomsUI;
        if (roomsUI == null)
        {     
        Debug.LogError("roomsUI or specific room UI is null.");
        return;
        }

        Image mapIcon = roomsUI[roomComponent.y, roomComponent.x].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        mapIcon.sprite = UIImage;


    }

    IEnumerator ShowMapUI(){
        uiMapContainer.gameObject.SetActive(true);
        yield return  new WaitForSeconds(0.5f);
        uiMapContainer.gameObject.SetActive(false);
    }
}
