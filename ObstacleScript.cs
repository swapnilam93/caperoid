using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {
	public float objectSpeed = -0.5f;
	
	void Update () {
		if (Time.timeScale == 1) {
			transform.Translate (0, objectSpeed, 0);
		}
	}
}