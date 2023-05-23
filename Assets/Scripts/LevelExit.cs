using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
   private float loadDelay = 1.5f;
   private void OnTriggerEnter2D(Collider2D other) {
    if(other.tag == "Player"){
        StartCoroutine(LoadNextLevel());       
    }
   }

   private IEnumerator LoadNextLevel(){
        Debug.Log("Loading");
        yield return new WaitForSecondsRealtime(loadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
   }
}
