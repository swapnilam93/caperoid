using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditDisplayScript : MonoBehaviour {

	public Button ForestButton;
	public Button SpaceButton;
	public Button ExitButton;
	public bool state = false;

	public void CreditDisplay(){
		ForestButton.enabled = false;
		SpaceButton.GetComponent<Button>().interactable = false;
		ExitButton.GetComponent<Button>().interactable = false;
	}

}
