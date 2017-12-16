using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
		
	public GameObject hazard;
	public Vector3 spawnValues;
	private int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public int waveNo;

	public GUIText scoreText;
	public Text scoreT;
	private int score;
	private int i;

	public Text restartText;
	public Text gameOverText;
	public bool restart;
	public bool gameOver;
	private KinectGestures.Gestures gesture;

	public GUISkin skin;
	private KinectManager kin;

	void Start(){
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine(SpawnWaves ());
		hazardCount = 2;
		waveNo = 0;
		//kin.ControlMouseCursor = false;
	}

	void Update(){
		if (restart) {
			if(Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel(Application.loadedLevel);
			}
		}

		/*if (gesture == KinectGestures.Gestures.Jump) {
				Debug.Log ("det");
			if (restart) {
				Application.LoadLevel (Application.loadedLevel);
				Debug.Log ("res");
			}
		}*/
	}

	IEnumerator SpawnWaves(){
		yield return new WaitForSeconds(startWait);
		while(true){
			for (i=0; i<hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
			waveNo += 1;
			if (waveNo == 4)
				hazardCount += 1;
			if (waveNo == 8)
				hazardCount += 1;
			if(gameOver){
				restartText.text = "'Jump' or Press 'R' to Restart!";
				restart = true;
				break;
			}	
		}
	}

	public void AddScore(int newScoreValue){
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore(){
		scoreText.text = "Score: " + score;
		scoreT.text = "Score: " + score;
		Debug.Log (scoreText.text);
	}

	public void GameOver(){
		gameOverText.text = "Game Over";
		gameOver = true;
	}

	void OnGUI()
	{

		GUI.skin=skin; //use the skin in game over menu
		//check if game is not over, if so, display the score and the time left
		if(!gameOver)    
		{
			//GUI.Label(new Rect(10, 10, Screen.width/5, Screen.height/6),"TIME LEFT: "+((int)timeRemaining).ToString());
			//GUI.Label(new Rect(Screen.width-(Screen.width/6), 10, Screen.width/6, Screen.height/6), "SCORE: "+((int)score).ToString());
		}
		//if game over, display game over menu with score
		else
		{
			//Time.timeScale = 0; //set the timescale to zero so as to stop the game world
			//kin.ControlMouseCursor = true;
			//display the final score
			GUI.Box(new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2), "GAME OVER\nYOUR SCORE: "+(int)score);
			
			//restart the game on click
			if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+Screen.height/10+10, Screen.width/2-20, Screen.height/10), "RESTART: 'Jump'")){
				Application.LoadLevel(Application.loadedLevel);
			}
			
			//load the main menu, which as of now has not been created
			if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+2*Screen.height/10+10, Screen.width/2-20, Screen.height/10), "MAIN MENU: 'Swipe Left'")){
				Application.LoadLevel(1);
			}
			
			//exit the game
			if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+3*Screen.height/10+10, Screen.width/2-20, Screen.height/10), "EXIT GAME: 'Swipe Right'")){
				Application.Quit();
			}
		}
	}
}
