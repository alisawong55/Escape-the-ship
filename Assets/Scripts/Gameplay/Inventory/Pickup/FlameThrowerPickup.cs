using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerPickup : Pickup
{
    FlameThrower item;
    void Awake(){
        audioController = FindObjectOfType<AudioController>();
        inventory = FindObjectOfType<Inventory>();
        item = FindObjectOfType<FlameThrower>();

    }
    
    protected override void OnPicked(){
        if (inventory.CanPickup()){
            inventory.AddItem(item);
            PlaySFX();
            Debug.Log("pick flame thrower");
            Destroy(gameObject);
        }
    }
}
