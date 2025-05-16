using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAlien : Enemy
{
    [SerializeField] AudioClip attackSfx;
    [SerializeField] AudioClip rangeAttackSfx;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] GameObject spawnVfx;
    [SerializeField] GameObject spawnEnemyLv1;
    [SerializeField] GameObject spawnEnemyLv2;
    float projectileSpeed = 10f;
    bool isSpawnCooldown = false;


    //melee attack
    protected override IEnumerator Attack(){
        if(!isSpawnCooldown){
            StartCoroutine(SpawnEnemy(spawnEnemyLv1, 1, 4));
            StartCoroutine(SpawnEnemy(spawnEnemyLv2, 1, 2));
            yield return new WaitForSeconds(3f);
        }
        if (Vector2.Distance(transform.position, player.position) > 5)
        {
            StartCoroutine(RangeAttack());
            yield return null;
        }
        else
        {
            isAttacking = true;
        
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + transform.forward * 2.5f, 5);
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

    //spawn alien
    IEnumerator SpawnEnemy(GameObject enemyPrefab,int minimum,int maximum){
        //if(!isSpawnCooldown){
            isSpawnCooldown = true;
            animator.SetTrigger("spawn");
            yield return new WaitForSeconds(2f);
            

            int objectCount = Random.Range(minimum, maximum);
            for(int i = 0; i < objectCount; i++)
            {
                Vector2 randomLocation = (Vector2)transform.position + Random.insideUnitCircle * 5;
                GameObject spawnEffect = Instantiate(spawnVfx, randomLocation, Quaternion.identity);
                spawnEffect.transform.position = Vector2.MoveTowards(transform.position, randomLocation, 5 * Time.deltaTime);
                Vector2 shootDirection = (randomLocation - (Vector2)transform.position).normalized;
                spawnEffect.transform.rotation = Quaternion.LookRotation(Vector3.forward, shootDirection);
                spawnEffect.AddComponent<TimedSelfDestruct>();
                GameObject instance = Instantiate(enemyPrefab, randomLocation, Quaternion.identity);
                //StartCoroutine(PopAnime(instance));
            }
            
            yield return new WaitForSeconds(30f);
            isSpawnCooldown = false;
        //}
        
    }

    private IEnumerator PopAnime(GameObject pop)
    {
        float popDuration = 1f;
        float timer = 0f;

        while (timer < popDuration)
        {
            float scale = Mathf.Lerp(0f, 1f, timer / popDuration);
            pop.transform.localScale = new Vector3(scale, scale, 1f);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the final scale is set to 1
        pop.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(1f);
    }
}
