using UnityEngine;

public enum LEVELTYPE {
	AIR
	, WATER
	, SPACE
}

public class LevelInfo : MonoBehaviour {

	public GameObject playerStartPivot;
	public float levelDuration = 15;
	public LEVELTYPE levelType;

	private float currentTime;

	private LevelManager levelManager;
	private PlayerController playerController;
	private UILevelTime uiLevelTime;

	void Awake () {
		levelManager = FindObjectOfType<LevelManager> ();
		uiLevelTime = FindObjectOfType<UILevelTime> ();
		playerController = FindObjectOfType<PlayerController>();
	}

	void OnEnable () {
		//Debug.Log ("aaa");
		if (playerController) {
			playerController.gameObject.transform.position = playerStartPivot.transform.position;
			playerController.SetWherePlayerIs (levelType);
		}

		uiLevelTime.StartValue (levelDuration);
	}

	void FixedUpdate () {
		currentTime += Time.deltaTime;
		uiLevelTime.SetValue (currentTime);

		if (levelDuration <= currentTime && playerController && levelManager) {
			levelManager.StartChangeLevel ();
			GetComponent<LevelInfo> ().enabled = false;
		}
	}

	public Vector3 GetPlayerStartPosition () {
		return playerStartPivot.transform.position;
	}
}
