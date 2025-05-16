using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Image slot1;
    [SerializeField] Image slot2;
    [SerializeField] Image slot3;
    [SerializeField] Image slot4;
    [SerializeField] Image slot5;
    [SerializeField] Image slot6;

    [SerializeField] Image item1;
    [SerializeField] Image item2;
    [SerializeField] Image item3;
    [SerializeField] Image item4;
    [SerializeField] Image item5;
    [SerializeField] Image item6;

    [SerializeField] Sprite uiItem;
    [SerializeField] Sprite uiItemActive;

    [SerializeField] Sprite blaster;
    [SerializeField] Sprite flameThrower;
    [SerializeField] Sprite laserRifle;
    [SerializeField] Sprite lightSaber;
    [SerializeField] Sprite theStick;
    [SerializeField] Sprite empty;

    Sprite UIImage;
    Inventory inventory;

    void Start(){
        inventory = FindObjectOfType<Inventory>();
    }

    public void SetActiveItemUI(int index, bool active){
        if(active){
            UIImage = uiItemActive;
        }else{
            UIImage = uiItem;
        }

        switch(index){
            case 0:
                slot1.sprite = UIImage;
                break;
            case 1:
                slot2.sprite = UIImage;
                break;
            case 2:
                slot3.sprite = UIImage;
                break;
            case 3:
                slot4.sprite = UIImage;
                break;
            case 4:
                slot5.sprite = UIImage;
                break;
            case 5:
                slot6.sprite = UIImage;
                break;
            default:
                break;

        }

    }

    public void UpdateItemUI(int index, bool isEmpty){
        //change item icon
        if(!isEmpty){
            string itemType = inventory.items[index].itemType;
            switch (itemType)
            {
            case "Blaster":
                UIImage = blaster;
                break;
            case "Flame thrower":
                UIImage = flameThrower;
                break;
            case "Laser rifle":
                UIImage = laserRifle;
                break;
            case "Light saber":
                UIImage = lightSaber;
                break;
            default:
                break;
            }
        }else{
            UIImage = empty;
        }

        switch(index){
            case 0:
                item1.sprite = UIImage;
                break;
            case 1:
                item2.sprite = UIImage;
                break;
            case 2:
                item3.sprite = UIImage;
                break;
            case 3:
                item4.sprite = UIImage;
                break;
            case 4:
                item5.sprite = UIImage;
                break;
            case 5:
                item6.sprite = UIImage;
                break;
            default:
                break;

        }             

    }
}
