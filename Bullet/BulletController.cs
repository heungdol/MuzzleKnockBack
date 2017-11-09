using UnityEngine;

public class BulletController : MonoBehaviour {

	public GameObject bulletHitPrefab;

	private Vector3 startPosition;

	private float maxRange;
	private float currentRange;

	private float maxTime;
	private float currentTime;

	private float damage;
	private float a;

	private float maxScaleXRatio = 2.5f;
	private float minScaleXRatio = 1;

	private Vector3 startScale;
	private Vector3 currentScale;

	void Start () {
		startPosition = transform.position;
		startScale = transform.localScale;
	}

	public void SetInfo (float range, float time, float damage) {
		this.maxRange = range;
		this.maxTime = time;
		this.damage = damage;

		a = maxRange / (maxTime * maxTime);
	}

	void FixedUpdate () {
		currentTime += Time.deltaTime;
		currentRange = a * Mathf.Pow ((maxTime - currentTime), 2) * -1 + maxRange; 
		transform.position = startPosition + transform.forward * currentRange;

		currentScale = startScale;
		currentScale.z *= minScaleXRatio + (maxScaleXRatio - minScaleXRatio) * (maxTime - currentTime) / maxTime;
		transform.localScale = currentScale;
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("MovingPlatform")) {
			Instantiate (bulletHitPrefab, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}

	public float GetBulletDamage () {return damage;}

}
