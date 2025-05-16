using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : Item
{
    float range = 3;
    
    void Awake(){
        this.itemType = "Light saber";
        audioController = FindObjectOfType<AudioController>();
    }

    //melee splash 30DMG 2this.energy/hit
    public override void UseItem(Vector2 target){
        //chech this.energy
        if(this.energy >= 4){
            this.energy-=4;
        PlaySFX();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy a = enemy.GetComponent<Enemy>();
            if (a != null){
                a.TakeDamage(30);
            }
        }
        
           
        }else{
            Debug.Log("not enough this.energy");
        }
        
    }
}
