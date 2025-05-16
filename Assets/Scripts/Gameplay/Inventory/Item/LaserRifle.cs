using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRifle : Item
{
    [SerializeField] GameObject projectilePrefab;
    float projectileSpeed = 10f;
    
    void Awake(){
        this.itemType = "Laser rifle";
        audioController = FindObjectOfType<AudioController>();
    }
    
    //range attack 2DMG/0.1second 4this.energy/second
    public override void UseItem(Vector2 target){
        if(this.energy >= 8){
            this.energy-=8;
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
