using UnityEngine;

public class ItemExp : MonoBehaviour {

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			PlayerInfo playerInfo = other.GetComponent<PlayerInfo> ();
			playerInfo.AddExp ();
			Destroy (gameObject);
		}
	}
}
