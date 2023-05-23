using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private Rigidbody2D enemyRigidBody;
    private BoxCollider2D edgeDetector;

    private void Awake() {
        enemyRigidBody = gameObject.GetComponent<Rigidbody2D>();
        edgeDetector = gameObject.GetComponent<BoxCollider2D>();
    }
    
   
    void Update()
    {
        enemyRigidBody.velocity = new Vector2(moveSpeed,0f);
    }

    private void OnTriggerExit2D(Collider2D other) {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    private void FlipEnemyFacing(){
        gameObject.transform.localScale = new Vector2 (-(Mathf.Sign(enemyRigidBody.velocity.x)), 1f);
    }
}
