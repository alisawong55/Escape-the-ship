using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Animator animator;
    protected AudioController audioController;
    protected SpriteRenderer spriteRenderer;
    SpawnRandomPickup spawner;
    public float maxhp;
    public float hp;
    public int tier = 1;
    [SerializeField] protected AudioClip hitSfx;
    [SerializeField] protected AudioClip dieSfx;
    [SerializeField] protected AudioClip healSfx;
    
    void Awake()
    {
        hp = maxhp;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioController = FindObjectOfType<AudioController>();
        
    }
    public void TakeDamage(float damage){
        hp -= damage;
        //hp=0?
        if (hp <= 0){
           Die();
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }
    private void Die(){
        hp = 0;
        animator.SetTrigger("die");
        audioController.CreateSFX(dieSfx, transform.position);
        //wait for animation then remove
        /*AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float duration = stateInfo.length;
        TimedSelfDestruct timedSelfDestruct = this.gameObject.AddComponent<TimedSelfDestruct>();
        timedSelfDestruct.LifeTime = duration;*/
        //int randomInt = Random.Range(0, tier);
        spawner = FindObjectOfType<SpawnRandomPickup>();
        spawner.SpawnPickup(tier, transform.position);
        gameObject.AddComponent<TimedSelfDestruct>();
        
    }
    public void Heal(float power){
        hp += power;
        audioController.CreateSFX(healSfx, transform.position);
        if (hp > maxhp){
            hp = maxhp;
        }
    }
}
