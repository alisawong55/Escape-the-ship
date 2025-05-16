using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    MapManager mapManager;

    void Start(){
        mapManager = FindObjectOfType<MapManager>();
    }

    void OnCollisionEnter2D(Collision2D collision){
        mapManager.PlayerEnterDoor(this.gameObject);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player pickingPlayer = collision.GetComponent<Player>();
        if (pickingPlayer != null){
            mapManager.PlayerEnterDoor(this.gameObject);
        }
    }
}
