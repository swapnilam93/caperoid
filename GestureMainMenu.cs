﻿using UnityEngine;
using System.Collections;
using System;

public class GestureMainMenu : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;
	
	// private bool to track if progress message has been displayed
	private bool progressDisplayed;
	
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
			GestureInfo.GetComponent<GUIText>().text = "Swipe Right, Swipe Left, Raise Right Hand or Jump.";
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
		
		if (gesture == KinectGestures.Gestures.Jump) {
			Debug.Log ("det");
			Application.Quit();
		}
		
		if (gesture == KinectGestures.Gestures.SwipeLeft) {
				Application.LoadLevel(3);
		}
		
		if (gesture == KinectGestures.Gestures.SwipeRight) {
			Application.LoadLevel(2);
		}
		
		if (gesture == KinectGestures.Gestures.RaiseRightHand) //check if Escape key/Back key is pressed
		{
			Application.LoadLevel(4);	
		}
		
		if(gesture == KinectGestures.Gestures.Click)
			sGestureText += string.Format(" at ({0:F1}, {1:F1})", screenPos.x, screenPos.y);
		
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
