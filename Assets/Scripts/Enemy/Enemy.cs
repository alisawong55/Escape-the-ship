using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected float speed = 2f;
    [SerializeField] protected float attackRange = 3f;
    [SerializeField] protected float sightDistance = 5f;
    [SerializeField] protected float retreatDistance = 2f; //must < attack range
    [SerializeField] protected float randomMovementRange = 3f;
    [SerializeField] AudioClip enemySfx;


    protected bool isAttacking = false;
    protected Transform player;
    protected bool playerInSight = true;
    protected Vector2 randomMovementTarget;
    protected Vector2 lastPosition;
    float scaleX;
    float scaleY;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        audioController = FindObjectOfType<AudioController>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }

    void Update()
    {
        // Check if the player is in sight
        if (Vector2.Distance(transform.position, player.position) < sightDistance){
            playerInSight = true;
        }
        else{
            playerInSight = false;
        }

        if (!isAttacking){
            // Move towards the player if in sight
            if(playerInSight){

            if (Vector2.Distance(transform.position, player.position) > attackRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < attackRange && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                StartCoroutine(Attack());
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }


            }else{
            // Move randomly if player is not in sight
            if (Vector2.Distance(transform.position, randomMovementTarget) < 0.1f)
            {
                randomMovementTarget = (Vector2)transform.position + Random.insideUnitCircle * randomMovementRange;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, randomMovementTarget, speed * Time.deltaTime);
            }
            }

            // Rotation
            Vector2 moveDirection = ((Vector2)transform.position - lastPosition) / Time.deltaTime;
            if (moveDirection.x > 0){
                transform.localScale = new Vector2(-scaleX, scaleY);
                animator.SetBool("move", true);
            }
            else if (moveDirection.x < 0){
                transform.localScale = new Vector2(scaleX, scaleY);
                animator.SetBool("move", true);
            }else{
                animator.SetBool("move", false);
            }
        lastPosition = transform.position;

        }
    }

    protected virtual IEnumerator Attack()
    {
        isAttacking = true;
        //do something
        yield return new WaitForSeconds(1f); //cooldown + wait animation
        animator.SetTrigger("attack");
        isAttacking = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightDistance);
    }
}
