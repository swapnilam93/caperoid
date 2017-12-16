using UnityEngine;
using System.Collections;
using System;

public class MainSceneListeners : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;
	
	// private bool to track if progress message has been displayed
	private bool progressDisplayed;
	private GameControlScript gameController;
	private PauseMainScene pause;
	
	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameControlScript");
		Debug.Log (gameControllerObject);
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameControlScript>();
			Debug.Log("found game control");
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
		
		GameObject pauseObject = GameObject.FindWithTag ("PauseMainScene");
		if (pauseObject != null) {
			pause = pauseObject.GetComponent <PauseMainScene>();
		}
		if (pauseObject == null) {
			Debug.Log ("Cannot find 'pauseObject' script");
		}
	}
	
	public void UserDetected(uint userId, int userIndex)
	{
		// as an example - detect these user specific gestures
		KinectManager manager = KinectManager.Instance;
		
		manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
		manager.DetectGesture(userId, KinectGestures.Gestures.Squat);
		
		//		manager.DetectGesture(userId, KinectGestures.Gestures.Push);
		//		manager.DetectGesture(userId, KinectGestures.Gestures.Pull);
		
		//		manager.DetectGesture(userId, KinectWrapper.Gestures.SwipeUp);
		//		manager.DetectGesture(userId, KinectWrapper.Gestures.SwipeDown);
		
		if(GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = "Move: Right, Left, Forward or Backword, Raise Right Hand to Pause.";
		}
	}
	
	public void UserLost(uint userId, int userIndex)
	{
		if(GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = string.Empty;
		}
	}
	
	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		//GestureInfo.guiText.text = string.Format("{0} Progress: {1:F1}%", gesture, (progress * 100));
		if(gesture == KinectGestures.Gestures.Click && progress > 0.3f)
		{
			string sGestureText = string.Format ("{0} {1:F1}% complete", gesture, progress * 100);
			if(GestureInfo != null)
				GestureInfo.GetComponent<GUIText>().text = sGestureText;
			
			progressDisplayed = true;
		}		
		else if((gesture == KinectGestures.Gestures.ZoomOut || gesture == KinectGestures.Gestures.ZoomIn) && progress > 0.5f)
		{
			string sGestureText = string.Format ("{0} detected, zoom={1:F1}%", gesture, screenPos.z * 100);
			if(GestureInfo != null)
				GestureInfo.GetComponent<GUIText>().text = sGestureText;
			
			progressDisplayed = true;
		}
		else if(gesture == KinectGestures.Gestures.Wheel && progress > 0.5f)
		{
			string sGestureText = string.Format ("{0} detected, angle={1:F1} deg", gesture, screenPos.z);
			if(GestureInfo != null)
				GestureInfo.GetComponent<GUIText>().text = sGestureText;
			
			progressDisplayed = true;
		}
		
	}
	
	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		string sGestureText = gesture + " detected";
		
		if (gesture == KinectGestures.Gestures.SwipeRight) {
			Debug.Log ("det");
			if ((gameController.isGameOver)|| (pause.paused)) {
				Application.LoadLevel (Application.loadedLevel);
				Debug.Log ("res");
			}
		}
		
		if (gesture == KinectGestures.Gestures.SwipeLeft) {
			if((gameController.isGameOver)||(pause.paused)){
				Application.LoadLevel(1);
			}
		}
		
		if (gesture == KinectGestures.Gestures.Jump) {
			if(gameController.isGameOver){
				Application.Quit ();
			}
		}
		
		if (gesture == KinectGestures.Gestures.RaiseRightHand) //check if Escape key/Back key is pressed
		{
			if (pause.paused)
				pause.paused = false;  //unpause the game if already paused
			else
				pause.paused = true;  //pause the game if not paused
			Debug.Log(gameController);
			
			if(pause.paused)
				Time.timeScale = 0;  //set the timeScale to 0 so that all the procedings are halted
			else
				Time.timeScale = 1;  //set it back to 1 on unpausing the game
			
		}
		
		//if(gesture == KinectGestures.Gestures.Click)
		//	sGestureText += string.Format(" at ({0:F1}, {1:F1})", screenPos.x, screenPos.y);
		
		if(GestureInfo != null)
			GestureInfo.GetComponent<GUIText>().text = sGestureText;
		
		progressDisplayed = false;
		
		return true;
	}
	
	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
		if(progressDisplayed)
		{
			// clear the progress info
			if(GestureInfo != null)
				GestureInfo.GetComponent<GUIText>().text = String.Empty;
			
			progressDisplayed = false;
		}
		
		return true;
	}
	
}
