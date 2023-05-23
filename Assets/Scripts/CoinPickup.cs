using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip collectSound;

    private void Awake() {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            other.gameObject.GetComponent<PlayerMovement>().sessionManager.IncrementAmmo();
            AudioSource.PlayClipAtPoint(collectSound, transform.localPosition);
            Destroy(gameObject);
        }
    }
}
