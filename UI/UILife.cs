using UnityEngine;
using UnityEngine.UI;

public class UILife : MonoBehaviour {
	
	public GameObject[] lifes;
	public Sprite lifeOn;
	public Sprite lifeOff;

	void Start () {
		for (int i = 0; i < lifes.Length; i++) {
			lifes [i].GetComponent<Image> ().sprite = lifeOn;
		}
	}

	public void ChangeImages (int l) {
		for (int i = 0; i < lifes.Length; i++) {
			if (i < l) {
				lifes [i].GetComponent<Image> ().sprite = lifeOn;
			} else {
				lifes [i].GetComponent<Image> ().sprite = lifeOff;
			}
		}
	}
}
