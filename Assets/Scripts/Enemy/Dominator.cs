using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dominator : Enemy
{
    [SerializeField] AudioClip attackSfx;
    [SerializeField] GameObject projectilePrefab;
    float projectileSpeed = 5f;
    float angleStep = 20f; // Angle difference between each projectile
    
    //range attack
    protected override IEnumerator Attack(){
        isAttacking = true;

        for (int i = 0; i < 3; i++)
        {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 shootDirection = Quaternion.Euler(0, 0, i * angleStep - angleStep) * (player.position - transform.position).normalized;
        projectile.transform.rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
        projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * projectileSpeed;
        }
        
        
        animator.SetTrigger("attack");
        audioController.CreateSFX(attackSfx, transform.position);
        yield return new WaitForSeconds(2.5f); //cooldown + wait animation
        isAttacking = false;
    }
}
