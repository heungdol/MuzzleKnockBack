using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossExplosion : MonoBehaviour {

	public GameObject explosionParticelPrefab;
	public UnityEvent afterExplosionEvent;

	public void StartBossExplosion () {
		StartCoroutine (Explosion ());
	}

	IEnumerator Explosion () {
		yield return new WaitForSeconds (3f);

		GameObject particle = Instantiate (explosionParticelPrefab, transform.position, Quaternion.identity) as GameObject;
		Destroy (particle, particle.GetComponent<ParticleSystem> ().main.startLifetime.constantMax);

		afterExplosionEvent.Invoke();
		gameObject.SetActive (false);
	}
}
