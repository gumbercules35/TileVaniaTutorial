using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip collectSound;
    private int coinValue;
    private int ammoValue;

    private bool hasTriggered = false;

    private void Start() {
        coinValue = (Random.Range(1,10) * 10) ;
        ammoValue = coinValue / 10;
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !hasTriggered){
            hasTriggered = true;
            other.gameObject.GetComponent<PlayerMovement>().sessionManager.IncrementAmmoAndScore(ammoValue, coinValue);
            AudioSource.PlayClipAtPoint(collectSound, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
