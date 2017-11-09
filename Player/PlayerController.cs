using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public GameObject playerSpriteGameObject;
	public GameObject rifleGameObject;
	public GameObject explosionParticelPrefab;
	//public GameObject backgroundLightPrefab;

	public bool isInvincibility;
	private float invincibilityTime = 1;
	private float invincibilityGapTime = 0.05f;
	private Vector4 spriteStartColor;
	private Vector4 spriteCurrentColor;
	private float hurtVelocity = 5;

	private RifleController rifleController;
	private RifleInfo rifleInfo;
	private Rigidbody playerRigidbody;
	private SpriteRenderer playerSpriteRenderer;

	private Vector3 startScale;
	private Vector3 currentScale;

	private GameObject currentMovingPlatform;
	private Vector3 movingPlatformOldPosition;
	private Vector3 movingPlatformNewPosition;
	private bool isOnMovingWithPlatform;

	IEnumerator invincibilityCoroutine;

	void Start () {
		rifleController = rifleGameObject.GetComponent<RifleController> ();
		rifleInfo = rifleGameObject.GetComponent<RifleInfo> ();
		playerRigidbody = GetComponent<Rigidbody> ();

		startScale = playerSpriteGameObject.transform.localScale;

		isInvincibility = false;

		playerSpriteRenderer = playerSpriteGameObject.GetComponent<SpriteRenderer> ();
		spriteStartColor = playerSpriteRenderer.color;
		spriteCurrentColor = spriteStartColor;
	}

	void Update () {
		if (rifleController.wherePlayerLookAt () == LOOKDIRECTION.RIGHT) {
			currentScale = startScale;
		} else {
			currentScale = new Vector3 (startScale.x * -1, startScale.y, startScale.z);
		}

		playerSpriteGameObject.transform.localScale = currentScale;
	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.CompareTag("MovingPlatform")) {
			currentMovingPlatform = other.gameObject;
			movingPlatformOldPosition = currentMovingPlatform.transform.position;
			isOnMovingWithPlatform = true;
		}
	}

	void OnCollisionExit (Collision other) {
		if (other.gameObject.CompareTag("MovingPlatform")) {
			currentMovingPlatform = null;
			movingPlatformNewPosition = Vector3.zero;
			movingPlatformOldPosition = Vector3.zero;
			isOnMovingWithPlatform = false;
		}
	}

	void FixedUpdate () {
		if (isOnMovingWithPlatform /*&& currentMovingPlatform.transform.position.y < transform.position.y*/) {
			movingPlatformNewPosition = currentMovingPlatform.transform.position;
			Vector3 gapPosition = movingPlatformNewPosition - movingPlatformOldPosition;

			if (Mathf.Abs (transform.position.y - currentMovingPlatform.transform.position.y) < 0.2f) {

				if ((gapPosition.x > 0 && transform.position.x > currentMovingPlatform.transform.position.x)
				    || (gapPosition.x < 0 && transform.position.x < currentMovingPlatform.transform.position.x)) {

					playerRigidbody.MovePosition (transform.position + gapPosition);

				}

			} else if (transform.position.y > currentMovingPlatform.transform.position.y){
				playerRigidbody.MovePosition (transform.position + gapPosition);
			}

			movingPlatformOldPosition = movingPlatformNewPosition;
		}

		RaycastHit hit;

		/*if (Physics.Raycast (transform.position, Vector3.down, out hit, 0.3f) && playerRigidbody.velocity.y < 0 
			&& (hit.collider.gameObject.tag == "Wall" || hit.collider.gameObject.tag == "MovingPlatform")) {
			//playerRigidbody.velocity -= Vector3.up * playerRigidbody.velocity.y;
			//playerRigidbody.velocity = Vector3.zero;
		}*/

		if (Physics.Raycast (transform.position, Vector3.up, out hit, 0.3f) && playerRigidbody.velocity.y > 0
			&& (hit.collider.gameObject.CompareTag("Wall") || hit.collider.gameObject.CompareTag("MovingPlatform"))) {
			playerRigidbody.velocity -= Vector3.up * playerRigidbody.velocity.y * 2;
		}

		if (Physics.Raycast (transform.position, Vector3.left, out hit, 0.25f) && playerRigidbody.velocity.x < 0
			&& (hit.collider.gameObject.CompareTag("Wall") || hit.collider.gameObject.CompareTag("MovingPlatform"))) {
			playerRigidbody.velocity -= Vector3.right * playerRigidbody.velocity.x;
		}

		if (Physics.Raycast (transform.position, Vector3.right, out hit, 0.25f) && playerRigidbody.velocity.x > 0
			&& (hit.collider.gameObject.CompareTag("Wall") || hit.collider.gameObject.CompareTag("MovingPlatform"))) {
			playerRigidbody.velocity -= Vector3.right * playerRigidbody.velocity.x;
		}
	}

	public void FireForce () {
		Vector3 rotationVector = rifleGameObject.transform.forward * -1;
		Vector3 forceVector = rotationVector * rifleInfo.GetRifleProperties().rifleFixedKnockBackForce;
		playerRigidbody.velocity = forceVector;
	}

	public void VelocityZero () {
		playerRigidbody.velocity = Vector3.zero;
	}

	public bool IsInvincibility () {
		return isInvincibility;
	}

	public void StartHurt (GameObject harmfulGameObject) {
		Vector3 rotationVector = (transform.position - harmfulGameObject.transform.position).normalized;
		playerRigidbody.velocity = rotationVector * hurtVelocity;
	}

	public void StartInvincibility () {
		if (invincibilityCoroutine != null)
			StopCoroutine (invincibilityCoroutine);
		invincibilityCoroutine = Invincibility ();
		StartCoroutine (invincibilityCoroutine);
	}

	IEnumerator Invincibility () {
		isInvincibility = true;

		float invincibilityLeftTime = invincibilityTime % invincibilityGapTime * 2;

		for (int i = 0; i < (invincibilityTime / (invincibilityGapTime * 2)); i++) {
			spriteCurrentColor.w = 0;
			playerSpriteRenderer.color = spriteCurrentColor;
			yield return new WaitForSeconds(invincibilityGapTime);

			spriteCurrentColor.w = 255;
			playerSpriteRenderer.color = spriteCurrentColor;
			yield return new WaitForSeconds(invincibilityGapTime);
		}

		yield return new WaitForSeconds(invincibilityLeftTime);

		playerSpriteRenderer.color = spriteStartColor;

		isInvincibility = false;
	}

	public void SetIsInvicibilityTrue () {
		isInvincibility = true;
	}

	public void GameOver () {
		gameObject.SetActive (false);
	}

	public void InstantiateSmokePrefab () {
		GameObject particle = Instantiate (explosionParticelPrefab, transform.position, Quaternion.identity) as GameObject;
		Destroy (particle, particle.GetComponent<ParticleSystem> ().main.startLifetime.constantMax);
	}

	public void SetWherePlayerIs (LEVELTYPE currentLevelType) {
		switch (currentLevelType) {
		case LEVELTYPE.AIR:
			Physics.gravity = Vector3.up * -15;
			break;
		case LEVELTYPE.WATER:
			Physics.gravity = Vector3.up * -5;
			break;
		case LEVELTYPE.SPACE:
			Physics.gravity = Vector3.zero;
			break;
		}
	}

	public void SetPlayerComponentsFalse () {
		rifleController.enabled = false;
		rifleInfo.enabled = false;
		playerRigidbody.useGravity = false;
		GetComponent<Collider> ().enabled = false;

		VelocityZero ();
		isOnMovingWithPlatform = false; 
	}

	public void SetPlayerComponentsTrue () {
		rifleController.enabled = true;
		rifleInfo.enabled = true;
		playerRigidbody.useGravity = true;
		GetComponent<Collider> ().enabled = true;
	}

}
