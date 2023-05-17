using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    void Start()
    {
        
    }

 
    void Update()
    {
        
    }

    private void OnMove(InputValue value){
         moveInput = value.Get<Vector2>();
         Debug.Log(moveInput);
    }
}
