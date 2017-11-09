using UnityEngine;

public class BackgroundLightFollowTarget : MonoBehaviour {

	public GameObject targetGameObject;

	//private float positionZ = 1;
	private float time;

	public void SetInfo (GameObject g, float t) {
		this.targetGameObject = g;
		this.time = t;
	}

	void Start () {
		//transform.rotation = Quaternion.Euler (Vector3.back);
		transform.SetParent(targetGameObject.transform);

		if (time != 0) {
			Destroy (this.gameObject, time);
		}
	}

	void Udpate () {
		if (!targetGameObject)
			return;
		
		Vector3 currentPosition;
		currentPosition = targetGameObject.transform.position;
		//currentPosition.z = positionZ;

		transform.position = currentPosition;
	}
}
