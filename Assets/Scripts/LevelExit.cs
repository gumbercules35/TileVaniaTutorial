using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private AudioClip exitSound;
   private float loadDelay = 4f;
   private void OnTriggerEnter2D(Collider2D other) {
    if(other.tag == "Player"){
        other.gameObject.GetComponent<PlayerMovement>().ToggleIsExiting();
        StartCoroutine(LoadNextLevel());       
    }
   }

   private IEnumerator LoadNextLevel(){
        AudioSource.PlayClipAtPoint(exitSound, transform.localPosition);
        yield return new WaitForSecondsRealtime(loadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
   }
}
