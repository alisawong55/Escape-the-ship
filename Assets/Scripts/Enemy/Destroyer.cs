using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : Enemy
{
    [SerializeField] AudioClip attackSfx;
    [SerializeField] GameObject projectilePrefab;
    float projectileSpeed = 5f;
    
    //range attack
    protected override IEnumerator Attack(){
        isAttacking = true;
        
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 shootDirection = (player.position - transform.position).normalized;
        projectile.transform.rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
        projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * projectileSpeed;
        
        
        animator.SetTrigger("attack");
        audioController.CreateSFX(attackSfx, transform.position);
        yield return new WaitForSeconds(1.5f); //cooldown + wait animation
        isAttacking = false;
    }
}
