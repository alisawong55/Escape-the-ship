using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //number of room column*row
    [SerializeField] public int columns = 3;
    [SerializeField] public int rows = 3;

    //object perfab
    [SerializeField] GameObject roomPrefab;
    [SerializeField] GameObject uiRoomPrefab;
    [SerializeField] Transform uiMapContainer;

    public GameObject[,] rooms;
    public GameObject[,] roomsUI;
    Transform gameMap;

    void Awake(){
        rooms = new GameObject[rows, columns];
        roomsUI = new GameObject[rows, columns];
        RoomSetup();
        UIMapSetup();
    }
    
    void RoomSetup()
    {
        Vector2 position = new Vector2(0, 0);
        gameMap = new GameObject("map").transform;
        //gen col*row room
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                //Instantiate what is set to toInsutantiate
                GameObject instance = Instantiate(roomPrefab, position, Quaternion.identity) as GameObject;
                rooms[x, y] = instance;
                instance.transform.SetParent(gameMap);

                Room room = instance.GetComponent<Room>();
                // set position
                room.x = x;
                room.y = y;
                //Debug.Log("Room at (" + x + ", " + y + ")");

                room.RandomDoor();
                // Activate doors based on position
                
                if(x == 0){ //top room
                    room.DisableDoor(0);
                }
                if(x == rows - 1){ //bottom room
                    room.DisableDoor(1);
                }
                if(y == 0){ //left room
                    room.DisableDoor(2);
                }
                if(y == columns - 1){ //right room
                    room.DisableDoor(3);
                }
                
                /*if(x != 0 && x != rows - 1 && y != 0 && y != columns - 1){
                    room.RandomDoor();
                }*/
                
                position += Vector2.left * -45f ; //change position by room size -45f

            }
            position.x = 0; //reset x
            position += Vector2.up * -20f; //change position by room size -20f
        }
    }

    void UIMapSetup()
    {
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                GameObject instance = Instantiate(uiRoomPrefab, uiMapContainer);
                RectTransform rt = instance.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(y * rt.rect.width - 380, -x * rt.rect.height + 100); // Adjust position based on cell size

                roomsUI[x, y] = instance;
                
            }
        }
    }
}
