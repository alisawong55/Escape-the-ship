using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaver : Enemy
{
    [SerializeField] AudioClip attackSfx;

    //melee attack
    protected override IEnumerator Attack(){
        isAttacking = true;
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + transform.forward * attackRange*0.5f, attackRange*0.5f);
        foreach (Collider2D enemy in hitEnemies)
        {
            Player a = enemy.GetComponent<Player>();
            if (a != null){
                a.TakeDamage(10);
            }
        }

        animator.SetTrigger("attack");
        audioController.CreateSFX(attackSfx, transform.position);
        yield return new WaitForSeconds(1.5f); //cooldown + wait animation
        isAttacking = false;
    }
}
