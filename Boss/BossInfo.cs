using UnityEngine;
using UnityEngine.Events;

public class BossInfo : MonoBehaviour {

	public UnityEvent bossHurtEvent;
	public UnityEvent bossDieEvent;

	private float maxLife;
	private float currentLife;

	public float minDeltaAngle = 30;
	public float maxDeltaAngle = 90;

	private BossMovement bossMovement;

	private UIBossLife uiBossLife;

	void Start () {
		maxLife = 500;
		currentLife = maxLife;

		bossMovement = GetComponent<BossMovement> ();
		bossMovement.SetDeltaAngle (minDeltaAngle);

		uiBossLife = FindObjectOfType<UIBossLife> ();
		uiBossLife.StartValue (maxLife);
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Bullet" && currentLife > 0) {
			float bulletDamage = other.GetComponent<BulletController> ().GetBulletDamage ();
			currentLife -= bulletDamage;
			uiBossLife.SetValue (currentLife);

			bossHurtEvent.Invoke ();
			Destroy (other.gameObject);

			if (currentLife <= 0) {
				bossDieEvent.Invoke ();
			}
		}
	}

	public void GiveCurrentDeltaAngleToInfo () {
		float ratio = (maxLife - currentLife) / maxLife;
		float currentDeltaAngle = minDeltaAngle + (maxDeltaAngle - minDeltaAngle) * ratio;

		bossMovement.SetDeltaAngle (currentDeltaAngle);
	}

	public float GetCurrentLife () {
		return currentLife;
	}

	public float GetMaxLife () {
		return maxLife;
	}
}
