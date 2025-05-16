using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    private int inventorySize = 6;
    public int activeItem = 0;
    private GameObject heldItem;
    InventoryUI inventoryUI;

    
    

    // Start is called before the first frame update
    void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
        ChangeItem();
        ChangeItem(); //set first item
    }
    

    public void ChangeItem(){
        //hide old item and show next item
        if(items[activeItem] != null){
            heldItem = items[activeItem].gameObject;
            heldItem.SetActive(false);
        }
        inventoryUI.SetActiveItemUI(activeItem, false);
        activeItem = (activeItem + 1) % inventorySize;
        
        //
        if(items[activeItem] != null){
            heldItem = items[activeItem].gameObject;
            heldItem.transform.SetParent(this.transform);
            heldItem.transform.localPosition = Vector3.zero + Vector3.left * 1f;
            heldItem.transform.localScale = new Vector2(1, 1);
            heldItem.SetActive(true);
            
            
            //heldItem.GetComponent<Rigidbody2D>().isKinematic = true;
            //heldItem.GetComponent<Collider2D>().enabled = false;
        }else{
            activeItem = 0;
            heldItem = items[activeItem].gameObject;
            heldItem.transform.SetParent(this.transform);
            heldItem.transform.localPosition = Vector3.zero + Vector3.left * 1f;
            heldItem.transform.localScale = new Vector2(1, 1);
            heldItem.SetActive(true);

        }
        inventoryUI.SetActiveItemUI(activeItem, true);

        
    }

    public bool CanPickup(){
        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] == null)
            {
                return true;
            }
        }
        Debug.Log("inventory full");
        return false;
    }

    public void RemoveItem(Item item){
        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] == item)
            {
                items[i] = null;
                inventoryUI.UpdateItemUI(i, true);
                break;
            }
        }
        
    }

    public void AddItem(Item item){
        for (int i = 0; i < inventorySize; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                inventoryUI.UpdateItemUI(i, false);
                break;
            }
        }
        
    }
}
