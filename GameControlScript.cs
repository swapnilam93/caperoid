using UnityEngine;
using System.Collections;

public class GameControlScript : MonoBehaviour {
	
	float timeRemaining = 10;
	float timeExtension = 4f;
	float timeDeduction = 3f;
	float totalTimeElapsed = 0;
	float score=0f;
	public bool isGameOver = false;
	//public string optionsLevel;
	public GUISkin skin;
	//private AvatarController avatar;
	
	void Start(){
		Time.timeScale = 1;  // set the time scale to 1, to start the game world. This is needed if you restart the game from the game over menu
		//avatar.ResetToInitialPosition ();
	}
	
	void Update () { 
		if(isGameOver)
			return;
		
		totalTimeElapsed += Time.deltaTime;
		score = totalTimeElapsed*100;
		timeRemaining -= Time.deltaTime;
		if(timeRemaining <= 0){
			isGameOver = true;
		}
	}
	
	public void PowerupCollected()
	{
		timeRemaining += timeExtension;
	}
	
	public void AlcoholCollected()
	{
		timeRemaining -= timeDeduction;
	}
	
	void OnGUI()
	{
		GUI.skin = skin;
		//check if game is not over, if so, display the score and the time left
		if(!isGameOver)    
		{
			GUI.Label(new Rect(10, 10, Screen.width/5, Screen.height/6),"TIME LEFT: "+((int)timeRemaining).ToString());
			GUI.Label(new Rect(Screen.width-(Screen.width/6), 10, Screen.width/6 + 100, Screen.height/6), "SCORE: "+((int)score).ToString());
		}
		//if game over, display game over menu with score
		else
		{
			Time.timeScale = 0; //set the timescale to zero so as to stop the game world
			//display the final score
			GUI.Box(new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2), "GAME OVER\nYOUR SCORE: "+(int)score);
			
			//restart the game on click
			if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+Screen.height/10+10, Screen.width/2-20, Screen.height/10), "RESTART : SWIPE RIGHT")){
				Application.LoadLevel(Application.loadedLevel);
			}
			
			//load the main menu, which as of now has not been created
			if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+2*Screen.height/10+10, Screen.width/2-20, Screen.height/10), "MAIN MENU : SWIPE LEFT")){
				Application.LoadLevel(1);
			}
			
			//exit the game
			if (GUI.Button(new Rect(Screen.width/4+10, Screen.height/4+3*Screen.height/10+10, Screen.width/2-20, Screen.height/10), "EXIT GAME : JUMP")){
				//Debug.Log("Quit");
				Application.Quit ();
			}
		}
	}
}