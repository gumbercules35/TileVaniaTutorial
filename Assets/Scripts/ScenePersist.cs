using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
     private void Awake() {
        //Singleton Logic
        //Find out how many gameSessions we currently have
        int scenePersistCount = FindObjectsOfType<ScenePersist>().Length;
        //If another instance of GameSession exists, this created instance shouldnt as it is not
        //the ORIGINAL instance so destroy
        if (scenePersistCount > 1){
            Destroy(gameObject);
        }else {
            //Else set the created object to be Persistent through loads
            DontDestroyOnLoad(gameObject);
           
        }
    }
}
