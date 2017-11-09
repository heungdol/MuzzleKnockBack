using UnityEngine;
using UnityEngine.Events;

public class PlayerInfo : MonoBehaviour {

	private int life = 3;
	private int exp = 0;

	public UnityEvent hurtEvent;
	public UnityEvent gameOverEvent;

	private int maxLife;
	private int maxExp;

	private PlayerController playerController;
	private RifleInfo rifleInfo;

	private UILife uiLife;
	private UIExp uiExp;

	private GameObject latestHarmfulGameObject;

	void Start () {
		life = 3;
		exp = 0;

		maxLife = 3;
		maxExp = 5;

		playerController = GetComponent<PlayerController> ();
		rifleInfo = FindObjectOfType<RifleInfo> ();

		uiLife = FindObjectOfType<UILife> ();
		uiExp = FindObjectOfType<UIExp> ();
	}

	public void AddLife () {
		life++;

		if (life >= maxLife) {
			life = maxLife;
		}

		uiLife.ChangeImages (life);
	}

	public void PlayerHurt (GameObject g) {
		SetHarmfulGameObject (g);
		SubLife ();
	}

	void SubLife () {
		if (playerController.IsInvincibility () == true)
			return;

		life--;

		playerController.StartHurt (latestHarmfulGameObject);
		hurtEvent.Invoke ();
		uiLife.ChangeImages (life);

		if (life <= 0) {
			//Debug.Log ("GAME OVER");
			gameOverEvent.Invoke ();
		}
	}

	void SetHarmfulGameObject (GameObject g) {
		latestHarmfulGameObject = g;
	}

	public void AddExp () {
		exp++;

		if (exp >= maxExp) {
			exp -= maxExp;
			rifleInfo.AddLvRandomly ();
		}

		uiExp.ChangeImages (exp);
	}
}
