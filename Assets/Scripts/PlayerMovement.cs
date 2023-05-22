using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerBody;   
    private CapsuleCollider2D playerBodyCollider;
    private BoxCollider2D playerFeetCollider;    
    private SpriteRenderer playerSprite;

    private Vector3 bulletSpawn;
    [SerializeField] private GameObject bulletCoin;
    
    private Animator playerAnimator;    
    private Vector2 moveInput;
    private float climbAnimationSpeed = 1f;

    [Header("Velocity Values")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 2f;
    
    private bool isAlive = true;
    private void Awake() {

        playerBody = gameObject.GetComponent<Rigidbody2D>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerSprite.color = Color.white;
        playerAnimator = gameObject.GetComponent<Animator>();
        playerBodyCollider = gameObject.GetComponent<CapsuleCollider2D>();
        playerFeetCollider = gameObject.GetComponentInChildren<BoxCollider2D>();
        bulletSpawn = gameObject.transform.GetChild(1).localPosition;
        Debug.Log(bulletSpawn);

        // Dealing with unknown Index of transform
        // Transform[] transformsArray = gameObject.GetComponentsInChildren<Transform>();
        // foreach (var transform in transformsArray)
        // {
        // Debug.Log(transform);
            
        // }

    }
    void Start()
    {
        
    }

 
    void Update()
    {
       if (!isAlive){ return;}

        Run();
        FlipSprite();
        Climb();
        Die();
             
    }

    private void OnMove(InputValue value){
        if(!isAlive){ return;}
         moveInput = value.Get<Vector2>();
        
    }

    private void OnJump(InputValue value){      
        if(!isAlive){ return; }
            if (value.isPressed && playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platforms"))){            
                playerBody.velocity += new Vector2(0f, jumpSpeed);            
            }       
    }

    private void Run(){
        bool isMovingX = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerBody.velocity.y);
        playerBody.velocity = playerVelocity;
        playerAnimator.SetBool("isRunning", isMovingX);
    }

    private void Climb(){
        bool isMovingY = Mathf.Abs(playerBody.velocity.y) > Mathf.Epsilon;
        if(playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            
            playerBody.velocity = new Vector2(playerBody.velocity.x, moveInput.y * moveSpeed);
            if (isMovingY){
                playerAnimator.SetFloat("climbSpeed", climbAnimationSpeed);
                playerAnimator.SetBool("isClimbing", true);
                playerBody.gravityScale = 2;
            } else {
                playerAnimator.SetFloat("climbSpeed", 0f);
                playerBody.gravityScale = 0;               
            }
           
        }else {
            playerAnimator.SetBool("isClimbing", false);
            playerBody.gravityScale = 2;            
        }
    }

    private void FlipSprite(){       
        bool isMovingX = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        // If the player has any X velocity, set the scale of X to 1 (positive X) or -1 (negative X)
        if (isMovingX) {
            gameObject.transform.localScale = new Vector2 (Mathf.Sign(playerBody.velocity.x), 1);
        }
        // if(playerBody.velocity.x < 0){
           
        //     playerSprite.flipX = true;
        // } else if (playerBody.velocity.x > 0){
        //     playerSprite.flipX = false;
        // }
        // This method of flipping the sprite can have weird effects when the object contains child sprites
    }

    private void Die() {

        if (playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Spikes")))
        {
        playerAnimator.SetTrigger("Death");
        playerBodyCollider.enabled = false;
        playerBody.velocity = new Vector2(0f, jumpSpeed / 1.5f);
        playerSprite.color = Color.red;          

        
        isAlive = false;
        }
    }

    private void OnFire(){
        if (isAlive){
        Vector3 spawnLocation = new Vector3(transform.localPosition.x + bulletSpawn.x, transform.localPosition.y + bulletSpawn.y, 0f);
        Instantiate(bulletCoin, spawnLocation, new Quaternion());        
        } else {
            return;
        }
    }


}
