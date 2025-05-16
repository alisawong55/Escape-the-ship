using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behemoth : Enemy
{
    [SerializeField] AudioClip attackSfx;
    [SerializeField] AudioClip rangeAttackSfx;
    [SerializeField] GameObject projectilePrefab;
    float projectileSpeed = 5f;

    //melee attack
    protected override IEnumerator Attack(){
        if (Vector2.Distance(transform.position, player.position) > 4)
        {
            StartCoroutine(RangeAttack());
            yield return null;
        }
        else
        {
            isAttacking = true;
        
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + transform.forward * 2, 4);
            foreach (Collider2D enemy in hitEnemies)
            {
                Player a = enemy.GetComponent<Player>();
                if (a != null){
                    a.TakeDamage(20);
                }
            }

            animator.SetTrigger("attack");
            audioController.CreateSFX(attackSfx, transform.position);
            yield return new WaitForSeconds(1.5f); //cooldown + wait animation
            isAttacking = false;
        }
        
    }

    //range attack
    IEnumerator RangeAttack(){
        isAttacking = true;
        
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 shootDirection = (player.position - transform.position).normalized;
        projectile.transform.rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
        projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * projectileSpeed;
        
        animator.SetTrigger("special");
        audioController.CreateSFX(rangeAttackSfx, transform.position);
        yield return new WaitForSeconds(1.5f); //cooldown + wait animation
        isAttacking = false;
    }

}
