using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaberPickup : Pickup
{
    LightSaber item;
    void Awake(){
        audioController = FindObjectOfType<AudioController>();
        inventory = FindObjectOfType<Inventory>();
        item = FindObjectOfType<LightSaber>();

    }

    protected override void OnPicked(){
        if (inventory.CanPickup()){
            inventory.AddItem(item);
            PlaySFX();
            Debug.Log("pick light saber");
            Destroy(gameObject);
        }
    }
}
