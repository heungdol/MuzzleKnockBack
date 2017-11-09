using UnityEngine;
using UnityEngine.UI;

public class UIExp : MonoBehaviour {

	public GameObject[] exps;
	public Sprite expOn;
	public Sprite expOff;

	void Start () {
		for (int i = 0; i < exps.Length; i++) {
			exps [i].GetComponent<Image> ().sprite = expOff;
		}
	}

	public void ChangeImages (int e) {
		for (int i = 0; i < exps.Length; i++) {
			if (i < e) {
				exps [i].GetComponent<Image> ().sprite = expOn;
			} else {
				exps [i].GetComponent<Image> ().sprite = expOff;
			}
		}
	}
}
