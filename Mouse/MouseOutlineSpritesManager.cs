using UnityEngine;

public class MouseOutlineSpritesManager : MonoBehaviour {

	public GameObject outlinePrefab;	// 1, 2
	public GameObject outlinePrefab2;	// 3, 4 ...

	//public GameObject outlineParent;

	private GameObject [] outlineGameObjects = new GameObject[0];

	//public float range;
	private float radius;	// mean damage
	private int number = 1;

	private float rotationAngle = 30;
	//private RifleController rifleController;

	public void SetInfo (/*float range,*/ float radius, int number/*, GameObject g*/) {
		//this.range = range;
		this.radius = radius * 0.025f;
		this.number = number;
		//this.rifleController = g.GetComponent<RifleController>();
	}

	public void FixInfo (/*float range,*/ float radius, int number) {
		//this.range = range;
		this.radius = radius * 0.025f;
		this.number = number;
	}

	void Update () {
		transform.Rotate (Time.deltaTime * rotationAngle * Vector3.forward);

		/*float currentRadius = Vector3.Distance (transform.position, rifleController.transform.position);
		currentRadius = Mathf.Clamp (currentRadius, 0, range);
		Vector3 rotationVector = (transform.position - rifleController.transform.position).normalized;

		transform.position = rifleController.transform.position + rotationVector * currentRadius;*/
	}

	public void ResetMouseSprites () {
		if (outlineGameObjects.Length != number) {
			for (int i = 0; i < outlineGameObjects.Length; i++) {
				Destroy (outlineGameObjects [i]);
			}

			GameObject currentOutlinePrefab;

			if (number <= 2) {
				currentOutlinePrefab = outlinePrefab;
			} else {
				currentOutlinePrefab = outlinePrefab2;
			}

			outlineGameObjects = new GameObject[number];
			for (int i = 0; i < number; i++) {
				outlineGameObjects [i] = Instantiate (currentOutlinePrefab, transform.position, Quaternion.identity);
				outlineGameObjects [i].gameObject.transform.SetParent (gameObject.transform);
			}
		}

		for (int i = 0; i < number; i++) {
			Vector3 localPosition = new Vector3 (
				Mathf.Sin ((i * 360 / number) * Mathf.Deg2Rad) * -1 * radius
				, Mathf.Cos ((i * 360 / number) * Mathf.Deg2Rad) * radius
				, 0);
			outlineGameObjects [i].gameObject.transform.localPosition = localPosition;
			outlineGameObjects [i].gameObject.transform.localRotation = Quaternion.Euler (0, 0, (i * 360 / number));
		}
	}
}
