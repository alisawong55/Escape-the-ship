using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRiflePickup : Pickup
{
    LaserRifle item;
    void Awake(){
        audioController = FindObjectOfType<AudioController>();
        inventory = FindObjectOfType<Inventory>();
        item = FindObjectOfType<LaserRifle>();

    }

    protected override void OnPicked(){
        if (inventory.CanPickup()){
            inventory.AddItem(item);
            PlaySFX();
            Debug.Log("pick laser rifle");
            Destroy(gameObject);
        }
    }
}
