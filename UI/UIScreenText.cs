using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenText : MonoBehaviour {

	public GameObject gameOver;
	public GameObject youWin;

	void Start () {
		gameOver.SetActive (false);
		youWin.SetActive (false);
	}

	public void GameOver () {
		gameOver.SetActive (true);
	}

	public void YouWin () {
		youWin.SetActive (true);
	}

	/*void Update () {
		if (Input.GetMouseButtonDown (0) && (gameOver.activeSelf || youWin.activeSelf)) {
			//Debug.Log ("Return to title screen");
		}
	}*/
}
