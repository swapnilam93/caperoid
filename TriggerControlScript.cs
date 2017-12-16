using UnityEngine;
using System.Collections;

public class TriggerControlScript : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		Destroy(other.gameObject);
	}
}
