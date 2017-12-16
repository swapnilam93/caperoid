using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	private GameController gameController;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter	(Collider other){
		if (other.tag == "Ground") {
			return;
		}

		if (other.tag == "U_CharacterBack") {
			//Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
			gameController.GameOver();
			//Debug.Log("smash");
		}

		Instantiate (explosion, transform.position, transform.rotation);

		if (other.tag == "End") {
			Destroy (gameObject);
			gameController.AddScore(10);
		}

		//Destroy (gameObject);

	}

}
