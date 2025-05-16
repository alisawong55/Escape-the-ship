using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterPickup : Pickup
{
    Blaster item;
    void Awake(){
        audioController = FindObjectOfType<AudioController>();
        inventory = FindObjectOfType<Inventory>();
        item = FindObjectOfType<Blaster>();

    }

    protected override void OnPicked(){
        if (inventory.CanPickup()){
            inventory.AddItem(item);
            PlaySFX();
            Debug.Log("pick blaster");
            Destroy(gameObject);
        }
    }
}
