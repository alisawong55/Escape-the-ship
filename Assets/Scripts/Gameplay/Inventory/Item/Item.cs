using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] AudioClip itemSfx;
    protected AudioController audioController;
    public string itemType;
    float maxEnergy = 100;
    public float energy;

    void Awake(){
        energy = maxEnergy;
    }

    public virtual void UseItem(Vector2 target){

    }

    public void PlaySFX(){
        if (itemSfx){
            audioController.CreateSFX(itemSfx, transform.position);
        }
    }
}
