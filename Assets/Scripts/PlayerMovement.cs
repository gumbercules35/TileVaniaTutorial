using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerBody;   
    private SpriteRenderer playerSprite;
    
    private Vector2 moveInput;
    private float moveSpeed = 5f;

    private void Awake() {
        playerBody = gameObject.GetComponent<Rigidbody2D>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        
       
    }
    void Start()
    {
        
    }

 
    void Update()
    {
        Run();
        FlipSprite();
    }

    private void OnMove(InputValue value){
         moveInput = value.Get<Vector2>();
         Debug.Log(moveInput);
    }

    private void Run(){
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, playerBody.velocity.y);
        playerBody.velocity = playerVelocity;
    }

    private void FlipSprite(){
        // Check if the player has Any x velocity (+ve or -ve ) greater than effectively 0
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
