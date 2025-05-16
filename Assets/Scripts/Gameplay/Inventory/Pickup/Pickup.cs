using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] AudioClip PickupSfx;
    [SerializeField] GameObject PickupVfxPrefab;
    protected AudioController audioController;
    protected Inventory inventory;

    void Awake(){
        audioController = FindObjectOfType<AudioController>();
        inventory = FindObjectOfType<Inventory>();
    }

    void OnTriggerEnter2D(Collider2D other){
        Player pickingPlayer = other.GetComponent<Player>();
        if (pickingPlayer != null){
            OnPicked();
        }
    }

    protected virtual void OnPicked(){
        PlaySFX();
        Debug.Log("pick item");
    }

    public void PlaySFX(){
        if (PickupSfx){
            audioController.CreateSFX(PickupSfx, transform.position);
        }
    }
}
