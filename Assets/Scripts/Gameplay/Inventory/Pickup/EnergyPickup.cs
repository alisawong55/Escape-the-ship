using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : Pickup
{
    Item item;

    void Awake(){
        audioController = FindObjectOfType<AudioController>();
        inventory = FindObjectOfType<Inventory>();
    }

    protected override void OnPicked(){
        item = inventory.items[inventory.activeItem];
        if(item.itemType != "The stick" && item.energy < 50){
            item.energy += 20;
            if(item.energy > 50){
                item.energy = 50;
            }
            PlaySFX();
            Debug.Log("pick energy for " + item.itemType);
            Destroy(gameObject);
        }
        
    }
}
