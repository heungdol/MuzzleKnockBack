using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossAttack : MonoBehaviour {

	public GameObject bossBulletPrefab;

	public float minFireCoolTime;
	public float maxFireCoolTime;

	public float bossBulletPower;

	private PlayerController playercontroller;
	private IEnumerator firing;
	private bool isFiring;

	void Start () {
		playercontroller = FindObjectOfType<PlayerController> ();	
		StartFiring ();
	}

	public void StartFiring () {
		isFiring = true;

		if (firing != null)
			StopCoroutine (firing);
		firing = KeepFiring ();
		StartCoroutine (firing);
	}

	public void StopFiring () {
		isFiring = false;

		StopCoroutine (firing);
	}

	IEnumerator KeepFiring () {
		while (isFiring) {
			float currentCoolTime = Random.Range (minFireCoolTime, maxFireCoolTime);
			yield return new WaitForSeconds (currentCoolTime);

			for (int i = 0; i < 3; i++) {
				Vector3 rotationVector = (playercontroller.transform.position - transform.position).normalized;

				GameObject bullet = Instantiate (bossBulletPrefab, transform.position, Quaternion.Euler (rotationVector));
				bullet.GetComponent<Rigidbody> ().velocity = rotationVector * bossBulletPower;

				yield return new WaitForSeconds (0.25f);
			}

		}
	}
}
