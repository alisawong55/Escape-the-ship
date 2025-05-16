using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    protected override void OnPicked(){
        Player player = FindObjectOfType<Player>();
        if(player.hp < player.maxhp){
            player.Heal(20);
            Debug.Log("pick health");
            Destroy(gameObject);
        }
        
    }
}
