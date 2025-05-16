using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheStick : Item
{
    float range = 3;
    
    void Awake(){
        this.itemType = "The stick";
        audioController = FindObjectOfType<AudioController>();
    }
    //melee attack 10DMG unlimited use no energy
    public override void UseItem(Vector2 target){
        PlaySFX();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy a = enemy.GetComponent<Enemy>();
            if (a != null){
                a.TakeDamage(10);
            }
           
        }
        
    }
}
