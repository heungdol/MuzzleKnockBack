using UnityEngine;
using UnityEngine.UI;

public class UIAmmo : MonoBehaviour {

	public GameObject ammoPrefab;
	public GameObject ammoParent;
	public GameObject[] ammos;

	public Sprite ammoOn;
	public Sprite ammoOff;

	public void ChangeImage (int max, int current) {
		if (ammos.Length != max) {
			for (int i = 0; i < ammos.Length; i++) {
				Destroy (ammos [i].gameObject);
			}

			ammos = new GameObject[max];

			for (int i = 0; i < max; i++) {
				GameObject a = Instantiate (ammoPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
				a.transform.SetParent (ammoParent.transform);
				a.transform.localPosition = new Vector3 (i * 30 - 200, 0, 0);	// need fix later

				ammos [i] = a;
			}
		}

		for (int i = 0; i < ammos.Length; i++) {
			if (i < current) {
				ammos [i].GetComponent<Image> ().sprite = ammoOn;
			} else {
				ammos [i].GetComponent<Image> ().sprite = ammoOff;
			}
		}
	}
}
