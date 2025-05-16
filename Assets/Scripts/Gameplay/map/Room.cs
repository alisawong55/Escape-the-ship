using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    //room object
    [SerializeField] GameObject block;


    //enemy spawner
    //[SerializeField] GameObject egg; //Lv0 hp10 DMG0
    [SerializeField] GameObject speedy; //Lv1 hp10 DMG10 range2
    [SerializeField] GameObject reaver; //Lv2 hp30 DMG10 range2, can dash
    [SerializeField] GameObject destroyer; //Lv2 hp20 DMG20 range7
    [SerializeField] GameObject behemoth; //Lv3 hp80 DMG30 range3, can drain hp
    [SerializeField] GameObject dominator; //Lv3 hp50 DMG20 range4, can stun
    [SerializeField] GameObject boss; //hp400 many ability, can summon enemy

    //item
    [SerializeField] GameObject blasterPickup;
    [SerializeField] GameObject flameThrowerPickup;
    [SerializeField] GameObject laserRiflePickup;
    [SerializeField] GameObject lightSaberPickup;

    //door
    [SerializeField] GameObject doorUp;
    [SerializeField] GameObject doorDown;
    [SerializeField] GameObject doorLeft;
    [SerializeField] GameObject doorRight;

    
    public string roomType; //normal, loot, warning, danger, boss
    public bool roomClear; //player pass this room, clear all enemy, empty room
    Transform roomMap;
    public int x; //room position
    public int y; //room position
    



    void Start()
    {
        roomMap = this.transform;
        
    }

    //how difficult enemy in this room
    public void RoomSetup(){
        //random generate enemy wall item other
        if(roomType == "normal"){// normal enemy Lv1, few Lv2, some loot
            LayoutObjectAtRandom(speedy, 2, 5);
            LayoutObjectAtRandom(reaver, 1, 3);
            LayoutObjectAtRandom(destroyer, 0, 2);
            LayoutObjectAtRandom(block, 2, 4);
        }
        else if(roomType == "loot1"){// weapon pickup and many item pickup, few enemy
            LayoutObjectAtRandom(speedy, 2, 5);
            LayoutObjectAtRandom(reaver, 1, 3);
            LayoutObjectAtRandom(lightSaberPickup, 1, 1);
            LayoutObjectAtRandom(block, 2, 4);
        }
        else if(roomType == "loot2"){// weapon pickup and many item pickup, few enemy
            LayoutObjectAtRandom(speedy, 2, 5);
            LayoutObjectAtRandom(destroyer, 1, 3);
            LayoutObjectAtRandom(laserRiflePickup, 1, 1);
            LayoutObjectAtRandom(block, 2, 4);
        }
        else if(roomType == "warning"){// enemy Lv2 and few Lv3, some loot
            LayoutObjectAtRandom(reaver, 1, 4);
            LayoutObjectAtRandom(destroyer, 1, 2);
            LayoutObjectAtRandom(dominator, 1, 2);
            LayoutObjectAtRandom(block, 2, 4);

        }
        else if(roomType == "danger"){// many dangerous enemy Lv3, some loot
            LayoutObjectAtRandom(destroyer, 1, 2);
            LayoutObjectAtRandom(dominator, 1, 3);
            LayoutObjectAtRandom(behemoth, 1, 3);
            LayoutObjectAtRandom(block, 2, 4);
        }
        else if(roomType == "boss"){// boss room no other enemy and no loot
            LayoutObjectAtRandom(boss, 1, 1);
        }
        else if(roomType == "start"){
            LayoutObjectAtRandom(blasterPickup, 1, 1);
        }
        //high level enemy dont spawn near starting room

        //loot weapon room near starting room


    }

    public void RandomDoor()
    {
        //int doorCount = Random.Range(3, 4);
        //int firstDoor;
        ActivateDoor(0);
        ActivateDoor(1);
        ActivateDoor(2);
        ActivateDoor(3);

        /*switch (doorCount)
        {
            case 1:
                ActivateDoor(Random.Range(0, 4));
                break;
            case 2:
                firstDoor = Random.Range(0, 4);
                ActivateDoor(firstDoor);
                ActivateDoor((firstDoor + 2) % 4);
                break;
            case 3:
                firstDoor = Random.Range(0, 4);
                ActivateDoor(firstDoor);
                ActivateDoor((firstDoor + 1) % 4);
                ActivateDoor((firstDoor + 2) % 4);
                break;
            case 4:
                ActivateDoor(0);
                ActivateDoor(1);
                ActivateDoor(2);
                ActivateDoor(3);
                break;
            default:
                break;
        }*/
    }

    public void ActivateDoor(int doorIndex)
    {
        switch (doorIndex)
        {
            case 0:
                doorUp.SetActive(true);
                break;
            case 1:
                doorDown.SetActive(true);
                break;
            case 2:
                doorLeft.SetActive(true);
                break;
            case 3:
                doorRight.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void DisableDoor(int doorIndex)
    {
        switch (doorIndex)
        {
            case 0:
                doorUp.SetActive(false);
                break;
            case 1:
                doorDown.SetActive(false);
                break;
            case 2:
                doorLeft.SetActive(false);
                break;
            case 3:
                doorRight.SetActive(false);
                break;
            default:
                break;
        }
    }

    //Randomly place arguments on the Map(Enemies, walls, items)
    void LayoutObjectAtRandom(GameObject tile,int minimum,int maximum)
    {
        int objectCount = Random.Range(minimum, maximum);
        for(int i = 0; i < objectCount; i++)
        {
           
            int randomX = Random.Range(-10, 10); //room size
            int randomY = Random.Range(-2, 2); //room size

            Vector2 newPosition = new Vector2(roomMap.position.x + randomX, roomMap.position.y + randomY);
            //Generate
            GameObject instance = Instantiate(tile, newPosition, Quaternion.identity);
            instance.transform.SetParent(roomMap);
        }
    }
}
