using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCoin : MonoBehaviour
{
    
    private CircleCollider2D bulletCollider;
    private Rigidbody2D bulletBody;
    private PlayerMovement player;
    
    private float timeUntilDestroy = 25f;
    private bool destroyTrigger = false;
    private void Awake() {
        bulletCollider = gameObject.GetComponent<CircleCollider2D>(); 
        bulletBody = gameObject.GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        Debug.Log(player);
        
    }
    void Start()
    {  float bulletSpeed = 10f * player.transform.localScale.x;
       bulletBody.velocity = new Vector2( bulletSpeed, 2.5f);
    }

   
    void Update()
    {
        Countdown();
        if (destroyTrigger){
            Destroy(gameObject);
        }

    }

    private void Countdown(){
        timeUntilDestroy -= 0.01f;

        if (timeUntilDestroy <= 0){
            destroyTrigger = true;
        }
        
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy"){
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    
}
