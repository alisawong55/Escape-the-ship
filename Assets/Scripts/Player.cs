using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] RectTransform healthBar;
    [SerializeField] RectTransform energyBar;
    [SerializeField] AlertMessage alertMessage;
    float speed = 10f;

    public bool isAttacking = false;
    int activeItem;

    Inventory inventory;
    MapManager mapManager;
    GameManager gameManager;
    UIManager uiManager;
    float scaleX;
    float scaleY;
    

    void Start(){
        inventory = FindObjectOfType<Inventory>();
        audioController = FindObjectOfType<AudioController>();
        mapManager = FindObjectOfType<MapManager>();
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }

    //control player
    void Update()
    {
        //wasd move
        if(!isAttacking)
        {
            
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;
            transform.Translate(moveDirection * speed * Time.deltaTime);

            // Rotation
            if(moveDirection.x > 0){
                transform.localScale = new Vector2(-scaleX, scaleY);
                animator.SetBool("move", true);
            }
            else if(moveDirection.x < 0){
                transform.localScale = new Vector2(scaleX, scaleY);
                animator.SetBool("move", true);
            }
            else if(moveDirection.y != 0){
                animator.SetBool("move", true);
            }
            else{
                animator.SetBool("move", false);
            }


            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Attack());
            }
        }

        //chance item
        if(Input.GetMouseButtonDown(1)){
            inventory.ChangeItem();
            activeItem = inventory.activeItem;
            energyBar.localScale = new Vector2(inventory.items[activeItem].energy/100, 1);
        }
        //open map
        if(Input.GetKeyDown("space")){
            mapManager.ShowMapUI();
        }
        //quit game
        if(Input.GetKeyDown("escape")){
           uiManager.ShowSettingUI();
        }
    }

    public void TakeDamage(float damage){
        hp -= damage;
        //hp=0?
        if (hp <= 0){
            hp = 0;
            StartCoroutine(Die());
        }
        else
        {
            animator.SetTrigger("hit");
        }
        
        healthBar.localScale = new Vector2(hp/maxhp, 1);
    }
    private IEnumerator Die(){
        hp = 0;
        animator.SetTrigger("die");
        audioController.CreateSFX(dieSfx, transform.position);
        yield return new WaitForSeconds(1f);
        gameManager.GameOver();
        Debug.Log("you die");
    }
    public void Heal(float power){
        hp += power;
        audioController.CreateSFX(healSfx, transform.position);
        if (hp > maxhp){
            hp = maxhp;
        }
        healthBar.localScale = new Vector2(hp/maxhp, 1);
    }

    IEnumerator Attack()
    {
        //get position
        isAttacking = true;
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //get item
        activeItem = inventory.activeItem;
        if(inventory.items[activeItem] != null){
            if(inventory.items[activeItem].itemType == "The stick" || inventory.items[activeItem].itemType == "Light saber"){
                //animator.SetTrigger("attack");
                if(inventory.items[activeItem].energy > 4){
                    Animator itemAnime = inventory.items[activeItem].gameObject.GetComponent<Animator>();
                    inventory.items[activeItem].UseItem(target);
                    itemAnime.SetTrigger("use item");
                }else{
                    StartCoroutine(alertMessage.showAlert("Not enough energy"));
                }
                
            }else{
                if(inventory.items[activeItem].energy > 4){
                    inventory.items[activeItem].UseItem(target);
                }else{
                    StartCoroutine(alertMessage.showAlert("Not enough energy"));
                }
                
            }
            energyBar.localScale = new Vector2(inventory.items[activeItem].energy/100, 1);
        }
        
        
        yield return new WaitForSeconds(0.5f); //cooldown + wait animation
        isAttacking = false;
    }

}
