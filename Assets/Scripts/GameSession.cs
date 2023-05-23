using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{   
    [SerializeField]public int playerLives = 3 ;
    private int ammoRemaining;
    private int totalScore;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ammoText;

    private float deathDelay = 2.5f;

    //DEBUG VARIABLES
    
    //DEBUG VARIABLES
    private void Awake() {        
        //Singleton Logic
        //Find out how many gameSessions we currently have
        int gameSessionCount = FindObjectsOfType<GameSession>().Length;
        //If another instance of GameSession exists, this created instance shouldnt as it is not
        //the ORIGINAL instance so destroy
        if (gameSessionCount > 1){            
            Destroy(gameObject);
        }else {
            //Else set the created object to be Persistent through loads            
            DontDestroyOnLoad(gameObject);
            
            
        }
    }

    private void Start() {
        
        livesText.text = "Lives:" + playerLives.ToString();
        scoreText.text = "Score:" + totalScore.ToString();
        ammoText.text = "Ammo:" + ammoRemaining.ToString();
        
    }
   public IEnumerator ProcessPlayerDeath(){
        yield return new WaitForSecondsRealtime(deathDelay);
        if (playerLives > 1) {
            TakeLifeAndReload();
        } else 
        {
            ResetGameSession();
        }
   }

   private void TakeLifeAndReload(){      
    playerLives--;
    int currentSceneIndex =SceneManager.GetActiveScene().buildIndex;    
    livesText.text = "Lives:" + playerLives.ToString();   
    SceneManager.LoadScene(currentSceneIndex);
   }

   private void ResetGameSession(){
    //Kick off the load to the first scene in the build index
    SceneManager.LoadScene(0);
    //Destroy this instance of GameSession as it is "complete" when the player runs out of lives
    //This method will only be called when wanting to COMPLETELY start over
    Destroy(gameObject);
   }

   public void IncrementAmmoAndScore (int ammoValue, int scoreValue) {
    ammoRemaining += ammoValue;   
    ammoText.text = "Ammo:" + ammoRemaining.ToString();
    totalScore += scoreValue;
    scoreText.text = "Score:" + totalScore.ToString(); 

    
   }

   public void DecrementAmmo (){
    ammoRemaining --;   
    ammoText.text = "Ammo:" + ammoRemaining.ToString(); 
   }

   public bool HasAmmo (){
        return ammoRemaining >= 1;
   }
}
