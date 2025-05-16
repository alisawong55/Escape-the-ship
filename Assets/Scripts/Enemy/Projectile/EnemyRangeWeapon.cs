using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeWeapon : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField] float damage;
    [SerializeField] AudioClip hitSfx;
    AudioController audioController;
    Vector2 lastPosition;

    void Awake(){
        audioController = FindObjectOfType<AudioController>();
        lastPosition = transform.position;
    }

    void Update(){
        //check range
        if (Vector2.Distance(transform.position, lastPosition) > range){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
            audioController.CreateSFX(hitSfx, transform.position);
            Destroy(gameObject); // Destroy the projectile on hit
        }
        
    }
}
