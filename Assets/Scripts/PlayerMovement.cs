using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerBody;   
    private SpriteRenderer playerSprite;

    private Animator playerAnimator;
    
    private Vector2 moveInput;

    [Header("Velocity Values")]
    [SerializeField]private float moveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 2f;

    private bool isMovingX = false;
    private bool isMovingY = false;

    private void Awake() {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerAnimator = gameObject.GetComponent<Animator>();
       
    }
    void Start()
    {
        
    }

 
    void Update()
    {
        // Check if the player has Any x velocity (+ve or -ve ) greater than effectively 0
        isMovingX = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        isMovingY = Mathf.Abs(playerBody.velocity.y) > Mathf.Epsilon;
        Run();
        FlipSprite();
    }

    private void OnMove(InputValue value){
         moveInput = value.Get<Vector2>();
         
    }

    private void OnJump(InputValue value){
        
        if (value.isPressed && playerBody.IsTouchingLayers(LayerMask.GetMask("Platforms"))){
            playerBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void Run(){
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerBody.velocity.y);
        playerBody.velocity = playerVelocity;

        playerAnimator.SetBool("isRunning", isMovingX);
    }

    private void FlipSprite(){
       
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
