using UnityEngine;

public class VibrationObject : MonoBehaviour {

	private float currentAngle = 0;
	private float deltaAngle = 180;

	private float maxPositionY = 0.05f;

	private Vector3 startLocalPosition;
	private Vector3 currentPosition;

	void Start () {
		startLocalPosition = transform.localPosition;
	}

	void Update () {
		currentAngle += Time.deltaTime * deltaAngle;

		if (currentAngle >= 360) {
			currentAngle -= 360;
		}

		if (currentAngle <= 0) {
			currentAngle += 360;
		}

		currentPosition = startLocalPosition + Vector3.up * Mathf.Sin (currentAngle * Mathf.Deg2Rad) * maxPositionY;
		transform.localPosition = currentPosition;
	}

}
