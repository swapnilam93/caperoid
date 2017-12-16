using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
	public GameObject bullet;
	public AudioClip sound;
	private GameController gameController;
	private PauseMenuScript pause;
		
	float timeElapsed = 0;
	float spawnCycle = 3f;
	bool spawnPowerup = true;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void Update () {
		timeElapsed += Time.deltaTime;
		if (gameController.gameOver == false){
			//yield return new WaitForSeconds(3.0);
			if (timeElapsed > spawnCycle) {
				GameObject temp;
				if (spawnPowerup) {
					temp = (GameObject)Instantiate (bullet);
					Vector3 pos = temp.transform.position;
						temp.transform.position = new Vector3 (Random.Range (-2, 3), pos.y, pos.z);
					GetComponent<AudioSource>().PlayOneShot (sound);
				}
			
				timeElapsed -= spawnCycle;
			}
		}
	}
}