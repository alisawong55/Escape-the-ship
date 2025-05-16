using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : Item
{
    [SerializeField] GameObject projectilePrefab;
    float projectileSpeed = 5f;
    
    void Awake(){
        this.itemType = "Flame thrower";
        audioController = FindObjectOfType<AudioController>();
    }

    //clase range AoE 20DMG 2this.energy/shoot
    public override void UseItem(Vector2 target){
        if(this.energy >= 2){
            this.energy-=2;
            PlaySFX();
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector2 shootDirection = (target - (Vector2)transform.position).normalized;
            projectile.transform.rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
            projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * projectileSpeed;


        }else{
            Debug.Log("not enough this.energy");
        }
    }
}
