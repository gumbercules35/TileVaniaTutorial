using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerBody;   
    private CapsuleCollider2D playerBodyCollider;
    private BoxCollider2D playerFeetCollider;    private SpriteRenderer playerSprite;
    private Animator playerAnimator;    
    private Vector2 moveInput;
    private float climbAnimationSpeed = 1f;

    [Header("Velocity Values")]
    [SerializeField]private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 2f;
    
    private void Awake() {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerAnimator = gameObject.GetComponent<Animator>();
        playerBodyCollider = gameObject.GetComponent<CapsuleCollider2D>();
        playerFeetCollider = gameObject.GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        
    }

 
    void Update()
    {
        // Check if the player has Any x velocity (+ve or -ve ) greater than effectively 0              
        Run();
        FlipSprite();
        Climb();
    }

    private void OnMove(InputValue value){
         moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value){        
        if (value.isPressed && playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Platforms"))){
           
            playerBody.velocity += new Vector2(0f, jumpSpeed);
           
        }
        //
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


}
