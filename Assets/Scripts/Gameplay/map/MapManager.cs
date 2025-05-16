using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{

    public GameObject activeRoom; //room where player in
    public GameObject startingRoom;
    public GameObject exitRoom;
    public GameObject centerRoom;

    MapGenerator mapGenerator;
    EnemyManager enemyManager;
    AudioController audioController;
    GameManager gameManager;
    GameObject[,] rooms;
    GameObject[,] roomsUI;
    List<GameObject> bossRooms = new List<GameObject>();
    int columns;
    int rows;
    Player player;
    Camera camera;

    [SerializeField] AudioClip doorSfx;
    [SerializeField] AudioClip victorySfx;
    [SerializeField] AlertMessage alertMessage;

    //---------------------------------------------------------------
    [SerializeField] Sprite uiMap;
    [SerializeField] Sprite uiMapActive;

    [SerializeField] Sprite warningIcon;
    [SerializeField] Sprite dangerIcon;
    [SerializeField] Sprite bossIcon;
    [SerializeField] Sprite lootIcon;
    [SerializeField] Sprite clearIcon;
    [SerializeField] Sprite exitIcon;
    [SerializeField] Sprite empty;
    
    Sprite UIImage;
    [SerializeField] GameObject uiMapContainer;
    

    void Awake()
    {
        player = FindObjectOfType<Player>();
        camera = FindObjectOfType<Camera>();
        mapGenerator = FindObjectOfType<MapGenerator>();
        enemyManager = FindObjectOfType<EnemyManager>();
        audioController = FindObjectOfType<AudioController>();
        gameManager = FindObjectOfType<GameManager>();
        rooms = mapGenerator.rooms;
        roomsUI = mapGenerator.roomsUI;
        columns = mapGenerator.columns;
        rows = mapGenerator.rows;
        
        SetRoomType();
        StartCoroutine(AutoShowMapUI());
        StartCoroutine(alertMessage.showAlert("test message"));

    }

    IEnumerator SetActiveRoom(GameObject room){
        //set and load room component
        //ทุกการเปลี่ยนห้องของผู้เล่น ไวรัสเอเลี่ยน(กลางแมพ)จะแพร่ออกไป1จุดข้างเคียงที่มีประตู
        if(bossRooms.Count > 0){
            int randomRoom = Random.Range(0, bossRooms.Count - 1);
            SetBossRoom(bossRooms[randomRoom]);
        }

        Room roomComponent = room.GetComponent<Room>();
        activeRoom = room;
        Debug.Log("active room (" + roomComponent.x + ", " + roomComponent.y + ")");

        SetActiveRoomUI(activeRoom, true);
        StartCoroutine(AutoShowMapUI());
        yield return new WaitForSeconds(1.5f); //wait map load
        
        if(roomComponent.roomClear == false){
            roomComponent.RoomSetup();
            roomComponent.roomClear = true;
        }

        if(roomComponent.roomType == "exit"){
            StartCoroutine(alertMessage.showAlert("enter right door to win"));
        }
        
    }

    void SetRoomType(){
        startingRoom = rooms[rows / 2, 0]; //middle row first col
        exitRoom = rooms[rows / 2, columns - 1]; //middle row last col
        centerRoom = rooms[rows / 2, columns / 2]; //center room = boss
        activeRoom = startingRoom;

        //set start room
        Room roomComponent = activeRoom.GetComponent<Room>();
        roomComponent.roomClear = true;
        roomComponent.roomType = "start";

        //random room type
        SetWarningRoom(7);
        SetDangerRoom(5);
        SetLootRoom();

        //set boss room
        roomComponent = centerRoom.GetComponent<Room>();
        roomComponent.roomType = "boss";
        SetMapTypeUI(centerRoom, "boss");
        bossRooms.Add(centerRoom);
        //set exit room
        roomComponent = exitRoom.GetComponent<Room>();
        roomComponent.roomType = "exit";
        roomComponent.ActivateDoor(3);
        SetMapTypeUI(exitRoom, "exit");
        //Debug.Log("boss room (" + roomComponent.x + ", " + roomComponent.y + ")");
        

        //teleport player to starting room
        player.transform.position = activeRoom.transform.position;
        camera.transform.position = activeRoom.transform.position;
        camera.transform.position += Vector3.back * 10f;
        
        
    }
    void SetBossRoom(GameObject bossRoom){
        //when player change room
        Room bossRoomComponent = bossRoom.GetComponent<Room>();
        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { -1, 1, 0, 0 };
        int i = Random.Range(0, 4);

        int nx = bossRoomComponent.x + dx[i];
        int ny = bossRoomComponent.y + dy[i];
        // Check if the neighboring room is within the bounds of the rooms array
            if (nx >= 0 && nx < rows && ny >= 0 && ny < columns){
            
                GameObject newBossRoom = rooms[nx, ny];
                Room roomComponent = newBossRoom.GetComponent<Room>();
                string roomType = roomComponent.roomType;
                if(roomType != "boss" && roomType != "exit"){
                    roomComponent.roomType = "boss";
                    roomComponent.roomClear = false;
                    SetMapTypeUI(newBossRoom, "boss");
                    bossRooms.Add(newBossRoom);
                    //Debug.Log("boss room (" + roomComponent.x + ", " + roomComponent.y + ")");
                }
            }

    }


    void SetLootRoom(){
        //first loot room
        int randomX = Random.Range(-1, 2);
        int randomY = Random.Range(1, 2);
        Room roomComponent = startingRoom.GetComponent<Room>();
        GameObject lootRoom = rooms[roomComponent.x + randomX, roomComponent.y + randomY];
        roomComponent = lootRoom.GetComponent<Room>();
        roomComponent.roomType = "loot1";
        Debug.Log("first loot room (" + roomComponent.x + ", " + roomComponent.y + ")");
        SetMapTypeUI(lootRoom, "loot");

        //second loot room
        randomX = Random.Range(-1, 2);
        randomY = Random.Range(0, 2);
        roomComponent = lootRoom.GetComponent<Room>();
        GameObject nextLootRoom = rooms[roomComponent.x + randomX, roomComponent.y + randomY];
        roomComponent = nextLootRoom.GetComponent<Room>();
        roomComponent.roomType = "loot2";
        Debug.Log("second loot room (" + roomComponent.x + ", " + roomComponent.y + ")");
        SetMapTypeUI(nextLootRoom, "loot");

        
    }

    void SetWarningRoom(int number){
        //random location
        
        for (int i = 0; i < number; i++){
            int randomX = Random.Range(0, rows);
            int randomY = Random.Range(0, columns);
            GameObject warningRoom = rooms[randomX, randomY];
            Room roomComponent = warningRoom.GetComponent<Room>();
            string roomType = roomComponent.roomType;
            if(roomType != "danger" && roomType != "loot1" && roomType != "loot2" && roomType != "boss" && roomType != "start"){
                roomComponent.roomType = "warning";
                SetMapTypeUI(warningRoom, "warning");
                //Debug.Log("warning room (" + roomComponent.x + ", " + roomComponent.y + ")");
                
            }
        }

    }

    void SetDangerRoom(int number){
        //random location
        
        for (int i = 0; i < number; i++){
            int randomX = Random.Range(0, rows);
            int randomY = Random.Range(0, columns);
            GameObject warningRoom = rooms[randomX, randomY];
            Room roomComponent = warningRoom.GetComponent<Room>();
            string roomType = roomComponent.roomType;
            if(roomType != "warning" && roomType != "loot1" && roomType != "loot2"  && roomType != "boss" && roomType != "start"){
                roomComponent.roomType = "danger";
                SetMapTypeUI(warningRoom, "danger");
                //Debug.Log("warning room (" + roomComponent.x + ", " + roomComponent.y + ")");
                
            }
        }

    }

    /*void SetDangerRoom(GameObject bossRoom){
        //all room around boss room is danger room
        Room bossRoomComponent = bossRoom.GetComponent<Room>();
        
        
        int[] dx = { 0, 0, -1, 1 };
        int[] dy = { -1, 1, 0, 0 };
        
        for (int i = 0; i < 4; i++)
        {
            int nx = bossRoomComponent.x + dx[i];
            int ny = bossRoomComponent.y + dy[i];

            // Check if the neighboring room is within the bounds of the rooms array
            if (nx >= 0 && nx < rows+1 && ny >= 0 && ny < columns+1){
            
                GameObject dangerRoom = rooms[bossRoomComponent.x + dx[i], bossRoomComponent.y + dy[i]];
                Room roomComponent = dangerRoom.GetComponent<Room>();
                string roomType = roomComponent.roomType;
                if(roomType != "danger" && roomType != "loot" && roomType != "boss"){
                    roomComponent.roomType = "danger";
                    SetMapTypeUI(dangerRoom, "danger");
                    //Debug.Log("danger room (" + roomComponent.x + ", " + roomComponent.y + ")");
                }   
            }
        }
    }*/

    public void PlayerEnterDoor(GameObject door)
    {
        if(enemyManager.enemyCount == 0){
        
        // Determine the direction of the door and get the corresponding room
        GameObject nextRoom = activeRoom;
        Room roomComponent = activeRoom.GetComponent<Room>();
            if(roomComponent.roomType == "exit" && door.CompareTag("DoorRight")){
                //win
                gameManager.Victory();
                audioController.CreateSFX(victorySfx, door.transform.position);
                Debug.Log("you win");
                return;
            }



        SetActiveRoomUI(activeRoom, false);
        SetMapTypeUI(activeRoom, "clear");
        if(roomComponent.roomType == "boss"){
            bossRooms.Remove(activeRoom);
        }
        audioController.CreateSFX(doorSfx, door.transform.position);


        //teleport player to room
        if (door.CompareTag("DoorUp"))
        {
            nextRoom = rooms[roomComponent.x - 1, roomComponent.y];
            player.transform.position = nextRoom.transform.position + Vector3.up * -4f;

        }
        else if (door.CompareTag("DoorDown"))
        {
            nextRoom = rooms[roomComponent.x + 1, roomComponent.y];
            player.transform.position = nextRoom.transform.position + Vector3.up * 4f;
        }
        else if (door.CompareTag("DoorLeft"))
        {
            nextRoom = rooms[roomComponent.x, roomComponent.y - 1];
            player.transform.position = nextRoom.transform.position + Vector3.left * -12f;
        }
        else if (door.CompareTag("DoorRight"))
        {
            nextRoom = rooms[roomComponent.x, roomComponent.y + 1];
            player.transform.position = nextRoom.transform.position + Vector3.left * 12f;
        }


        camera.transform.position = nextRoom.transform.position;
        camera.transform.position += Vector3.back * 10f;
        StartCoroutine(SetActiveRoom(nextRoom));

        }
        
    }

    //-----------------------------------------------------------------
    //-----------------------------------------------------------------
    //-----------------------------------------------------------------
    public void SetActiveRoomUI(GameObject room, bool active){
        if(active){
            UIImage = uiMapActive;
            //StartCoroutine(ShowMapUI());
        }else{
            UIImage = uiMap;
        }

        Room roomComponent = room.GetComponent<Room>();
        Image mapIcon = roomsUI[roomComponent.x, roomComponent.y].gameObject.GetComponent<Image>();
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
            case "exit":
                UIImage = exitIcon;
                break;
            default:
                UIImage = empty;
                break;
            }

        Room roomComponent = room.GetComponent<Room>();
        Image mapIcon = roomsUI[roomComponent.x, roomComponent.y].gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        mapIcon.sprite = UIImage;
        mapIcon.gameObject.AddComponent<ImagePop>();

    }

    public void ShowMapUI(){
        if(uiMapContainer.activeSelf == true){
            uiMapContainer.SetActive(false);
            gameManager.PauseGame(false);
        }else{
            uiMapContainer.SetActive(true);
            gameManager.PauseGame(true);
        }
    }

    public IEnumerator AutoShowMapUI(){
        uiMapContainer.SetActive(true);
        //cant move when map show
        player.isAttacking = true;
        yield return new WaitForSeconds(1.5f);
        uiMapContainer.SetActive(false);
        player.isAttacking = false;
    }
}
